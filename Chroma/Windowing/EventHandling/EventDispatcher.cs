﻿using System.Collections.Generic;
using Chroma.Diagnostics.Logging;
using Chroma.Natives.SDL;

namespace Chroma.Windowing.EventHandling
{
    internal sealed class EventDispatcher
    {
        private static readonly Log _log = LogManager.GetForCurrentAssembly();
        
        private Window Owner { get; }

        internal delegate void SdlEventHandler(Window window, SDL2.SDL_Event ev);
        internal delegate void WindowEventHandler(Window window, SDL2.SDL_WindowEvent ev);

        internal Dictionary<SDL2.SDL_EventType, SdlEventHandler> SdlEventHandlers { get; } = new();
        internal Dictionary<SDL2.SDL_WindowEventID, WindowEventHandler> WindowEventHandlers { get; } = new();
        internal Dictionary<SDL2.SDL_EventType, bool> DiscardedEventTypes { get; } = new();

        internal EventDispatcher(Window owner)
        {
            Owner = owner;
        }

        internal void Dispatch(SDL2.SDL_Event ev)
        {
            if (DiscardedEventTypes.ContainsKey(ev.type))
                return;

            if (ev.type == SDL2.SDL_EventType.SDL_WINDOWEVENT)
            {
                DispatchWindowEvent(ev.window);
                return;
            }

            DispatchEvent(ev);
        }

        internal void DispatchWindowEvent(SDL2.SDL_WindowEvent ev)
        {
            if (WindowEventHandlers.ContainsKey(ev.windowEvent))
            {
                WindowEventHandlers[ev.windowEvent]?.Invoke(Owner, ev);
            }
            else
            {
                _log.Debug($"Unsupported window event: {ev.windowEvent}.");
            }
        }

        internal void DispatchEvent(SDL2.SDL_Event ev)
        {
            if (SdlEventHandlers.ContainsKey(ev.type))
            {
                SdlEventHandlers[ev.type]?.Invoke(Owner, ev);
            }
            else
            {
                _log.Debug($"Unsupported generic event: {ev.type}.");
            }
        }
        
        internal void RegisterWindowEventHandler(SDL2.SDL_WindowEventID eventId, WindowEventHandler handler)
        {
            if (WindowEventHandlers.ContainsKey(eventId))
            {
                _log.Warning($"{eventId} handler is getting redefined.");
                WindowEventHandlers[eventId] = handler;
            }
            else
            {
                WindowEventHandlers.Add(eventId, handler);
            }
        }

        internal void RegisterEventHandler(SDL2.SDL_EventType type, SdlEventHandler handler)
        {
            if (SdlEventHandlers.ContainsKey(type))
            {
                _log.Warning($"{type} handler is getting redefined.");
                SdlEventHandlers[type] = handler;
            }
            else
            {
                SdlEventHandlers.Add(type, handler);
            }
        }

        internal void Discard(params SDL2.SDL_EventType[] types)
        {
            foreach (var type in types)
            {
                if (DiscardedEventTypes.ContainsKey(type))
                {
                    _log.Warning($"{type} handler is getting discarded yet another time. Ignoring.");
                    continue;
                }

                DiscardedEventTypes.Add(type, true);
            }
        }
    }
}