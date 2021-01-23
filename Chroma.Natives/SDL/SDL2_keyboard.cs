using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Keysym
        {
            public SDL_Scancode scancode;
            public SDL_Keycode sym;
            public SDL_Keymod mod; /* UInt16 */
            public uint unicode; /* Deprecated */
        }

        /* Get the window which has kbd focus */
        /* Return type is an SDL_Window pointer */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_GetKeyboardFocus();

        /* Get a snapshot of the keyboard state. */
        /* Return value is a pointer to a UInt8 array */
        /* Numkeys returns the size of the array if non-null */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_GetKeyboardState(out int numkeys);

        /* Get the current key modifier state for the keyboard. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Keymod SDL_GetModState();

        /* Set the current key modifier state */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetModState(SDL_Keymod modstate);

        /* Get the key code corresponding to the given scancode
         * with the current keyboard layout.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Keycode SDL_GetKeyFromScancode(SDL_Scancode scancode);

        /* Get the scancode for the given keycode */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_Scancode SDL_GetScancodeFromKey(SDL_Keycode key);

        /* Wrapper for SDL_GetScancodeName */
        [DllImport(nativeLibName, EntryPoint = "SDL_GetScancodeName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetScancodeName(SDL_Scancode scancode);

        public static string SDL_GetScancodeName(SDL_Scancode scancode)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GetScancodeName(scancode)
            );
        }

        /* Get a scancode from a human-readable name */
        [DllImport(nativeLibName, EntryPoint = "SDL_GetScancodeFromName", CallingConvention = CallingConvention.Cdecl)]
        private static extern SDL_Scancode INTERNAL_SDL_GetScancodeFromName(
            byte[] name
        );

        public static SDL_Scancode SDL_GetScancodeFromName(string name)
        {
            return INTERNAL_SDL_GetScancodeFromName(
                UTF8_ToNative(name)
            );
        }

        /* Wrapper for SDL_GetKeyName */
        [DllImport(nativeLibName, EntryPoint = "SDL_GetKeyName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetKeyName(SDL_Keycode key);

        public static string SDL_GetKeyName(SDL_Keycode key)
        {
            return UTF8_ToManaged(INTERNAL_SDL_GetKeyName(key));
        }

        /* Get a key code from a human-readable name */
        [DllImport(nativeLibName, EntryPoint = "SDL_GetKeyFromName", CallingConvention = CallingConvention.Cdecl)]
        private static extern SDL_Keycode INTERNAL_SDL_GetKeyFromName(
            byte[] name
        );

        public static SDL_Keycode SDL_GetKeyFromName(string name)
        {
            return INTERNAL_SDL_GetKeyFromName(UTF8_ToNative(name));
        }

        /* Start accepting Unicode text input events, show keyboard */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_StartTextInput();

        /* Check if unicode input events are enabled */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IsTextInputActive();

        /* Stop receiving any text input events, hide onscreen kbd */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_StopTextInput();

        /* Set the rectangle used for text input, hint for IME */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetTextInputRect(ref SDL_Rect rect);

        /* Does the platform support an on-screen keyboard? */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_HasScreenKeyboardSupport();

        /* Is the on-screen keyboard shown for a given window? */
        /* window is an SDL_Window pointer */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IsScreenKeyboardShown(IntPtr window);
    }
}