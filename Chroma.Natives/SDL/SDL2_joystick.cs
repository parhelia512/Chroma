using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public const byte SDL_HAT_CENTERED = 0x00;
        public const byte SDL_HAT_UP = 0x01;
        public const byte SDL_HAT_RIGHT = 0x02;
        public const byte SDL_HAT_DOWN = 0x04;
        public const byte SDL_HAT_LEFT = 0x08;
        public const byte SDL_HAT_RIGHTUP = SDL_HAT_RIGHT | SDL_HAT_UP;
        public const byte SDL_HAT_RIGHTDOWN = SDL_HAT_RIGHT | SDL_HAT_DOWN;
        public const byte SDL_HAT_LEFTUP = SDL_HAT_LEFT | SDL_HAT_UP;
        public const byte SDL_HAT_LEFTDOWN = SDL_HAT_LEFT | SDL_HAT_DOWN;

        public enum SDL_JoystickPowerLevel
        {
            SDL_JOYSTICK_POWER_UNKNOWN = -1,
            SDL_JOYSTICK_POWER_EMPTY,
            SDL_JOYSTICK_POWER_LOW,
            SDL_JOYSTICK_POWER_MEDIUM,
            SDL_JOYSTICK_POWER_FULL,
            SDL_JOYSTICK_POWER_WIRED,
            SDL_JOYSTICK_POWER_MAX
        }

        public enum SDL_JoystickType
        {
            SDL_JOYSTICK_TYPE_UNKNOWN,
            SDL_JOYSTICK_TYPE_GAMECONTROLLER,
            SDL_JOYSTICK_TYPE_WHEEL,
            SDL_JOYSTICK_TYPE_ARCADE_STICK,
            SDL_JOYSTICK_TYPE_FLIGHT_STICK,
            SDL_JOYSTICK_TYPE_DANCE_PAD,
            SDL_JOYSTICK_TYPE_GUITAR,
            SDL_JOYSTICK_TYPE_DRUM_KIT,
            SDL_JOYSTICK_TYPE_ARCADE_PAD
        }

        /* joystick refers to an SDL_Joystick*.
         * Only available in 2.0.9 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickRumble(
            IntPtr joystick,
            ushort low_frequency_rumble,
            ushort high_frequency_rumble,
            uint duration_ms
        );

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickClose(IntPtr joystick);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickEventState(int state);

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern short SDL_JoystickGetAxis(
            IntPtr joystick,
            int axis
        );

        /* joystick refers to an SDL_Joystick*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_JoystickGetAxisInitialState(
            IntPtr joystick,
            int axis,
            out ushort state
        );

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickGetBall(
            IntPtr joystick,
            int ball,
            out int dx,
            out int dy
        );

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_JoystickGetButton(
            IntPtr joystick,
            int button
        );

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_JoystickGetHat(
            IntPtr joystick,
            int hat
        );

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, EntryPoint = "SDL_JoystickName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_JoystickName(
            IntPtr joystick
        );

        public static string SDL_JoystickName(IntPtr joystick)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_JoystickName(joystick)
            );
        }

        [DllImport(nativeLibName, EntryPoint = "SDL_JoystickNameForIndex", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_JoystickNameForIndex(
            int device_index
        );

        public static string SDL_JoystickNameForIndex(int device_index)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_JoystickNameForIndex(device_index)
            );
        }

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickNumAxes(IntPtr joystick);

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickNumBalls(IntPtr joystick);

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickNumButtons(IntPtr joystick);

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickNumHats(IntPtr joystick);

        /* IntPtr refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_JoystickOpen(int device_index);

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickUpdate();

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumJoysticks();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Guid SDL_JoystickGetDeviceGUID(
            int device_index
        );

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Guid SDL_JoystickGetGUID(
            IntPtr joystick
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickGetGUIDString(
            Guid guid,
            byte[] pszGUID,
            int cbGUID
        );

        [DllImport(nativeLibName, EntryPoint = "SDL_JoystickGetGUIDFromString",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern Guid INTERNAL_SDL_JoystickGetGUIDFromString(
            byte[] pchGUID
        );

        public static Guid SDL_JoystickGetGUIDFromString(string pchGuid)
        {
            return INTERNAL_SDL_JoystickGetGUIDFromString(
                UTF8_ToNative(pchGuid)
            );
        }

        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetDeviceVendor(int device_index);

        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetDeviceProduct(int device_index);

        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetDeviceProductVersion(int device_index);

        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_JoystickType SDL_JoystickGetDeviceType(int device_index);

        /* int refers to an SDL_JoystickID.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickGetDeviceInstanceID(int device_index);

        /* joystick refers to an SDL_Joystick*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetVendor(IntPtr joystick);

        /* joystick refers to an SDL_Joystick*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetProduct(IntPtr joystick);

        /* joystick refers to an SDL_Joystick*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_JoystickGetProductVersion(IntPtr joystick);

        /* joystick refers to an SDL_Joystick*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_JoystickType SDL_JoystickGetType(IntPtr joystick);

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_JoystickGetAttached(IntPtr joystick);

        /* int refers to an SDL_JoystickID, joystick to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickInstanceID(IntPtr joystick);

        /* joystick refers to an SDL_Joystick*.
         * Only available in 2.0.4 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_JoystickPowerLevel SDL_JoystickCurrentPowerLevel(
            IntPtr joystick
        );

        /* int refers to an SDL_JoystickID, IntPtr to an SDL_Joystick*.
         * Only available in 2.0.4 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_JoystickFromInstanceID(int instance_id);

        /* Only available in 2.0.7 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LockJoysticks();

        /* Only available in 2.0.7 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockJoysticks();

        /* IntPtr refers to an SDL_Joystick*.
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_JoystickFromPlayerIndex(int player_index);

        /* IntPtr refers to an SDL_Joystick*.
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_JoystickSetPlayerIndex(
            IntPtr joystick,
            int player_index
        );
    }
}