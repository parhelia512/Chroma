using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public enum SDL_GameControllerBindType
        {
            SDL_CONTROLLER_BINDTYPE_NONE,
            SDL_CONTROLLER_BINDTYPE_BUTTON,
            SDL_CONTROLLER_BINDTYPE_AXIS,
            SDL_CONTROLLER_BINDTYPE_HAT
        }

        public enum SDL_GameControllerAxis
        {
            SDL_CONTROLLER_AXIS_INVALID = -1,
            SDL_CONTROLLER_AXIS_LEFTX,
            SDL_CONTROLLER_AXIS_LEFTY,
            SDL_CONTROLLER_AXIS_RIGHTX,
            SDL_CONTROLLER_AXIS_RIGHTY,
            SDL_CONTROLLER_AXIS_TRIGGERLEFT,
            SDL_CONTROLLER_AXIS_TRIGGERRIGHT,
            SDL_CONTROLLER_AXIS_MAX
        }

        public enum SDL_GameControllerButton
        {
            SDL_CONTROLLER_BUTTON_INVALID = -1,
            SDL_CONTROLLER_BUTTON_A,
            SDL_CONTROLLER_BUTTON_B,
            SDL_CONTROLLER_BUTTON_X,
            SDL_CONTROLLER_BUTTON_Y,
            SDL_CONTROLLER_BUTTON_BACK,
            SDL_CONTROLLER_BUTTON_GUIDE,
            SDL_CONTROLLER_BUTTON_START,
            SDL_CONTROLLER_BUTTON_LEFTSTICK,
            SDL_CONTROLLER_BUTTON_RIGHTSTICK,
            SDL_CONTROLLER_BUTTON_LEFTSHOULDER,
            SDL_CONTROLLER_BUTTON_RIGHTSHOULDER,
            SDL_CONTROLLER_BUTTON_DPAD_UP,
            SDL_CONTROLLER_BUTTON_DPAD_DOWN,
            SDL_CONTROLLER_BUTTON_DPAD_LEFT,
            SDL_CONTROLLER_BUTTON_DPAD_RIGHT,
            SDL_CONTROLLER_BUTTON_MAX,
        }

        public enum SDL_GameControllerType
        {
            SDL_CONTROLLER_TYPE_UNKNOWN = 0,
            SDL_CONTROLLER_TYPE_XBOX360,
            SDL_CONTROLLER_TYPE_XBOXONE,
            SDL_CONTROLLER_TYPE_PS3,
            SDL_CONTROLLER_TYPE_PS4,
            SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_PRO
        }

        // FIXME: I'd rather this somehow be private...
        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_GameControllerButtonBind_hat
        {
            public int hat;
            public int hat_mask;
        }

        // FIXME: I'd rather this somehow be private...
        [StructLayout(LayoutKind.Explicit)]
        public struct INTERNAL_GameControllerButtonBind_union
        {
            [FieldOffset(0)] public int button;
            [FieldOffset(0)] public int axis;
            [FieldOffset(0)] public INTERNAL_GameControllerButtonBind_hat hat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_GameControllerButtonBind
        {
            public SDL_GameControllerBindType bindType;
            public INTERNAL_GameControllerButtonBind_union value;
        }

        /* This exists to deal with C# being stupid about blittable types. */
        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNAL_SDL_GameControllerButtonBind
        {
            public int bindType;

            /* Largest data type in the union is two ints in size */
            public int unionVal0;
            public int unionVal1;
        }

        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerAddMapping",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern int INTERNAL_SDL_GameControllerAddMapping(
            byte[] mappingString
        );

        public static int SDL_GameControllerAddMapping(
            string mappingString
        )
        {
            return INTERNAL_SDL_GameControllerAddMapping(
                UTF8_ToNative(mappingString)
            );
        }

        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerNumMappings();

        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerMappingForIndex",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GameControllerMappingForIndex(int mapping_index);

        public static string SDL_GameControllerMappingForIndex(int mapping_index)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GameControllerMappingForIndex(
                    mapping_index
                )
            );
        }

        /* THIS IS AN RWops FUNCTION! */
        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerAddMappingsFromRW",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern int INTERNAL_SDL_GameControllerAddMappingsFromRW(
            IntPtr rw,
            int freerw
        );

        public static int SDL_GameControllerAddMappingsFromFile(string file)
        {
            IntPtr rwops = SDL_RWFromFile(file, "rb");
            return INTERNAL_SDL_GameControllerAddMappingsFromRW(rwops, 1);
        }

        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerMappingForGUID",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GameControllerMappingForGUID(
            Guid guid
        );

        public static string SDL_GameControllerMappingForGUID(Guid guid)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GameControllerMappingForGUID(guid)
            );
        }

        /* gamecontroller refers to an SDL_GameController* */
        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerMapping",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GameControllerMapping(
            IntPtr gamecontroller
        );

        public static string SDL_GameControllerMapping(
            IntPtr gamecontroller
        )
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GameControllerMapping(
                    gamecontroller
                )
            );
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IsGameController(int joystick_index);

        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerNameForIndex",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GameControllerNameForIndex(
            int joystick_index
        );

        public static string SDL_GameControllerNameForIndex(
            int joystick_index
        )
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GameControllerNameForIndex(joystick_index)
            );
        }

        /* Only available in 2.0.9 or higher. */
        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerMappingForDeviceIndex",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GameControllerMappingForDeviceIndex(
            int joystick_index
        );

        public static string SDL_GameControllerMappingForDeviceIndex(
            int joystick_index
        )
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GameControllerMappingForDeviceIndex(joystick_index)
            );
        }

        /* IntPtr refers to an SDL_GameController* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_GameControllerOpen(int joystick_index);

        /* gamecontroller refers to an SDL_GameController* */
        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GameControllerName(
            IntPtr gamecontroller
        );

        public static string SDL_GameControllerName(
            IntPtr gamecontroller
        )
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GameControllerName(gamecontroller)
            );
        }

        /* gamecontroller refers to an SDL_GameController*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_GameControllerGetVendor(
            IntPtr gamecontroller
        );

        /* gamecontroller refers to an SDL_GameController*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_GameControllerGetProduct(
            IntPtr gamecontroller
        );

        /* gamecontroller refers to an SDL_GameController*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_GameControllerGetProductVersion(
            IntPtr gamecontroller
        );

        /* gamecontroller refers to an SDL_GameController* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_GameControllerGetAttached(
            IntPtr gamecontroller
        );

        /* IntPtr refers to an SDL_Joystick*
         * gamecontroller refers to an SDL_GameController*
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_GameControllerGetJoystick(
            IntPtr gamecontroller
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerEventState(int state);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GameControllerUpdate();

        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerGetAxisFromString",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern SDL_GameControllerAxis INTERNAL_SDL_GameControllerGetAxisFromString(
            byte[] pchString
        );

        public static SDL_GameControllerAxis SDL_GameControllerGetAxisFromString(
            string pchString
        )
        {
            return INTERNAL_SDL_GameControllerGetAxisFromString(
                UTF8_ToNative(pchString)
            );
        }

        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerGetStringForAxis",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GameControllerGetStringForAxis(
            SDL_GameControllerAxis axis
        );

        public static string SDL_GameControllerGetStringForAxis(
            SDL_GameControllerAxis axis
        )
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GameControllerGetStringForAxis(
                    axis
                )
            );
        }

        /* gamecontroller refers to an SDL_GameController* */
        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerGetBindForAxis",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern INTERNAL_SDL_GameControllerButtonBind INTERNAL_SDL_GameControllerGetBindForAxis(
            IntPtr gamecontroller,
            SDL_GameControllerAxis axis
        );

        public static SDL_GameControllerButtonBind SDL_GameControllerGetBindForAxis(
            IntPtr gamecontroller,
            SDL_GameControllerAxis axis
        )
        {
            // This is guaranteed to never be null
            INTERNAL_SDL_GameControllerButtonBind dumb = INTERNAL_SDL_GameControllerGetBindForAxis(
                gamecontroller,
                axis
            );
            SDL_GameControllerButtonBind result = new SDL_GameControllerButtonBind();
            result.bindType = (SDL_GameControllerBindType)dumb.bindType;
            result.value.hat.hat = dumb.unionVal0;
            result.value.hat.hat_mask = dumb.unionVal1;
            return result;
        }

        /* gamecontroller refers to an SDL_GameController* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern short SDL_GameControllerGetAxis(
            IntPtr gamecontroller,
            SDL_GameControllerAxis axis
        );

        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerGetButtonFromString",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern SDL_GameControllerButton INTERNAL_SDL_GameControllerGetButtonFromString(
            byte[] pchString
        );

        public static SDL_GameControllerButton SDL_GameControllerGetButtonFromString(
            string pchString
        )
        {
            return INTERNAL_SDL_GameControllerGetButtonFromString(
                UTF8_ToNative(pchString)
            );
        }

        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerGetStringForButton",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GameControllerGetStringForButton(
            SDL_GameControllerButton button
        );

        public static string SDL_GameControllerGetStringForButton(
            SDL_GameControllerButton button
        )
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GameControllerGetStringForButton(button)
            );
        }

        /* gamecontroller refers to an SDL_GameController* */
        [DllImport(nativeLibName, EntryPoint = "SDL_GameControllerGetBindForButton",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern INTERNAL_SDL_GameControllerButtonBind INTERNAL_SDL_GameControllerGetBindForButton(
            IntPtr gamecontroller,
            SDL_GameControllerButton button
        );

        public static SDL_GameControllerButtonBind SDL_GameControllerGetBindForButton(
            IntPtr gamecontroller,
            SDL_GameControllerButton button
        )
        {
            // This is guaranteed to never be null
            INTERNAL_SDL_GameControllerButtonBind dumb = INTERNAL_SDL_GameControllerGetBindForButton(
                gamecontroller,
                button
            );
            SDL_GameControllerButtonBind result = new SDL_GameControllerButtonBind();
            result.bindType = (SDL_GameControllerBindType)dumb.bindType;
            result.value.hat.hat = dumb.unionVal0;
            result.value.hat.hat_mask = dumb.unionVal1;
            return result;
        }

        /* gamecontroller refers to an SDL_GameController* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_GameControllerGetButton(
            IntPtr gamecontroller,
            SDL_GameControllerButton button
        );

        /* gamecontroller refers to an SDL_GameController*.
         * Only available in 2.0.9 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GameControllerRumble(
            IntPtr gamecontroller,
            ushort low_frequency_rumble,
            ushort high_frequency_rumble,
            uint duration_ms
        );

        /* gamecontroller refers to an SDL_GameController* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GameControllerClose(
            IntPtr gamecontroller
        );

        /* int refers to an SDL_JoystickID, IntPtr to an SDL_GameController*.
         * Only available in 2.0.4 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_GameControllerFromInstanceID(int joyid);

        /* Only available in 2.0.11 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerType SDL_GameControllerTypeForIndex(
            int joystick_index
        );

        /* IntPtr refers to an SDL_GameController*.
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_GameControllerType SDL_GameControllerGetType(
            IntPtr gamecontroller
        );

        /* IntPtr refers to an SDL_GameController*.
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_GameControllerFromPlayerIndex(
            int player_index
        );

        /* IntPtr refers to an SDL_GameController*.
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GameControllerSetPlayerIndex(
            IntPtr gamecontroller,
            int player_index
        );
    }
}