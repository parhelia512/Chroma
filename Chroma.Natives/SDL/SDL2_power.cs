using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public enum SDL_PowerState
        {
            SDL_POWERSTATE_UNKNOWN = 0,
            SDL_POWERSTATE_ON_BATTERY,
            SDL_POWERSTATE_NO_BATTERY,
            SDL_POWERSTATE_CHARGING,
            SDL_POWERSTATE_CHARGED
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_PowerState SDL_GetPowerInfo(
            out int secs,
            out int pct
        );
    }
}