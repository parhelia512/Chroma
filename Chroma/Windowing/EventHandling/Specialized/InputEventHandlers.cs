﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using Chroma.Hardware;
using Chroma.Input;
using Chroma.Natives.SDL;

namespace Chroma.Windowing.EventHandling.Specialized
{
    internal class InputEventHandlers
    {
        private EventDispatcher Dispatcher { get; }

        internal InputEventHandlers(EventDispatcher dispatcher)
        {
            Dispatcher = dispatcher;

            Dispatcher.Discard(
                SDL2.SDL_EventType.SDL_JOYAXISMOTION,
                SDL2.SDL_EventType.SDL_JOYDEVICEADDED,
                SDL2.SDL_EventType.SDL_JOYDEVICEREMOVED,
                SDL2.SDL_EventType.SDL_JOYBUTTONUP,
                SDL2.SDL_EventType.SDL_JOYBUTTONDOWN,
                SDL2.SDL_EventType.SDL_JOYHATMOTION,
                SDL2.SDL_EventType.SDL_JOYBALLMOTION,
                SDL2.SDL_EventType.SDL_KEYMAPCHANGED,
                SDL2.SDL_EventType.SDL_TEXTEDITING
            );

            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_CONTROLLERDEVICEADDED, ControllerConnected);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED, ControllerDisconnected);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_CONTROLLERBUTTONDOWN, ControllerButtonPressed);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_CONTROLLERBUTTONUP, ControllerButtonReleased);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_CONTROLLERAXISMOTION, ControllerAxisMoved);

            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_KEYUP, KeyReleased);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_KEYDOWN, KeyPressed);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_TEXTINPUT, TextInput);

            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_MOUSEMOTION, MouseMoved);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_MOUSEWHEEL, WheelMoved);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_MOUSEBUTTONDOWN, MousePressed);
            Dispatcher.RegisterEventHandler(SDL2.SDL_EventType.SDL_MOUSEBUTTONUP, MouseReleased);
        }

        private void ControllerAxisMoved(Window owner, SDL2.SDL_Event ev)
        {
            var instance = SDL2.SDL_GameControllerFromInstanceID(ev.caxis.which);
            var controller = ControllerRegistry.Instance.GetControllerByPointer(instance);

            var axis = (ControllerAxis)ev.caxis.axis;

            owner.Game.OnControllerAxisMoved(
                new(controller, axis, ev.caxis.axisValue)
            );
        }

        private void ControllerButtonPressed(Window owner, SDL2.SDL_Event ev)
        {
            var instance = SDL2.SDL_GameControllerFromInstanceID(ev.cbutton.which);
            var controller = ControllerRegistry.Instance.GetControllerByPointer(instance);

            var button = (ControllerButton)ev.cbutton.button;

            Controller.OnButtonPressed(
                owner.Game,
                new(controller, button)
            );
        }

        private void ControllerButtonReleased(Window owner, SDL2.SDL_Event ev)
        {
            var instance = SDL2.SDL_GameControllerFromInstanceID(ev.cbutton.which);
            var controller = ControllerRegistry.Instance.GetControllerByPointer(instance);

            var button = (ControllerButton)ev.cbutton.button;

            Controller.OnButtonReleased(
                owner.Game,
                new(controller, button)
            );
        }

        private void ControllerConnected(Window owner, SDL2.SDL_Event ev)
        {
            var instance = SDL2.SDL_GameControllerOpen(ev.cdevice.which);
            var joyInstance = SDL2.SDL_GameControllerGetJoystick(instance);
            var instanceId = SDL2.SDL_JoystickInstanceID(joyInstance);

            var guid = SDL2.SDL_JoystickGetGUID(joyInstance);
            var serialNumber = SDL2.SDL_GameControllerGetSerial(instance);
            
            var playerIndex = ControllerRegistry.Instance.FindFirstFreePlayerSlot();
            SDL2.SDL_GameControllerSetPlayerIndex(instance, playerIndex);

            var name = SDL2.SDL_GameControllerName(instance);
            var productInfo = new ProductInfo(
                SDL2.SDL_GameControllerGetVendor(instance),
                SDL2.SDL_GameControllerGetProduct(instance)
            );
            var type = (ControllerType)SDL2.SDL_GameControllerGetType(instance);
            var hasConfigurableLed = SDL2.SDL_GameControllerHasLED(instance);

            var hasGyroscope = SDL2.SDL_GameControllerHasSensor(instance, SDL2.SDL_SensorType.SDL_SENSOR_GYRO);
            var hasAccelerometer = SDL2.SDL_GameControllerHasSensor(instance, SDL2.SDL_SensorType.SDL_SENSOR_ACCEL);

            var touchpadCount = SDL2.SDL_GameControllerGetNumTouchpads(instance);
            
            var supportedAxes = new Dictionary<ControllerAxis, bool>();
            for (var i = SDL2.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX;
                i < SDL2.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_MAX;
                i++)
            {
                supportedAxes.Add(
                    (ControllerAxis)i, 
                    SDL2.SDL_GameControllerHasAxis(instance, i)
                );
            }

            var supportedButtons = new Dictionary<ControllerButton, bool>();
            for (var i = SDL2.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A;
                i < SDL2.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_MAX;
                i++)
            {
                supportedButtons.Add(
                    (ControllerButton)i,
                    SDL2.SDL_GameControllerHasButton(instance, i)
                );
            }

            var controllerInfo = new ControllerInfo(
                instance, 
                instanceId,
                guid,
                playerIndex,
                name,
                serialNumber,
                productInfo,
                type,
                hasConfigurableLed,
                hasGyroscope,
                hasAccelerometer,
                touchpadCount,
                supportedAxes,
                supportedButtons
            );

            var controller = ControllerFactory.Create(type, controllerInfo);
            
            ControllerRegistry.Instance.Register(instance, controller);
            
            Controller.OnConnected(
                owner.Game, 
                new(controller)
            );
        }

        private void ControllerDisconnected(Window owner, SDL2.SDL_Event ev)
        {
            var instance = SDL2.SDL_GameControllerFromInstanceID(ev.cdevice.which);
            var controller = ControllerRegistry.Instance.GetControllerByPointer(instance);

            ControllerRegistry.Instance.Unregister(instance);

            Controller.OnDisconnected(
                owner.Game, 
                new ControllerEventArgs(controller)
            );
        }

        private void KeyReleased(Window owner, SDL2.SDL_Event ev)
        {
            Keyboard.OnKeyReleased(
                owner.Game,
                new KeyEventArgs(
                    (ScanCode)ev.key.keysym.scancode,
                    (KeyCode)ev.key.keysym.sym,
                    (KeyModifiers)ev.key.keysym.mod,
                    ev.key.repeat != 0
                )
            );
        }

        private void KeyPressed(Window owner, SDL2.SDL_Event ev)
        {
            Keyboard.OnKeyPressed(
                owner.Game,
                new KeyEventArgs(
                    (ScanCode)ev.key.keysym.scancode,
                    (KeyCode)ev.key.keysym.sym,
                    (KeyModifiers)ev.key.keysym.mod,
                    ev.key.repeat != 0
                )
            );
        }

        private void TextInput(Window owner, SDL2.SDL_Event ev)
        {
            string textInput;
            unsafe
            {
                textInput = Marshal.PtrToStringUTF8(
                    new IntPtr(ev.text.text)
                );
            }

            owner.Game.OnTextInput(new TextInputEventArgs(textInput));
        }

        private void MouseMoved(Window owner, SDL2.SDL_Event ev)
        {
            if (ev.motion.which == SDL2.SDL_TOUCH_MOUSEID)
                return;

            owner.Game.OnMouseMoved(
                new MouseMoveEventArgs(
                    new Vector2(ev.motion.x, ev.motion.y),
                    new Vector2(ev.motion.xrel, ev.motion.yrel),
                    new MouseButtonState(ev.motion.state)
                )
            );
        }

        private void WheelMoved(Window owner, SDL2.SDL_Event ev)
        {
            if (ev.wheel.which == SDL2.SDL_TOUCH_MOUSEID)
                return;

            owner.Game.OnWheelMoved(
                new MouseWheelEventArgs(
                    new Vector2(ev.wheel.x, ev.wheel.y),
                    ev.wheel.direction
                )
            );
        }

        private void MousePressed(Window owner, SDL2.SDL_Event ev)
        {
            if (ev.button.which == SDL2.SDL_TOUCH_MOUSEID)
                return;

            Mouse.OnButtonPressed(
                owner.Game,
                new MouseButtonEventArgs(
                    new Vector2(
                        ev.button.x,
                        ev.button.y
                    ),
                    ev.button.state,
                    ev.button.button,
                    ev.button.clicks
                )
            );
        }

        private void MouseReleased(Window owner, SDL2.SDL_Event ev)
        {
            if (ev.button.which == SDL2.SDL_TOUCH_MOUSEID)
                return;

            Mouse.OnButtonReleased(
                owner.Game,
                new MouseButtonEventArgs(
                    new Vector2(
                        ev.button.x,
                        ev.button.y
                    ),
                    ev.button.state,
                    ev.button.button,
                    ev.button.clicks
                )
            );
        }
    }
}