using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        private const string nativeLibName = "SDL2";

        #region UTF8 Marshaling

        internal static byte[] UTF8_ToNative(string s)
        {
            if (s == null)
            {
                return null;
            }

            // Add a null terminator. That's kind of it... :/
            return System.Text.Encoding.UTF8.GetBytes(s + '\0');
        }

        /* This is public because SDL_DropEvent needs it! */
        public static unsafe string UTF8_ToManaged(IntPtr s, bool freePtr = false)
        {
            if (s == IntPtr.Zero)
            {
                return null;
            }

            /* We get to do strlen ourselves! */
            var ptr = (byte*)s;
            while (*ptr != 0)
                ptr++;

            /* Modern C# lets you just send the byte*, nice! */
            var result = System.Text.Encoding.UTF8.GetString(
                (byte*)s,
                (int)(ptr - (byte*)s)
            );

            /* Some SDL functions will malloc, we have to free! */
            if (freePtr)
            {
                SDL_free(s);
            }
            return result;
        }

        #endregion

        public const uint SDL_INIT_TIMER = 0x00000001;
        public const uint SDL_INIT_AUDIO = 0x00000010;
        public const uint SDL_INIT_VIDEO = 0x00000020;
        public const uint SDL_INIT_JOYSTICK = 0x00000200;
        public const uint SDL_INIT_HAPTIC = 0x00001000;
        public const uint SDL_INIT_GAMECONTROLLER = 0x00002000;
        public const uint SDL_INIT_EVENTS = 0x00004000;
        public const uint SDL_INIT_SENSOR = 0x00008000;
        public const uint SDL_INIT_NOPARACHUTE = 0x00100000;

        public const uint SDL_INIT_EVERYTHING =
            SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO |
            SDL_INIT_EVENTS | SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC |
            SDL_INIT_GAMECONTROLLER | SDL_INIT_SENSOR;

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_Init(uint flags);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_InitSubSystem(uint flags);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_Quit();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_QuitSubSystem(uint flags);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WasInit(uint flags);
    }
}