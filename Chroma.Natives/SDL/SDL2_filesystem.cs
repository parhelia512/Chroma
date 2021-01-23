using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        /* Only available in 2.0.1 or higher. */
        [DllImport(nativeLibName, EntryPoint = "SDL_GetBasePath", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetBasePath();

        public static string SDL_GetBasePath()
        {
            return UTF8_ToManaged(INTERNAL_SDL_GetBasePath(), true);
        }

        /* Only available in 2.0.1 or higher. */
        [DllImport(nativeLibName, EntryPoint = "SDL_GetPrefPath", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetPrefPath(
            byte[] org,
            byte[] app
        );

        public static string SDL_GetPrefPath(string org, string app)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GetPrefPath(
                    UTF8_ToNative(org),
                    UTF8_ToNative(app)
                ),
                true
            );
        }
    }
}