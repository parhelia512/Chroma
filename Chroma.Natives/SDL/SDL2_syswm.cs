using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public enum SDL_SYSWM_TYPE
        {
            SDL_SYSWM_UNKNOWN,
            SDL_SYSWM_WINDOWS,
            SDL_SYSWM_X11,
            SDL_SYSWM_DIRECTFB,
            SDL_SYSWM_COCOA,
            SDL_SYSWM_UIKIT,
            SDL_SYSWM_WAYLAND,
            SDL_SYSWM_MIR,
            SDL_SYSWM_WINRT,
            SDL_SYSWM_ANDROID,
            SDL_SYSWM_VIVANTE,
            SDL_SYSWM_OS2,
            SDL_SYSWM_HAIKU
        }

        // FIXME: I wish these weren't public...
        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_windows_wminfo
        {
            public IntPtr window; // Refers to an HWND
            public IntPtr hdc; // Refers to an HDC
            public IntPtr hinstance; // Refers to an HINSTANCE
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_winrt_wminfo
        {
            public IntPtr window; // Refers to an IInspectable*
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_x11_wminfo
        {
            public IntPtr display; // Refers to a Display*
            public IntPtr window; // Refers to a Window (XID, use ToInt64!)
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_directfb_wminfo
        {
            public IntPtr dfb; // Refers to an IDirectFB*
            public IntPtr window; // Refers to an IDirectFBWindow*
            public IntPtr surface; // Refers to an IDirectFBSurface*
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_cocoa_wminfo
        {
            public IntPtr window; // Refers to an NSWindow*
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_uikit_wminfo
        {
            public IntPtr window; // Refers to a UIWindow*
            public uint framebuffer;
            public uint colorbuffer;
            public uint resolveFramebuffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_wayland_wminfo
        {
            public IntPtr display; // Refers to a wl_display*
            public IntPtr surface; // Refers to a wl_surface*
            public IntPtr shell_surface; // Refers to a wl_shell_surface*
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_mir_wminfo
        {
            public IntPtr connection; // Refers to a MirConnection*
            public IntPtr surface; // Refers to a MirSurface*
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_android_wminfo
        {
            public IntPtr window; // Refers to an ANativeWindow
            public IntPtr surface; // Refers to an EGLSurface
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNAL_vivante_wminfo
        {
            public IntPtr display; // Refers to an EGLNativeDisplayType
            public IntPtr window; // Refers to an EGLNativeWindowType
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INTERNAL_SysWMDriverUnion
        {
            [FieldOffset(0)] public INTERNAL_windows_wminfo win;
            [FieldOffset(0)] public INTERNAL_winrt_wminfo winrt;
            [FieldOffset(0)] public INTERNAL_x11_wminfo x11;
            [FieldOffset(0)] public INTERNAL_directfb_wminfo dfb;
            [FieldOffset(0)] public INTERNAL_cocoa_wminfo cocoa;
            [FieldOffset(0)] public INTERNAL_uikit_wminfo uikit;
            [FieldOffset(0)] public INTERNAL_wayland_wminfo wl;
            [FieldOffset(0)] public INTERNAL_mir_wminfo mir;
            [FieldOffset(0)] public INTERNAL_android_wminfo android;

            [FieldOffset(0)] public INTERNAL_vivante_wminfo vivante;
            // private int dummy;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_SysWMinfo
        {
            public SDL_version version;
            public SDL_SYSWM_TYPE subsystem;
            public INTERNAL_SysWMDriverUnion info;
        }

        /* window refers to an SDL_Window* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_GetWindowWMInfo(
            IntPtr window,
            ref SDL_SysWMinfo info
        );
    }
}