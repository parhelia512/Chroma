using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        /* System timers rely on different OS mechanisms depending on
         * which operating system SDL2 is compiled against.
         */

        /* Compare tick values, return true if A has passed B. Introduced in SDL 2.0.1,
         * but does not require it (it was a macro).
         */
        public static bool SDL_TICKS_PASSED(uint A, uint B)
        {
            return (int)(B - A) <= 0;
        }

        /* Delays the thread's processing based on the milliseconds parameter */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_Delay(uint ms);

        /* Returns the milliseconds that have passed since SDL was initialized */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetTicks();

        /* Get the current value of the high resolution counter */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong SDL_GetPerformanceCounter();

        /* Get the count per second of the high resolution counter */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong SDL_GetPerformanceFrequency();

        /* param refers to a void* */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint SDL_TimerCallback(uint interval, IntPtr param);

        /* int refers to an SDL_TimerID, param to a void* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AddTimer(
            uint interval,
            SDL_TimerCallback callback,
            IntPtr param
        );

        /* id refers to an SDL_TimerID */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_bool SDL_RemoveTimer(int id);
    }
}