using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, EntryPoint = "SDL_Vulkan_LoadLibrary", CallingConvention = CallingConvention.Cdecl)]
        private static extern int INTERNAL_SDL_Vulkan_LoadLibrary(
            byte[] path
        );

        public static int SDL_Vulkan_LoadLibrary(string path)
        {
            return INTERNAL_SDL_Vulkan_LoadLibrary(
                UTF8_ToNative(path)
            );
        }

        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_Vulkan_GetVkGetInstanceProcAddr();

        /* Only available in 2.0.6 or higher. */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_Vulkan_UnloadLibrary();

        /* window refers to an SDL_Window*, pNames to a const char**.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_Vulkan_GetInstanceExtensions(
            IntPtr window,
            out uint pCount,
            IntPtr[] pNames
        );

        /* window refers to an SDL_Window.
         * instance refers to a VkInstance.
         * surface refers to a VkSurfaceKHR.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_Vulkan_CreateSurface(
            IntPtr window,
            IntPtr instance,
            out ulong surface
        );

        /* window refers to an SDL_Window*.
         * Only available in 2.0.6 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_Vulkan_GetDrawableSize(
            IntPtr window,
            out int w,
            out int h
        );
    }
}