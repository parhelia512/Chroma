using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public const int RW_SEEK_SET = 0;
        public const int RW_SEEK_CUR = 1;
        public const int RW_SEEK_END = 2;

        public const uint SDL_RWOPS_UNKNOWN = 0; /* Unknown stream type */
        public const uint SDL_RWOPS_WINFILE = 1; /* Win32 file */
        public const uint SDL_RWOPS_STDFILE = 2; /* Stdio file */
        public const uint SDL_RWOPS_JNIFILE = 3; /* Android asset */
        public const uint SDL_RWOPS_MEMORY = 4; /* Memory stream */
        public const uint SDL_RWOPS_MEMORY_RO = 5; /* Read-Only memory stream */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long SDLRWopsSizeCallback(IntPtr context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long SDLRWopsSeekCallback(
            IntPtr context,
            long offset,
            int whence
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr SDLRWopsReadCallback(
            IntPtr context,
            IntPtr ptr,
            IntPtr size,
            IntPtr maxnum
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr SDLRWopsWriteCallback(
            IntPtr context,
            IntPtr ptr,
            IntPtr size,
            IntPtr num
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int SDLRWopsCloseCallback(
            IntPtr context
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_RWops
        {
            public IntPtr size;
            public IntPtr seek;
            public IntPtr read;
            public IntPtr write;
            public IntPtr close;

            public uint type;

            /* NOTE: This isn't the full structure since
             * the native SDL_RWops contains a hidden union full of
             * internal information and platform-specific stuff depending
             * on what conditions the native library was built with
             */
        }

        /* IntPtr refers to an SDL_RWops* */
        [DllImport(nativeLibName, EntryPoint = "SDL_RWFromFile", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_RWFromFile(
            byte[] file,
            byte[] mode
        );

        public static IntPtr SDL_RWFromFile(
            string file,
            string mode
        )
        {
            return INTERNAL_SDL_RWFromFile(
                UTF8_ToNative(file),
                UTF8_ToNative(mode)
            );
        }

        /* IntPtr refers to an SDL_RWops* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_AllocRW();

        /* area refers to an SDL_RWops* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeRW(IntPtr area);

        /* fp refers to a void* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_RWFromFP(IntPtr fp, SDL_bool autoclose);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_RWFromMem(IntPtr mem, int size);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr SDL_RWFromMem(void* mem, int size);

        /* mem refers to a const void*, IntPtr to an SDL_RWops* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_RWFromConstMem(IntPtr mem, int size);

        /* context refers to an SDL_RWops*.
         * Only available in SDL 2.0.10 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWsize(IntPtr context);

        /* context refers to an SDL_RWops*.
         * Only available in SDL 2.0.10 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWseek(
            IntPtr context,
            long offset,
            int whence
        );

        /* context refers to an SDL_RWops*.
         * Only available in SDL 2.0.10 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWtell(IntPtr context);

        /* context refers to an SDL_RWops*, ptr refers to a void*.
         * Only available in SDL 2.0.10 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWread(
            IntPtr context,
            IntPtr ptr,
            IntPtr size,
            IntPtr maxnum
        );

        /* context refers to an SDL_RWops*, ptr refers to a const void*.
         * Only available in SDL 2.0.10 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWwrite(
            IntPtr context,
            IntPtr ptr,
            IntPtr size,
            IntPtr maxnum
        );

        /* Read endian functions */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte SDL_ReadU8(IntPtr src);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_ReadLE16(IntPtr src);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort SDL_ReadBE16(IntPtr src);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_ReadLE32(IntPtr src);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_ReadBE32(IntPtr src);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong SDL_ReadLE64(IntPtr src);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong SDL_ReadBE64(IntPtr src);

        /* Write endian functions */

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WriteU8(IntPtr dst, byte value);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WriteLE16(IntPtr dst, ushort value);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WriteBE16(IntPtr dst, ushort value);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WriteLE32(IntPtr dst, uint value);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WriteBE32(IntPtr dst, uint value);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WriteLE64(IntPtr dst, ulong value);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_WriteBE64(IntPtr dst, ulong value);

        /* context refers to an SDL_RWops*
         * Only available in SDL 2.0.10 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern long SDL_RWclose(IntPtr context);

        /* file refers to a const char*, datasize to a size_t*
         * IntPtr refers to a void*
         * Only available in SDL 2.0.10 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_LoadFile(IntPtr file, IntPtr datasize);
    }
}