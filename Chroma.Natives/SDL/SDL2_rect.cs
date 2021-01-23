using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Point
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Rect
        {
            public int x;
            public int y;
            public int w;
            public int h;
        }

        /* Only available in 2.0.10 or higher. */
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_FPoint
        {
            public float x;
            public float y;
        }

        /* Only available in 2.0.10 or higher. */
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_FRect
        {
            public float x;
            public float y;
            public float w;
            public float h;
        }

        /* Only available in 2.0.4 or higher. */
        public static SDL_bool SDL_PointInRect(ref SDL_Point p, ref SDL_Rect r)
        {
            return p.x >= r.x &&
                   p.x < r.x + r.w &&
                   p.y >= r.y &&
                   p.y < r.y + r.h
                ? SDL_bool.SDL_TRUE
                : SDL_bool.SDL_FALSE;
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_EnclosePoints(
            [In] SDL_Point[] points,
            int count,
            ref SDL_Rect clip,
            out SDL_Rect result
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_HasIntersection(
            ref SDL_Rect A,
            ref SDL_Rect B
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IntersectRect(
            ref SDL_Rect A,
            ref SDL_Rect B,
            out SDL_Rect result
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IntersectRectAndLine(
            ref SDL_Rect rect,
            ref int X1,
            ref int Y1,
            ref int X2,
            ref int Y2
        );

        public static SDL_bool SDL_RectEmpty(ref SDL_Rect r)
        {
            return r.w <= 0 || r.h <= 0 ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE;
        }

        public static SDL_bool SDL_RectEquals(
            ref SDL_Rect a,
            ref SDL_Rect b
        )
        {
            return a.x == b.x &&
                   a.y == b.y &&
                   a.w == b.w &&
                   a.h == b.h
                ? SDL_bool.SDL_TRUE
                : SDL_bool.SDL_FALSE;
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnionRect(
            ref SDL_Rect A,
            ref SDL_Rect B,
            out SDL_Rect result
        );
    }
}