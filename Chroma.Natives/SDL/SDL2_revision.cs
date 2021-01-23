using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        [DllImport(nativeLibName, EntryPoint = "SDL_GetRevision", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetRevision();

        public static string SDL_GetRevision()
        {
            return UTF8_ToManaged(INTERNAL_SDL_GetRevision());
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRevisionNumber();
    }
}