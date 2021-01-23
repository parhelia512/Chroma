using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SDL_HasClipboardText();

        [DllImport(nativeLibName, EntryPoint = "SDL_GetClipboardText", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetClipboardText();

        public static string SDL_GetClipboardText()
        {
            return UTF8_ToManaged(INTERNAL_SDL_GetClipboardText(), true);
        }

        [DllImport(nativeLibName, EntryPoint = "SDL_SetClipboardText", CallingConvention = CallingConvention.Cdecl)]
        private static extern int INTERNAL_SDL_SetClipboardText(
            byte[] text
        );

        public static int SDL_SetClipboardText(
            string text
        )
        {
            return INTERNAL_SDL_SetClipboardText(
                UTF8_ToNative(text)
            );
        }
    }
}