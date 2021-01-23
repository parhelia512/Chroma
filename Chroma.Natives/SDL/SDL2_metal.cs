using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        /* Only available in 2.0.11 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_Metal_CreateView(
            IntPtr window
        );

        /* Only available in 2.0.11 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_Metal_DestroyView(
            IntPtr view
        );
    }
}