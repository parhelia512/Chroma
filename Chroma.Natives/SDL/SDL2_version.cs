using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public const int SDL_MAJOR_VERSION = 2;
        public const int SDL_MINOR_VERSION = 0;
        public const int SDL_PATCHLEVEL = 12;

        public static readonly int SDL_COMPILEDVERSION = SDL_VERSIONNUM(
            SDL_MAJOR_VERSION,
            SDL_MINOR_VERSION,
            SDL_PATCHLEVEL
        );

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_version
        {
            public byte major;
            public byte minor;
            public byte patch;
        }

        public static void SDL_VERSION(out SDL_version x)
        {
            x.major = SDL_MAJOR_VERSION;
            x.minor = SDL_MINOR_VERSION;
            x.patch = SDL_PATCHLEVEL;
        }

        public static int SDL_VERSIONNUM(int X, int Y, int Z)
        {
            return X * 1000 + Y * 100 + Z;
        }

        public static bool SDL_VERSION_ATLEAST(int X, int Y, int Z)
        {
            return SDL_COMPILEDVERSION >= SDL_VERSIONNUM(X, Y, Z);
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_GetVersion(out SDL_version ver);
    }
}