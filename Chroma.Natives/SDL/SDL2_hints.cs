using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public const string SDL_HINT_FRAMEBUFFER_ACCELERATION =
            "SDL_FRAMEBUFFER_ACCELERATION";

        public const string SDL_HINT_RENDER_DRIVER =
            "SDL_RENDER_DRIVER";

        public const string SDL_HINT_RENDER_OPENGL_SHADERS =
            "SDL_RENDER_OPENGL_SHADERS";

        public const string SDL_HINT_RENDER_DIRECT3D_THREADSAFE =
            "SDL_RENDER_DIRECT3D_THREADSAFE";

        public const string SDL_HINT_RENDER_VSYNC =
            "SDL_RENDER_VSYNC";

        public const string SDL_HINT_VIDEO_X11_XVIDMODE =
            "SDL_VIDEO_X11_XVIDMODE";

        public const string SDL_HINT_VIDEO_X11_XINERAMA =
            "SDL_VIDEO_X11_XINERAMA";

        public const string SDL_HINT_VIDEO_X11_XRANDR =
            "SDL_VIDEO_X11_XRANDR";

        public const string SDL_HINT_GRAB_KEYBOARD =
            "SDL_GRAB_KEYBOARD";

        public const string SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS =
            "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";

        public const string SDL_HINT_IDLE_TIMER_DISABLED =
            "SDL_IOS_IDLE_TIMER_DISABLED";

        public const string SDL_HINT_ORIENTATIONS =
            "SDL_IOS_ORIENTATIONS";

        public const string SDL_HINT_XINPUT_ENABLED =
            "SDL_XINPUT_ENABLED";

        public const string SDL_HINT_GAMECONTROLLERCONFIG =
            "SDL_GAMECONTROLLERCONFIG";

        public const string SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS =
            "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";

        public const string SDL_HINT_ALLOW_TOPMOST =
            "SDL_ALLOW_TOPMOST";

        public const string SDL_HINT_TIMER_RESOLUTION =
            "SDL_TIMER_RESOLUTION";

        public const string SDL_HINT_RENDER_SCALE_QUALITY =
            "SDL_RENDER_SCALE_QUALITY";

        /* Only available in SDL 2.0.1 or higher. */
        public const string SDL_HINT_VIDEO_HIGHDPI_DISABLED =
            "SDL_VIDEO_HIGHDPI_DISABLED";

        /* Only available in SDL 2.0.2 or higher. */
        public const string SDL_HINT_CTRL_CLICK_EMULATE_RIGHT_CLICK =
            "SDL_CTRL_CLICK_EMULATE_RIGHT_CLICK";

        public const string SDL_HINT_VIDEO_WIN_D3DCOMPILER =
            "SDL_VIDEO_WIN_D3DCOMPILER";

        public const string SDL_HINT_MOUSE_RELATIVE_MODE_WARP =
            "SDL_MOUSE_RELATIVE_MODE_WARP";

        public const string SDL_HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT =
            "SDL_VIDEO_WINDOW_SHARE_PIXEL_FORMAT";

        public const string SDL_HINT_VIDEO_ALLOW_SCREENSAVER =
            "SDL_VIDEO_ALLOW_SCREENSAVER";

        public const string SDL_HINT_ACCELEROMETER_AS_JOYSTICK =
            "SDL_ACCELEROMETER_AS_JOYSTICK";

        public const string SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES =
            "SDL_VIDEO_MAC_FULLSCREEN_SPACES";

        /* Only available in SDL 2.0.3 or higher. */
        public const string SDL_HINT_WINRT_PRIVACY_POLICY_URL =
            "SDL_WINRT_PRIVACY_POLICY_URL";

        public const string SDL_HINT_WINRT_PRIVACY_POLICY_LABEL =
            "SDL_WINRT_PRIVACY_POLICY_LABEL";

        public const string SDL_HINT_WINRT_HANDLE_BACK_BUTTON =
            "SDL_WINRT_HANDLE_BACK_BUTTON";

        /* Only available in SDL 2.0.4 or higher. */
        public const string SDL_HINT_NO_SIGNAL_HANDLERS =
            "SDL_NO_SIGNAL_HANDLERS";

        public const string SDL_HINT_IME_INTERNAL_EDITING =
            "SDL_IME_INTERNAL_EDITING";

        public const string SDL_HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH =
            "SDL_ANDROID_SEPARATE_MOUSE_AND_TOUCH";

        public const string SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT =
            "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";

        public const string SDL_HINT_THREAD_STACK_SIZE =
            "SDL_THREAD_STACK_SIZE";

        public const string SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN =
            "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";

        public const string SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP =
            "SDL_WINDOWS_ENABLE_MESSAGELOOP";

        public const string SDL_HINT_WINDOWS_NO_CLOSE_ON_ALT_F4 =
            "SDL_WINDOWS_NO_CLOSE_ON_ALT_F4";

        public const string SDL_HINT_XINPUT_USE_OLD_JOYSTICK_MAPPING =
            "SDL_XINPUT_USE_OLD_JOYSTICK_MAPPING";

        public const string SDL_HINT_MAC_BACKGROUND_APP =
            "SDL_MAC_BACKGROUND_APP";

        public const string SDL_HINT_VIDEO_X11_NET_WM_PING =
            "SDL_VIDEO_X11_NET_WM_PING";

        public const string SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION =
            "SDL_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION";

        public const string SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION =
            "SDL_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION";

        /* Only available in 2.0.5 or higher. */
        public const string SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH =
            "SDL_MOUSE_FOCUS_CLICKTHROUGH";

        public const string SDL_HINT_BMP_SAVE_LEGACY_FORMAT =
            "SDL_BMP_SAVE_LEGACY_FORMAT";

        public const string SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING =
            "SDL_WINDOWS_DISABLE_THREAD_NAMING";

        public const string SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION =
            "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";

        /* Only available in 2.0.6 or higher. */
        public const string SDL_HINT_AUDIO_RESAMPLING_MODE =
            "SDL_AUDIO_RESAMPLING_MODE";

        public const string SDL_HINT_RENDER_LOGICAL_SIZE_MODE =
            "SDL_RENDER_LOGICAL_SIZE_MODE";

        public const string SDL_HINT_MOUSE_NORMAL_SPEED_SCALE =
            "SDL_MOUSE_NORMAL_SPEED_SCALE";

        public const string SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE =
            "SDL_MOUSE_RELATIVE_SPEED_SCALE";

        public const string SDL_HINT_TOUCH_MOUSE_EVENTS =
            "SDL_TOUCH_MOUSE_EVENTS";

        public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON =
            "SDL_WINDOWS_INTRESOURCE_ICON";

        public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL =
            "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";

        /* Only available in 2.0.8 or higher. */
        public const string SDL_HINT_IOS_HIDE_HOME_INDICATOR =
            "SDL_IOS_HIDE_HOME_INDICATOR";

        public const string SDL_HINT_TV_REMOTE_AS_JOYSTICK =
            "SDL_TV_REMOTE_AS_JOYSTICK";

        /* Only available in 2.0.9 or higher. */
        public const string SDL_HINT_MOUSE_DOUBLE_CLICK_TIME =
            "SDL_MOUSE_DOUBLE_CLICK_TIME";

        public const string SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS =
            "SDL_MOUSE_DOUBLE_CLICK_RADIUS";

        public const string SDL_HINT_JOYSTICK_HIDAPI =
            "SDL_JOYSTICK_HIDAPI";

        public const string SDL_HINT_JOYSTICK_HIDAPI_PS4 =
            "SDL_JOYSTICK_HIDAPI_PS4";

        public const string SDL_HINT_JOYSTICK_HIDAPI_PS4_RUMBLE =
            "SDL_JOYSTICK_HIDAPI_PS4_RUMBLE";

        public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM =
            "SDL_JOYSTICK_HIDAPI_STEAM";

        public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH =
            "SDL_JOYSTICK_HIDAPI_SWITCH";

        public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX =
            "SDL_JOYSTICK_HIDAPI_XBOX";

        public const string SDL_HINT_ENABLE_STEAM_CONTROLLERS =
            "SDL_ENABLE_STEAM_CONTROLLERS";

        public const string SDL_HINT_ANDROID_TRAP_BACK_BUTTON =
            "SDL_ANDROID_TRAP_BACK_BUTTON";

        /* Only available in 2.0.10 or higher. */
        public const string SDL_HINT_MOUSE_TOUCH_EVENTS =
            "SDL_MOUSE_TOUCH_EVENTS";

        public const string SDL_HINT_GAMECONTROLLERCONFIG_FILE =
            "SDL_GAMECONTROLLERCONFIG_FILE";

        public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE =
            "SDL_ANDROID_BLOCK_ON_PAUSE";

        public const string SDL_HINT_RENDER_BATCHING =
            "SDL_RENDER_BATCHING";

        public const string SDL_HINT_EVENT_LOGGING =
            "SDL_EVENT_LOGGING";

        public const string SDL_HINT_WAVE_RIFF_CHUNK_SIZE =
            "SDL_WAVE_RIFF_CHUNK_SIZE";

        public const string SDL_HINT_WAVE_TRUNCATION =
            "SDL_WAVE_TRUNCATION";

        public const string SDL_HINT_WAVE_FACT_CHUNK =
            "SDL_WAVE_FACT_CHUNK";

        /* Only available in 2.0.11 or higher. */
        public const string SDL_HINT_VIDO_X11_WINDOW_VISUALID =
            "SDL_VIDEO_X11_WINDOW_VISUALID";

        public const string SDL_HINT_GAMECONTROLLER_USE_BUTTON_LABELS =
            "SDL_GAMECONTROLLER_USE_BUTTON_LABELS";

        public const string SDL_HINT_VIDEO_EXTERNAL_CONTEXT =
            "SDL_VIDEO_EXTERNAL_CONTEXT";

        public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE =
            "SDL_JOYSTICK_HIDAPI_GAMECUBE";

        public const string SDL_HINT_DISPLAY_USABLE_BOUNDS =
            "SDL_DISPLAY_USABLE_BOUNDS";

        public const string SDL_HINT_VIDEO_X11_FORCE_EGL =
            "SDL_VIDEO_X11_FORCE_EGL";

        public const string SDL_HINT_GAMECONTROLLERTYPE =
            "SDL_GAMECONTROLLERTYPE";

        public enum SDL_HintPriority
        {
            SDL_HINT_DEFAULT,
            SDL_HINT_NORMAL,
            SDL_HINT_OVERRIDE
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearHints();

        [DllImport(nativeLibName, EntryPoint = "SDL_GetHint", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetHint(byte[] name);

        public static string SDL_GetHint(string name)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GetHint(
                    UTF8_ToNative(name)
                )
            );
        }

        [DllImport(nativeLibName, EntryPoint = "SDL_SetHint", CallingConvention = CallingConvention.Cdecl)]
        private static extern SDL_bool INTERNAL_SDL_SetHint(
            byte[] name,
            byte[] value
        );

        public static SDL_bool SDL_SetHint(string name, string value)
        {
            return INTERNAL_SDL_SetHint(
                UTF8_ToNative(name),
                UTF8_ToNative(value)
            );
        }

        [DllImport(nativeLibName, EntryPoint = "SDL_SetHintWithPriority", CallingConvention = CallingConvention.Cdecl)]
        private static extern SDL_bool INTERNAL_SDL_SetHintWithPriority(
            byte[] name,
            byte[] value,
            SDL_HintPriority priority
        );

        public static SDL_bool SDL_SetHintWithPriority(
            string name,
            string value,
            SDL_HintPriority priority
        )
        {
            return INTERNAL_SDL_SetHintWithPriority(
                UTF8_ToNative(name),
                UTF8_ToNative(value),
                priority
            );
        }

        /* Only available in 2.0.5 or higher. */
        [DllImport(nativeLibName, EntryPoint = "SDL_GetHintBoolean", CallingConvention = CallingConvention.Cdecl)]
        private static extern SDL_bool INTERNAL_SDL_GetHintBoolean(
            byte[] name,
            SDL_bool default_value
        );

        public static SDL_bool SDL_GetHintBoolean(
            string name,
            SDL_bool default_value
        )
        {
            return INTERNAL_SDL_GetHintBoolean(
                UTF8_ToNative(name),
                default_value
            );
        }
    }
}