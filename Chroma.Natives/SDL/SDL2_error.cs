using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearError();

        [DllImport(nativeLibName, EntryPoint = "SDL_GetError", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetError();

        public static string SDL_GetError()
        {
            return UTF8_ToManaged(INTERNAL_SDL_GetError());
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_SetError", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_SetError(byte[] fmtAndArglist);

        public static void SDL_SetError(string fmtAndArglist)
        {
            INTERNAL_SDL_SetError(
                UTF8_ToNative(fmtAndArglist)
            );
        }
    }
}