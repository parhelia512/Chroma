using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public static uint SDL_DEFINE_PIXELFOURCC(byte A, byte B, byte C, byte D)
        {
            return SDL_FOURCC(A, B, C, D);
        }

        public static uint SDL_DEFINE_PIXELFORMAT(
            SDL_PixelType type,
            uint order,
            SDL_PackedLayout layout,
            byte bits,
            byte bytes
        )
        {
            return (uint)(
                1 << 28 |
                (byte)type << 24 |
                (byte)order << 20 |
                (byte)layout << 16 |
                bits << 8 |
                bytes
            );
        }

        public static byte SDL_PIXELFLAG(uint X)
        {
            return (byte)(X >> 28 & 0x0F);
        }

        public static byte SDL_PIXELTYPE(uint X)
        {
            return (byte)(X >> 24 & 0x0F);
        }

        public static byte SDL_PIXELORDER(uint X)
        {
            return (byte)(X >> 20 & 0x0F);
        }

        public static byte SDL_PIXELLAYOUT(uint X)
        {
            return (byte)(X >> 16 & 0x0F);
        }

        public static byte SDL_BITSPERPIXEL(uint X)
        {
            return (byte)(X >> 8 & 0xFF);
        }

        public static byte SDL_BYTESPERPIXEL(uint X)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(X))
            {
                if (X == SDL_PIXFMT_YUY2 ||
                    X == SDL_PIXFMT_UYVY ||
                    X == SDL_PIXFMT_YVYU)
                {
                    return 2;
                }
                return 1;
            }
            return (byte)(X & 0xFF);
        }

        public static bool SDL_ISPIXELFORMAT_INDEXED(uint format)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(format))
            {
                return false;
            }
            SDL_PixelType pType =
                (SDL_PixelType)SDL_PIXELTYPE(format);
            return
                pType == SDL_PixelType.SDL_PIXELTYPE_INDEX1 ||
                pType == SDL_PixelType.SDL_PIXELTYPE_INDEX4 ||
                pType == SDL_PixelType.SDL_PIXELTYPE_INDEX8
                ;
        }

        public static bool SDL_ISPIXELFORMAT_PACKED(uint format)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(format))
            {
                return false;
            }
            SDL_PixelType pType =
                (SDL_PixelType)SDL_PIXELTYPE(format);
            return
                pType == SDL_PixelType.SDL_PIXELTYPE_PACKED8 ||
                pType == SDL_PixelType.SDL_PIXELTYPE_PACKED16 ||
                pType == SDL_PixelType.SDL_PIXELTYPE_PACKED32
                ;
        }

        public static bool SDL_ISPIXELFORMAT_ARRAY(uint format)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(format))
            {
                return false;
            }
            SDL_PixelType pType =
                (SDL_PixelType)SDL_PIXELTYPE(format);
            return
                pType == SDL_PixelType.SDL_PIXELTYPE_ARRAYU8 ||
                pType == SDL_PixelType.SDL_PIXELTYPE_ARRAYU16 ||
                pType == SDL_PixelType.SDL_PIXELTYPE_ARRAYU32 ||
                pType == SDL_PixelType.SDL_PIXELTYPE_ARRAYF16 ||
                pType == SDL_PixelType.SDL_PIXELTYPE_ARRAYF32
                ;
        }

        public static bool SDL_ISPIXELFORMAT_ALPHA(uint format)
        {
            if (SDL_ISPIXELFORMAT_PACKED(format))
            {
                SDL_PackedOrder pOrder =
                    (SDL_PackedOrder)SDL_PIXELORDER(format);
                return
                    pOrder == SDL_PackedOrder.SDL_PACKEDORDER_ARGB ||
                    pOrder == SDL_PackedOrder.SDL_PACKEDORDER_RGBA ||
                    pOrder == SDL_PackedOrder.SDL_PACKEDORDER_ABGR ||
                    pOrder == SDL_PackedOrder.SDL_PACKEDORDER_BGRA
                    ;
            }
            else if (SDL_ISPIXELFORMAT_ARRAY(format))
            {
                SDL_ArrayOrder aOrder =
                    (SDL_ArrayOrder)SDL_PIXELORDER(format);
                return
                    aOrder == SDL_ArrayOrder.SDL_ARRAYORDER_ARGB ||
                    aOrder == SDL_ArrayOrder.SDL_ARRAYORDER_RGBA ||
                    aOrder == SDL_ArrayOrder.SDL_ARRAYORDER_ABGR ||
                    aOrder == SDL_ArrayOrder.SDL_ARRAYORDER_BGRA
                    ;
            }
            return false;
        }

        public static bool SDL_ISPIXELFORMAT_FOURCC(uint format)
        {
            return format == 0 && SDL_PIXELFLAG(format) != 1;
        }

        public enum SDL_PixelType
        {
            SDL_PIXELTYPE_UNKNOWN,
            SDL_PIXELTYPE_INDEX1,
            SDL_PIXELTYPE_INDEX4,
            SDL_PIXELTYPE_INDEX8,
            SDL_PIXELTYPE_PACKED8,
            SDL_PIXELTYPE_PACKED16,
            SDL_PIXELTYPE_PACKED32,
            SDL_PIXELTYPE_ARRAYU8,
            SDL_PIXELTYPE_ARRAYU16,
            SDL_PIXELTYPE_ARRAYU32,
            SDL_PIXELTYPE_ARRAYF16,
            SDL_PIXELTYPE_ARRAYF32
        }

        public enum SDL_BitmapOrder
        {
            SDL_BITMAPORDER_NONE,
            SDL_BITMAPORDER_4321,
            SDL_BITMAPORDER_1234
        }

        public enum SDL_PackedOrder
        {
            SDL_PACKEDORDER_NONE,
            SDL_PACKEDORDER_XRGB,
            SDL_PACKEDORDER_RGBX,
            SDL_PACKEDORDER_ARGB,
            SDL_PACKEDORDER_RGBA,
            SDL_PACKEDORDER_XBGR,
            SDL_PACKEDORDER_BGRX,
            SDL_PACKEDORDER_ABGR,
            SDL_PACKEDORDER_BGRA
        }

        public enum SDL_ArrayOrder
        {
            SDL_ARRAYORDER_NONE,
            SDL_ARRAYORDER_RGB,
            SDL_ARRAYORDER_RGBA,
            SDL_ARRAYORDER_ARGB,
            SDL_ARRAYORDER_BGR,
            SDL_ARRAYORDER_BGRA,
            SDL_ARRAYORDER_ABGR
        }

        public enum SDL_PackedLayout
        {
            SDL_PACKEDLAYOUT_NONE,
            SDL_PACKEDLAYOUT_332,
            SDL_PACKEDLAYOUT_4444,
            SDL_PACKEDLAYOUT_1555,
            SDL_PACKEDLAYOUT_5551,
            SDL_PACKEDLAYOUT_565,
            SDL_PACKEDLAYOUT_8888,
            SDL_PACKEDLAYOUT_2101010,
            SDL_PACKEDLAYOUT_1010102
        }

        public const uint SDL_PIXELFORMAT_UNKNOWN = 0;

        public const uint SDL_PIXFMT_INDEX1LSB = (1 << 28)
                                                 | (byte)SDL_PixelType.SDL_PIXELTYPE_INDEX1 << 24
                                                 | (byte)SDL_BitmapOrder.SDL_BITMAPORDER_4321 << 20
                                                 | 0 << 16
                                                 | 1 << 8
                                                 | 0;

        public const uint SDL_PIXFMT_INDEX1MSB = (1 << 28)
                                                 | (byte)SDL_PixelType.SDL_PIXELTYPE_INDEX1 << 24
                                                 | (byte)SDL_BitmapOrder.SDL_BITMAPORDER_1234 << 20
                                                 | 0 << 16
                                                 | 1 << 8
                                                 | 0;

        public const uint SDL_PIXFMT_INDEX4LSB = (1 << 28)
                                                 | (byte)SDL_PixelType.SDL_PIXELTYPE_INDEX4 << 24
                                                 | (byte)SDL_BitmapOrder.SDL_BITMAPORDER_4321 << 20
                                                 | 0 << 16
                                                 | 4 << 8
                                                 | 0;

        public const uint SDL_PIXFMT_INDEX4MSB = (1 << 28)
                                                 | (byte)SDL_PixelType.SDL_PIXELTYPE_INDEX4 << 24
                                                 | (byte)SDL_BitmapOrder.SDL_BITMAPORDER_1234 << 20
                                                 | 0 << 16
                                                 | 4 << 8
                                                 | 0;

        public const uint SDL_PIXFMT_INDEX8 = (1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_INDEX8 << 24
                                              | 0 << 20
                                              | 0 << 16
                                              | 8 << 8
                                              | 1;

        public const uint SDL_PIXFMT_RGB332 = (1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED8 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XRGB << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_332 << 16
                                              | 8 << 8
                                              | 1;

        public const uint SDL_PIXFMT_RGB444 = (1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XRGB << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444 << 16
                                              | 12 << 8
                                              | 2;

        public const uint SDL_PIXFMT_BGR444 = (1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XBGR << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444 << 16
                                              | 12 << 8
                                              | 2;

        public const uint SDL_PIXFMT_RGB555 = (1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XRGB << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_1555 << 16
                                              | 15 << 8
                                              | 2;

        public const uint SDL_PIXFMT_BGR555 = (1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XBGR << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_1555 << 16
                                              | 15 << 8
                                              | 2;

        public const uint SDL_PIXFMT_ARGB4444 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_ARGB << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444 << 16
                                                | 16 << 8
                                                | 2;

        public const uint SDL_PIXFMT_RGBA4444 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_RGBA << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444 << 16
                                                | 16 << 8
                                                | 2;

        public const uint SDL_PIXFMT_ABGR4444 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_ABGR << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444 << 16
                                                | 16 << 8
                                                | 2;

        public const uint SDL_PIXFMT_BGRA4444 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_BGRA << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_4444 << 16
                                                | 16 << 8
                                                | 2;


        public const uint SDL_PIXFMT_ARGB1555 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_ARGB << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_1555 << 16
                                                | 16 << 8
                                                | 2;

        public const uint SDL_PIXFMT_ABGR1555 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_ABGR << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_1555 << 16
                                                | 16 << 8
                                                | 2;

        public const uint SDL_PIXFMT_ARGB5551 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_RGBA << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_5551 << 16
                                                | 16 << 8
                                                | 2;

        public const uint SDL_PIXFMT_ABGR5551 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_ABGR << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_5551 << 16
                                                | 16 << 8
                                                | 2;

        public const uint SDL_PIXFMT_BGRA5551 = (1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_BGRA << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_5551 << 16
                                                | 16 << 8
                                                | 2;

        public const uint SDL_PIXFMT_RGB565 = (1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XRGB << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_565 << 16
                                              | 16 << 8
                                              | 2;

        public const uint SDL_PIXFMT_BGR565 = (1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED16 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XBGR << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_565 << 16
                                              | 16 << 8
                                              | 2;

        public const uint SDL_PIXFMT_RGB24 = (1 << 28)
                                             | (byte)SDL_PixelType.SDL_PIXELTYPE_ARRAYU8 << 24
                                             | (byte)SDL_ArrayOrder.SDL_ARRAYORDER_RGB << 20
                                             | 0 << 16
                                             | 24 << 8
                                             | 3;

        public const uint SDL_PIXFMT_BGR24 = (1 << 28)
                                             | (byte)SDL_PixelType.SDL_PIXELTYPE_ARRAYU8 << 24
                                             | (byte)SDL_ArrayOrder.SDL_ARRAYORDER_BGR << 20
                                             | 0 << 16
                                             | 24 << 8
                                             | 3;


        public const uint SDL_PIXFMT_RGB888 = (uint)(1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XRGB << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888 << 16
                                              | 24 << 8
                                              | 4;

        public const uint SDL_PIXFMT_BGR888 = (uint)(1 << 28)
                                              | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                              | (byte)SDL_PackedOrder.SDL_PACKEDORDER_XBGR << 20
                                              | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888 << 16
                                              | 24 << 8
                                              | 4;

        public const uint SDL_PIXFMT_RGBX8888 = (uint)(1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_RGBX << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888 << 16
                                                | 24 << 8
                                                | 4;

        public const uint SDL_PIXFMT_BGRX8888 = (uint)(1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_BGRX << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888 << 16
                                                | 24 << 8
                                                | 4;

        public const uint SDL_PIXFMT_ARGB8888 = (uint)(1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_ARGB << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888 << 16
                                                | (byte)32 << 8
                                                | 4;

        public const uint SDL_PIXFMT_RGBA8888 = (uint)(1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_RGBA << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888 << 16
                                                | (byte)32 << 8
                                                | 4;

        public const uint SDL_PIXFMT_ABGR8888 = (uint)(1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_ABGR << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888 << 16
                                                | (byte)32 << 8
                                                | 4;

        public const uint SDL_PIXFMT_BGRA8888 = (uint)(1 << 28)
                                                | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                                | (byte)SDL_PackedOrder.SDL_PACKEDORDER_BGRA << 20
                                                | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_8888 << 16
                                                | (byte)32 << 8
                                                | 4;

        public const uint SDL_PIXFMT_ARGB2101010 = (uint)(1 << 28)
                                                   | (byte)SDL_PixelType.SDL_PIXELTYPE_PACKED32 << 24
                                                   | (byte)SDL_PackedOrder.SDL_PACKEDORDER_ARGB << 20
                                                   | (byte)SDL_PackedLayout.SDL_PACKEDLAYOUT_2101010 << 16
                                                   | 32 << 8
                                                   | 4;

        public const uint SDL_PIXFMT_YV12 = (uint)((byte)'Y'
                                                   | ((byte)'V') << 8 
                                                   | ((byte)'1') << 16 
                                                   | ((byte)'2') << 24);
        
        public const uint SDL_PIXFMT_IYUV = (uint)((byte)'I'
                                                   | ((byte)'Y') << 8 
                                                   | ((byte)'U') << 16 
                                                   | ((byte)'V') << 24);

        public const uint SDL_PIXFMT_YUY2 = (uint)((byte)'Y'
                                                   | ((byte)'U') << 8 
                                                   | ((byte)'Y') << 16 
                                                   | ((byte)'2') << 24);

        public const uint SDL_PIXFMT_UYVY = (uint)((byte)'U'
                                                   | ((byte)'Y') << 8 
                                                   | ((byte)'V') << 16 
                                                   | ((byte)'Y') << 24);
        
        public const uint SDL_PIXFMT_YVYU = (uint)((byte)'Y'
                                                   | ((byte)'V') << 8 
                                                   | ((byte)'Y') << 16 
                                                   | ((byte)'U') << 24);

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Color
        {
            public byte r;
            public byte g;
            public byte b;
            public byte a;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Palette
        {
            public int ncolors;
            public IntPtr colors;
            public int version;
            public int refcount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_PixelFormat
        {
            public uint format;
            public IntPtr palette; // SDL_Palette*
            public byte BitsPerPixel;
            public byte BytesPerPixel;
            public uint Rmask;
            public uint Gmask;
            public uint Bmask;
            public uint Amask;
            public byte Rloss;
            public byte Gloss;
            public byte Bloss;
            public byte Aloss;
            public byte Rshift;
            public byte Gshift;
            public byte Bshift;
            public byte Ashift;
            public int refcount;
            public IntPtr next; // SDL_PixelFormat*
        }

        /* IntPtr refers to an SDL_PixelFormat* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_AllocFormat(uint pixel_format);

        /* IntPtr refers to an SDL_Palette* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_AllocPalette(int ncolors);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_CalculateGammaRamp(
            float gamma,
            [Out()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)]
            ushort[] ramp
        );

        /* format refers to an SDL_PixelFormat* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeFormat(IntPtr format);

        /* palette refers to an SDL_Palette* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreePalette(IntPtr palette);

        [DllImport(nativeLibName, EntryPoint = "SDL_GetPixelFormatName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetPixelFormatName(
            uint format
        );

        public static string SDL_GetPixelFormatName(uint format)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GetPixelFormatName(format)
            );
        }

        /* format refers to an SDL_PixelFormat* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetRGB(
            uint pixel,
            IntPtr format,
            out byte r,
            out byte g,
            out byte b
        );

        /* format refers to an SDL_PixelFormat* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetRGBA(
            uint pixel,
            IntPtr format,
            out byte r,
            out byte g,
            out byte b,
            out byte a
        );

        /* format refers to an SDL_PixelFormat* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_MapRGB(
            IntPtr format,
            byte r,
            byte g,
            byte b
        );

        /* format refers to an SDL_PixelFormat* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_MapRGBA(
            IntPtr format,
            byte r,
            byte g,
            byte b,
            byte a
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_MasksToPixelFormatEnum(
            int bpp,
            uint Rmask,
            uint Gmask,
            uint Bmask,
            uint Amask
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_PixelFormatEnumToMasks(
            uint format,
            out int bpp,
            out uint Rmask,
            out uint Gmask,
            out uint Bmask,
            out uint Amask
        );

        /* palette refers to an SDL_Palette* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetPaletteColors(
            IntPtr palette,
            [In] SDL_Color[] colors,
            int firstcolor,
            int ncolors
        );

        /* format and palette refer to an SDL_PixelFormat* and SDL_Palette* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetPixelFormatPalette(
            IntPtr format,
            IntPtr palette
        );
    }
}