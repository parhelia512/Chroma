using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        /* Windows */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr SDL_WindowsMessageHook(
            IntPtr userdata,
            IntPtr hWnd,
            uint message,
            ulong wParam,
            long lParam
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SetWindowsMessageHook(
            SDL_WindowsMessageHook callback,
            IntPtr userdata
        );

        /* iOS */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SDL_iPhoneAnimationCallback(IntPtr p);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_iPhoneSetAnimationCallback(
            IntPtr window, /* SDL_Window* */
            int interval,
            SDL_iPhoneAnimationCallback callback,
            IntPtr callbackParam
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_iPhoneSetEventPump(SDL_bool enabled);

        /* Android */

        public const int SDL_ANDROID_EXTERNAL_STORAGE_READ = 0x01;
        public const int SDL_ANDROID_EXTERNAL_STORAGE_WRITE = 0x02;

        /* IntPtr refers to a JNIEnv* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_AndroidGetJNIEnv();

        /* IntPtr refers to a jobject */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_AndroidGetActivity();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IsAndroidTV();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IsChromebook();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IsDeXMode();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AndroidBackButton();

        [DllImport(nativeLibName, EntryPoint = "SDL_AndroidGetInternalStoragePath",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_AndroidGetInternalStoragePath();

        public static string SDL_AndroidGetInternalStoragePath()
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_AndroidGetInternalStoragePath()
            );
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AndroidGetExternalStorageState();

        [DllImport(nativeLibName, EntryPoint = "SDL_AndroidGetExternalStorageState",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_AndroidGetExternalStoragePath();

        public static string SDL_AndroidGetExternalStoragePath()
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_AndroidGetExternalStoragePath()
            );
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetAndroidSDKVersion();

        /* WinRT */

        public enum SDL_WinRT_DeviceFamily
        {
            SDL_WINRT_DEVICEFAMILY_UNKNOWN,
            SDL_WINRT_DEVICEFAMILY_DESKTOP,
            SDL_WINRT_DEVICEFAMILY_MOBILE,
            SDL_WINRT_DEVICEFAMILY_XBOX
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_WinRT_DeviceFamily SDL_WinRTGetDeviceFamily();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_IsTablet();
    }
}