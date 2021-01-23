using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        
        public static uint SDL_FOURCC(byte A, byte B, byte C, byte D)
        {
            return (uint)(A | B << 8 | C << 16 | D << 24);
        }

        public enum SDL_bool
        {
            SDL_FALSE = 0,
            SDL_TRUE = 1
        }

        /* malloc/free are used by the marshaler! -flibit */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SDL_malloc(IntPtr size);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SDL_free(IntPtr memblock);

        /* Buffer.BlockCopy is not available in every runtime yet. Also,
         * using memcpy directly can be a compatibility issue in other
         * strange ways. So, we expose this to get around all that.
         * -flibit
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_memcpy(IntPtr dst, IntPtr src, IntPtr len);

    }
}