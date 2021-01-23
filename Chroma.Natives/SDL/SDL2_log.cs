using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public enum SDL_LogCategory
        {
            SDL_LOG_CATEGORY_APPLICATION,
            SDL_LOG_CATEGORY_ERROR,
            SDL_LOG_CATEGORY_ASSERT,
            SDL_LOG_CATEGORY_SYSTEM,
            SDL_LOG_CATEGORY_AUDIO,
            SDL_LOG_CATEGORY_VIDEO,
            SDL_LOG_CATEGORY_RENDER,
            SDL_LOG_CATEGORY_INPUT,
            SDL_LOG_CATEGORY_TEST,

            /* Reserved for future SDL library use */
            SDL_LOG_CATEGORY_RESERVED1,
            SDL_LOG_CATEGORY_RESERVED2,
            SDL_LOG_CATEGORY_RESERVED3,
            SDL_LOG_CATEGORY_RESERVED4,
            SDL_LOG_CATEGORY_RESERVED5,
            SDL_LOG_CATEGORY_RESERVED6,
            SDL_LOG_CATEGORY_RESERVED7,
            SDL_LOG_CATEGORY_RESERVED8,
            SDL_LOG_CATEGORY_RESERVED9,
            SDL_LOG_CATEGORY_RESERVED10,

            /* Beyond this point is reserved for application use, e.g.
            enum {
                MYAPP_CATEGORY_AWESOME1 = SDL_LOG_CATEGORY_CUSTOM,
                MYAPP_CATEGORY_AWESOME2,
                MYAPP_CATEGORY_AWESOME3,
                ...
            };
            */
            SDL_LOG_CATEGORY_CUSTOM
        }

        public enum SDL_LogPriority
        {
            SDL_LOG_PRIORITY_VERBOSE = 1,
            SDL_LOG_PRIORITY_DEBUG,
            SDL_LOG_PRIORITY_INFO,
            SDL_LOG_PRIORITY_WARN,
            SDL_LOG_PRIORITY_ERROR,
            SDL_LOG_PRIORITY_CRITICAL,
            SDL_NUM_LOG_PRIORITIES
        }

        /* userdata refers to a void*, message to a const char* */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SDL_LogOutputFunction(
            IntPtr userdata,
            int category,
            SDL_LogPriority priority,
            IntPtr message
        );

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_Log", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_Log(byte[] fmtAndArglist);

        public static void SDL_Log(string fmtAndArglist)
        {
            INTERNAL_SDL_Log(
                UTF8_ToNative(fmtAndArglist)
            );
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_LogVerbose", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_LogVerbose(
            int category,
            byte[] fmtAndArglist
        );

        public static void SDL_LogVerbose(
            int category,
            string fmtAndArglist
        )
        {
            INTERNAL_SDL_LogVerbose(
                category,
                UTF8_ToNative(fmtAndArglist)
            );
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_LogDebug", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_LogDebug(
            int category,
            byte[] fmtAndArglist
        );

        public static void SDL_LogDebug(
            int category,
            string fmtAndArglist
        )
        {
            INTERNAL_SDL_LogDebug(
                category,
                UTF8_ToNative(fmtAndArglist)
            );
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_LogInfo", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_LogInfo(
            int category,
            byte[] fmtAndArglist
        );

        public static void SDL_LogInfo(
            int category,
            string fmtAndArglist
        )
        {
            INTERNAL_SDL_LogInfo(
                category,
                UTF8_ToNative(fmtAndArglist)
            );
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_LogWarn", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_LogWarn(
            int category,
            byte[] fmtAndArglist
        );

        public static void SDL_LogWarn(
            int category,
            string fmtAndArglist
        )
        {
            INTERNAL_SDL_LogWarn(
                category,
                UTF8_ToNative(fmtAndArglist)
            );
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_LogError", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_LogError(
            int category,
            byte[] fmtAndArglist
        );

        public static void SDL_LogError(
            int category,
            string fmtAndArglist
        )
        {
            INTERNAL_SDL_LogError(
                category,
                UTF8_ToNative(fmtAndArglist)
            );
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_LogCritical", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_LogCritical(
            int category,
            byte[] fmtAndArglist
        );

        public static void SDL_LogCritical(
            int category,
            string fmtAndArglist
        )
        {
            INTERNAL_SDL_LogCritical(
                category,
                UTF8_ToNative(fmtAndArglist)
            );
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_LogMessage", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_LogMessage(
            int category,
            SDL_LogPriority priority,
            byte[] fmtAndArglist
        );

        public static void SDL_LogMessage(
            int category,
            SDL_LogPriority priority,
            string fmtAndArglist
        )
        {
            INTERNAL_SDL_LogMessage(
                category,
                priority,
                UTF8_ToNative(fmtAndArglist)
            );
        }

        /* Use string.Format for arglists */
        [DllImport(nativeLibName, EntryPoint = "SDL_LogMessageV", CallingConvention = CallingConvention.Cdecl)]
        private static extern void INTERNAL_SDL_LogMessageV(
            int category,
            SDL_LogPriority priority,
            byte[] fmtAndArglist
        );

        public static void SDL_LogMessageV(
            int category,
            SDL_LogPriority priority,
            string fmtAndArglist
        )
        {
            INTERNAL_SDL_LogMessageV(
                category,
                priority,
                UTF8_ToNative(fmtAndArglist)
            );
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_LogPriority SDL_LogGetPriority(
            int category
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetPriority(
            int category,
            SDL_LogPriority priority
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetAllPriority(
            SDL_LogPriority priority
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogResetPriorities();

        /* userdata refers to a void* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_LogGetOutputFunction(
            out IntPtr callback,
            out IntPtr userdata
        );

        public static void SDL_LogGetOutputFunction(
            out SDL_LogOutputFunction callback,
            out IntPtr userdata
        )
        {
            IntPtr result;
            SDL_LogGetOutputFunction(
                out result,
                out userdata
            );
            if (result != IntPtr.Zero)
            {
                callback = (SDL_LogOutputFunction)Marshal.GetDelegateForFunctionPointer(
                    result,
                    typeof(SDL_LogOutputFunction)
                );
            }
            else
            {
                callback = null;
            }
        }

        /* userdata refers to a void* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LogSetOutputFunction(
            SDL_LogOutputFunction callback,
            IntPtr userdata
        );
    }
}