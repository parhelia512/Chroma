using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        [Flags]
        public enum SDL_RendererFlags : uint
        {
            SDL_RENDERER_SOFTWARE = 0x00000001,
            SDL_RENDERER_ACCELERATED = 0x00000002,
            SDL_RENDERER_PRESENTVSYNC = 0x00000004,
            SDL_RENDERER_TARGETTEXTURE = 0x00000008
        }

        [Flags]
        public enum SDL_RendererFlip
        {
            SDL_FLIP_NONE = 0x00000000,
            SDL_FLIP_HORIZONTAL = 0x00000001,
            SDL_FLIP_VERTICAL = 0x00000002
        }

        public enum SDL_TextureAccess
        {
            SDL_TEXTUREACCESS_STATIC,
            SDL_TEXTUREACCESS_STREAMING,
            SDL_TEXTUREACCESS_TARGET
        }

        [Flags]
        public enum SDL_TextureModulate
        {
            SDL_TEXTUREMODULATE_NONE = 0x00000000,
            SDL_TEXTUREMODULATE_HORIZONTAL = 0x00000001,
            SDL_TEXTUREMODULATE_VERTICAL = 0x00000002
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SDL_RendererInfo
        {
            public IntPtr name; // const char*
            public uint flags;
            public uint num_texture_formats;
            public fixed uint texture_formats[16];
            public int max_texture_width;
            public int max_texture_height;
        }

        /* Only available in 2.0.11 or higher. */
        public enum SDL_ScaleMode
        {
            SDL_ScaleModeNearest,
            SDL_ScaleModeLinear,
            SDL_ScaleModeBest
        }

        /* IntPtr refers to an SDL_Renderer*, window to an SDL_Window* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_CreateRenderer(
            IntPtr window,
            int index,
            SDL_RendererFlags flags
        );

        /* IntPtr refers to an SDL_Renderer*, surface to an SDL_Surface* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_CreateSoftwareRenderer(IntPtr surface);

        /* IntPtr refers to an SDL_Texture*, renderer to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_CreateTexture(
            IntPtr renderer,
            uint format,
            int access,
            int w,
            int h
        );

        /* IntPtr refers to an SDL_Texture*
         * renderer refers to an SDL_Renderer*
         * surface refers to an SDL_Surface*
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_CreateTextureFromSurface(
            IntPtr renderer,
            IntPtr surface
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DestroyRenderer(IntPtr renderer);

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_DestroyTexture(IntPtr texture);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumRenderDrivers();

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDrawBlendMode(
            IntPtr renderer,
            out SDL_BlendMode blendMode
        );

        /* texture refers to an SDL_Texture*
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureScaleMode(
            IntPtr texture,
            SDL_ScaleMode scaleMode
        );

        /* texture refers to an SDL_Texture*
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureScaleMode(
            IntPtr texture,
            out SDL_ScaleMode scaleMode
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDrawColor(
            IntPtr renderer,
            out byte r,
            out byte g,
            out byte b,
            out byte a
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRenderDriverInfo(
            int index,
            out SDL_RendererInfo info
        );

        /* IntPtr refers to an SDL_Renderer*, window to an SDL_Window* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_GetRenderer(IntPtr window);

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRendererInfo(
            IntPtr renderer,
            out SDL_RendererInfo info
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetRendererOutputSize(
            IntPtr renderer,
            out int w,
            out int h
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureAlphaMod(
            IntPtr texture,
            out byte alpha
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureBlendMode(
            IntPtr texture,
            out SDL_BlendMode blendMode
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetTextureColorMod(
            IntPtr texture,
            out byte r,
            out byte g,
            out byte b
        );

        /* texture refers to an SDL_Texture*, pixels to a void* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockTexture(
            IntPtr texture,
            ref SDL_Rect rect,
            out IntPtr pixels,
            out int pitch
        );

        /* texture refers to an SDL_Texture*, pixels to a void*.
         * Internally, this function contains logic to use default values when
         * the rectangle is passed as NULL.
         * This overload allows for IntPtr.Zero to be passed for rect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockTexture(
            IntPtr texture,
            IntPtr rect,
            out IntPtr pixels,
            out int pitch
        );

        /* texture refers to an SDL_Texture*, surface to an SDL_Surface*
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockTextureToSurface(
            IntPtr texture,
            ref SDL_Rect rect,
            out IntPtr surface
        );

        /* texture refers to an SDL_Texture*, surface to an SDL_Surface*
         * Internally, this function contains logic to use default values when
         * the rectangle is passed as NULL.
         * This overload allows for IntPtr.Zero to be passed for rect.
         * Only available in 2.0.11 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_LockTextureToSurface(
            IntPtr texture,
            IntPtr rect,
            out IntPtr surface
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_QueryTexture(
            IntPtr texture,
            out uint format,
            out int access,
            out int w,
            out int h
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderClear(IntPtr renderer);

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_Rect dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source and destination rectangles are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for srcrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            ref SDL_Rect dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source and destination rectangles are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for dstrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            IntPtr dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source and destination rectangles are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for both SDL_Rects.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            IntPtr dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_Rect dstrect,
            double angle,
            ref SDL_Point center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for srcrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            ref SDL_Rect dstrect,
            double angle,
            ref SDL_Point center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for dstrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            IntPtr dstrect,
            double angle,
            ref SDL_Point center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for center.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_Rect dstrect,
            double angle,
            IntPtr center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for both
         * srcrect and dstrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            IntPtr dstrect,
            double angle,
            ref SDL_Point center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for both
         * srcrect and center.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            ref SDL_Rect dstrect,
            double angle,
            IntPtr center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for both
         * dstrect and center.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            IntPtr dstrect,
            double angle,
            IntPtr center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for all
         * three parameters.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            IntPtr dstrect,
            double angle,
            IntPtr center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLine(
            IntPtr renderer,
            int x1,
            int y1,
            int x2,
            int y2
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLines(
            IntPtr renderer,
            [In] SDL_Point[] points,
            int count
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPoint(
            IntPtr renderer,
            int x,
            int y
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPoints(
            IntPtr renderer,
            [In] SDL_Point[] points,
            int count
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRect(
            IntPtr renderer,
            ref SDL_Rect rect
        );

        /* renderer refers to an SDL_Renderer*, rect to an SDL_Rect*.
         * This overload allows for IntPtr.Zero (null) to be passed for rect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRect(
            IntPtr renderer,
            IntPtr rect
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRects(
            IntPtr renderer,
            [In] SDL_Rect[] rects,
            int count
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRect(
            IntPtr renderer,
            ref SDL_Rect rect
        );

        /* renderer refers to an SDL_Renderer*, rect to an SDL_Rect*.
         * This overload allows for IntPtr.Zero (null) to be passed for rect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRect(
            IntPtr renderer,
            IntPtr rect
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRects(
            IntPtr renderer,
            [In] SDL_Rect[] rects,
            int count
        );

        #region Floating Point Render Functions

        /* This region only available in SDL 2.0.10 or higher. */

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyF(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_FRect dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source and destination rectangles are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for srcrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyF(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            ref SDL_FRect dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source and destination rectangles are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for dstrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyF(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            IntPtr dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source and destination rectangles are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for both SDL_Rects.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyF(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            IntPtr dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_FRect dstrect,
            double angle,
            ref SDL_FPoint center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for srcrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            ref SDL_FRect dstrect,
            double angle,
            ref SDL_FPoint center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for dstrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            IntPtr dstrect,
            double angle,
            ref SDL_FPoint center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for center.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_FRect dstrect,
            double angle,
            IntPtr center,
            SDL_RendererFlip flip
        );
        
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_FRect dstrect,
            double angle,
            ref SDL_FPoint center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for both
         * srcrect and dstrect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            IntPtr dstrect,
            double angle,
            ref SDL_FPoint center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for both
         * srcrect and center.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            ref SDL_FRect dstrect,
            double angle,
            IntPtr center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for both
         * dstrect and center.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            IntPtr dstrect,
            double angle,
            IntPtr center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
         * Internally, this function contains logic to use default values when
         * source, destination, and/or center are passed as NULL.
         * This overload allows for IntPtr.Zero (null) to be passed for all
         * three parameters.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderCopyExF(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            IntPtr dstrect,
            double angle,
            IntPtr center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPointF(
            IntPtr renderer,
            float x,
            float y
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawPointsF(
            IntPtr renderer,
            [In] SDL_FPoint[] points,
            int count
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLineF(
            IntPtr renderer,
            float x1,
            float y1,
            float x2,
            float y2
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawLinesF(
            IntPtr renderer,
            [In] SDL_FPoint[] points,
            int count
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRectF(
            IntPtr renderer,
            ref SDL_FRect rect
        );

        /* renderer refers to an SDL_Renderer*, rect to an SDL_Rect*.
         * This overload allows for IntPtr.Zero (null) to be passed for rect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRectF(
            IntPtr renderer,
            IntPtr rect
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderDrawRectsF(
            IntPtr renderer,
            [In] SDL_FRect[] rects,
            int count
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRectF(
            IntPtr renderer,
            ref SDL_FRect rect
        );

        /* renderer refers to an SDL_Renderer*, rect to an SDL_Rect*.
         * This overload allows for IntPtr.Zero (null) to be passed for rect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRectF(
            IntPtr renderer,
            IntPtr rect
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFillRectsF(
            IntPtr renderer,
            [In] SDL_FRect[] rects,
            int count
        );

        #endregion

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetClipRect(
            IntPtr renderer,
            out SDL_Rect rect
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetLogicalSize(
            IntPtr renderer,
            out int w,
            out int h
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderGetScale(
            IntPtr renderer,
            out float scaleX,
            out float scaleY
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderGetViewport(
            IntPtr renderer,
            out SDL_Rect rect
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_RenderPresent(IntPtr renderer);

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderReadPixels(
            IntPtr renderer,
            ref SDL_Rect rect,
            uint format,
            IntPtr pixels,
            int pitch
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetClipRect(
            IntPtr renderer,
            ref SDL_Rect rect
        );

        /* renderer refers to an SDL_Renderer*
         * This overload allows for IntPtr.Zero (null) to be passed for rect.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetClipRect(
            IntPtr renderer,
            IntPtr rect
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetLogicalSize(
            IntPtr renderer,
            int w,
            int h
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetScale(
            IntPtr renderer,
            float scaleX,
            float scaleY
        );

        /* renderer refers to an SDL_Renderer*
         * Only available in 2.0.5 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetIntegerScale(
            IntPtr renderer,
            SDL_bool enable
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderSetViewport(
            IntPtr renderer,
            ref SDL_Rect rect
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderDrawBlendMode(
            IntPtr renderer,
            SDL_BlendMode blendMode
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderDrawColor(
            IntPtr renderer,
            byte r,
            byte g,
            byte b,
            byte a
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetRenderTarget(
            IntPtr renderer,
            IntPtr texture
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureAlphaMod(
            IntPtr texture,
            byte alpha
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureBlendMode(
            IntPtr texture,
            SDL_BlendMode blendMode
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SetTextureColorMod(
            IntPtr texture,
            byte r,
            byte g,
            byte b
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockTexture(IntPtr texture);

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateTexture(
            IntPtr texture,
            ref SDL_Rect rect,
            IntPtr pixels,
            int pitch
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateTexture(
            IntPtr texture,
            IntPtr rect,
            IntPtr pixels,
            int pitch
        );

        /* texture refers to an SDL_Texture*
         * Only available in 2.0.1 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_UpdateYUVTexture(
            IntPtr texture,
            ref SDL_Rect rect,
            IntPtr yPlane,
            int yPitch,
            IntPtr uPlane,
            int uPitch,
            IntPtr vPlane,
            int vPitch
        );

        /* renderer refers to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_RenderTargetSupported(
            IntPtr renderer
        );

        /* IntPtr refers to an SDL_Texture*, renderer to an SDL_Renderer* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_GetRenderTarget(IntPtr renderer);

        /* renderer refers to an SDL_Renderer*
         * Only available in 2.0.8 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_RenderGetMetalLayer(
            IntPtr renderer
        );

        /* renderer refers to an SDL_Renderer*
         * Only available in 2.0.8 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_RenderGetMetalCommandEncoder(
            IntPtr renderer
        );

        /* renderer refers to an SDL_Renderer*
         * Only available in 2.0.4 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_RenderIsClipEnabled(IntPtr renderer);

        /* renderer refers to an SDL_Renderer*
         * Only available in 2.0.10 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_RenderFlush(IntPtr renderer);
    }
}