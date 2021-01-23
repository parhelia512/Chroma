using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        public const ushort SDL_AUDIO_MASK_BITSIZE = 0xFF;
        public const ushort SDL_AUDIO_MASK_DATATYPE = 1 << 8;
        public const ushort SDL_AUDIO_MASK_ENDIAN = 1 << 12;
        public const ushort SDL_AUDIO_MASK_SIGNED = 1 << 15;

        public static ushort SDL_AUDIO_BITSIZE(ushort x)
        {
            return (ushort)(x & SDL_AUDIO_MASK_BITSIZE);
        }

        public static bool SDL_AUDIO_ISFLOAT(ushort x)
        {
            return (x & SDL_AUDIO_MASK_DATATYPE) != 0;
        }

        public static bool SDL_AUDIO_ISBIGENDIAN(ushort x)
        {
            return (x & SDL_AUDIO_MASK_ENDIAN) != 0;
        }

        public static bool SDL_AUDIO_ISSIGNED(ushort x)
        {
            return (x & SDL_AUDIO_MASK_SIGNED) != 0;
        }

        public static bool SDL_AUDIO_ISINT(ushort x)
        {
            return (x & SDL_AUDIO_MASK_DATATYPE) == 0;
        }

        public static bool SDL_AUDIO_ISLITTLEENDIAN(ushort x)
        {
            return (x & SDL_AUDIO_MASK_ENDIAN) == 0;
        }

        public static bool SDL_AUDIO_ISUNSIGNED(ushort x)
        {
            return (x & SDL_AUDIO_MASK_SIGNED) == 0;
        }

        public const ushort AUDIO_U8 = 0x0008;
        public const ushort AUDIO_S8 = 0x8008;
        public const ushort AUDIO_U16LSB = 0x0010;
        public const ushort AUDIO_S16LSB = 0x8010;
        public const ushort AUDIO_U16MSB = 0x1010;
        public const ushort AUDIO_S16MSB = 0x9010;
        public const ushort AUDIO_U16 = AUDIO_U16LSB;
        public const ushort AUDIO_S16 = AUDIO_S16LSB;
        public const ushort AUDIO_S32LSB = 0x8020;
        public const ushort AUDIO_S32MSB = 0x9020;
        public const ushort AUDIO_S32 = AUDIO_S32LSB;
        public const ushort AUDIO_F32LSB = 0x8120;
        public const ushort AUDIO_F32MSB = 0x9120;
        public const ushort AUDIO_F32 = AUDIO_F32LSB;

        public static readonly ushort AUDIO_U16SYS =
            BitConverter.IsLittleEndian ? AUDIO_U16LSB : AUDIO_U16MSB;

        public static readonly ushort AUDIO_S16SYS =
            BitConverter.IsLittleEndian ? AUDIO_S16LSB : AUDIO_S16MSB;

        public static readonly ushort AUDIO_S32SYS =
            BitConverter.IsLittleEndian ? AUDIO_S32LSB : AUDIO_S32MSB;

        public static readonly ushort AUDIO_F32SYS =
            BitConverter.IsLittleEndian ? AUDIO_F32LSB : AUDIO_F32MSB;

        public const uint SDL_AUDIO_ALLOW_FREQUENCY_CHANGE = 0x00000001;
        public const uint SDL_AUDIO_ALLOW_FORMAT_CHANGE = 0x00000002;
        public const uint SDL_AUDIO_ALLOW_CHANNELS_CHANGE = 0x00000004;
        public const uint SDL_AUDIO_ALLOW_SAMPLES_CHANGE = 0x00000008;

        public const uint SDL_AUDIO_ALLOW_ANY_CHANGE =
            SDL_AUDIO_ALLOW_FREQUENCY_CHANGE |
            SDL_AUDIO_ALLOW_FORMAT_CHANGE |
            SDL_AUDIO_ALLOW_CHANNELS_CHANGE |
            SDL_AUDIO_ALLOW_SAMPLES_CHANGE;

        public const int SDL_MIX_MAXVOLUME = 128;

        public enum SDL_AudioStatus
        {
            SDL_AUDIO_STOPPED,
            SDL_AUDIO_PLAYING,
            SDL_AUDIO_PAUSED
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_AudioSpec
        {
            public int freq;
            public ushort format; // SDL_AudioFormat
            public byte channels;
            public byte silence;
            public ushort samples;
            public uint size;
            public IntPtr callback;
            public IntPtr userdata; // void*
        }

        /* userdata refers to a void*, stream to a Uint8 */
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SDL_AudioCallback(
            IntPtr userdata,
            IntPtr stream,
            int len
        );

        [DllImport(nativeLibName, EntryPoint = "SDL_AudioInit", CallingConvention = CallingConvention.Cdecl)]
        private static extern int INTERNAL_SDL_AudioInit(
            byte[] driver_name
        );

        public static int SDL_AudioInit(string driver_name)
        {
            return INTERNAL_SDL_AudioInit(
                UTF8_ToNative(driver_name)
            );
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AudioQuit();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_CloseAudio();

        /* dev refers to an SDL_AudioDeviceID */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_CloseAudioDevice(uint dev);

        /* audio_buf refers to a malloc()'d buffer from SDL_LoadWAV */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeWAV(IntPtr audio_buf);

        [DllImport(nativeLibName, EntryPoint = "SDL_GetAudioDeviceName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetAudioDeviceName(
            int index,
            bool iscapture
        );

        public static string SDL_GetAudioDeviceName(
            int index,
            bool iscapture
        )
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GetAudioDeviceName(index, iscapture)
            );
        }

        /* dev refers to an SDL_AudioDeviceID */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_AudioStatus SDL_GetAudioDeviceStatus(
            uint dev
        );

        [DllImport(nativeLibName, EntryPoint = "SDL_GetAudioDriver", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetAudioDriver(int index);

        public static string SDL_GetAudioDriver(int index)
        {
            return UTF8_ToManaged(
                INTERNAL_SDL_GetAudioDriver(index)
            );
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_AudioStatus SDL_GetAudioStatus();

        [DllImport(nativeLibName, EntryPoint = "SDL_GetCurrentAudioDriver",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_GetCurrentAudioDriver();

        public static string SDL_GetCurrentAudioDriver()
        {
            return UTF8_ToManaged(INTERNAL_SDL_GetCurrentAudioDriver());
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumAudioDevices(int iscapture);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_GetNumAudioDrivers();

        /* audio_buf will refer to a malloc()'d byte buffer */
        /* THIS IS AN RWops FUNCTION! */
        [DllImport(nativeLibName, EntryPoint = "SDL_LoadWAV_RW", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_LoadWAV_RW(
            IntPtr src,
            int freesrc,
            ref SDL_AudioSpec spec,
            out IntPtr audio_buf,
            out uint audio_len
        );

        public static SDL_AudioSpec SDL_LoadWAV(
            string file,
            ref SDL_AudioSpec spec,
            out IntPtr audio_buf,
            out uint audio_len
        )
        {
            SDL_AudioSpec result;
            IntPtr rwops = SDL_RWFromFile(file, "rb");
            IntPtr result_ptr = INTERNAL_SDL_LoadWAV_RW(
                rwops,
                1,
                ref spec,
                out audio_buf,
                out audio_len
            );
            result = (SDL_AudioSpec)Marshal.PtrToStructure(
                result_ptr,
                typeof(SDL_AudioSpec)
            );
            return result;
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LockAudio();

        /* dev refers to an SDL_AudioDeviceID */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_LockAudioDevice(uint dev);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_MixAudio(
            [Out()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)]
            byte[] dst,
            [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)]
            byte[] src,
            uint len,
            int volume
        );

        /* format refers to an SDL_AudioFormat */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_MixAudioFormat(
            [Out()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 3)]
            byte[] dst,
            [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 3)]
            byte[] src,
            ushort format,
            uint len,
            int volume
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_OpenAudio(
            ref SDL_AudioSpec desired,
            out SDL_AudioSpec obtained
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_OpenAudio(
            ref SDL_AudioSpec desired,
            IntPtr obtained
        );

        /* uint refers to an SDL_AudioDeviceID */
        [DllImport(nativeLibName, EntryPoint = "SDL_OpenAudioDevice", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint INTERNAL_SDL_OpenAudioDevice(
            byte[] device,
            int iscapture,
            ref SDL_AudioSpec desired,
            out SDL_AudioSpec obtained,
            int allowed_changes
        );

        public static uint SDL_OpenAudioDevice(
            string device,
            int iscapture,
            ref SDL_AudioSpec desired,
            out SDL_AudioSpec obtained,
            int allowed_changes
        )
        {
            return INTERNAL_SDL_OpenAudioDevice(
                UTF8_ToNative(device),
                iscapture,
                ref desired,
                out obtained,
                allowed_changes
            );
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_PauseAudio(int pause_on);

        /* dev refers to an SDL_AudioDeviceID */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_PauseAudioDevice(
            uint dev,
            int pause_on
        );

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockAudio();

        /* dev refers to an SDL_AudioDeviceID */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_UnlockAudioDevice(uint dev);

        /* dev refers to an SDL_AudioDeviceID, data to a void*
         * Only available in 2.0.4 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_QueueAudio(
            uint dev,
            IntPtr data,
            uint len
        );

        /* dev refers to an SDL_AudioDeviceID, data to a void*
         * Only available in 2.0.5 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_DequeueAudio(
            uint dev,
            IntPtr data,
            uint len
        );

        /* dev refers to an SDL_AudioDeviceID
         * Only available in 2.0.4 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_GetQueuedAudioSize(uint dev);

        /* dev refers to an SDL_AudioDeviceID
         * Only available in 2.0.4 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_ClearQueuedAudio(uint dev);

        /* src_format and dst_format refer to SDL_AudioFormats.
         * IntPtr refers to an SDL_AudioStream*.
         * Only available in 2.0.7 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_NewAudioStream(
            ushort src_format,
            byte src_channels,
            int src_rate,
            ushort dst_format,
            byte dst_channels,
            int dst_rate
        );

        /* stream refers to an SDL_AudioStream*, buf to a void*.
         * Only available in 2.0.7 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamPut(
            IntPtr stream,
            IntPtr buf,
            int len
        );

        /* stream refers to an SDL_AudioStream*, buf to a void*.
         * Only available in 2.0.7 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamGet(
            IntPtr stream,
            IntPtr buf,
            int len
        );

        /* stream refers to an SDL_AudioStream*.
         * Only available in 2.0.7 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_AudioStreamAvailable(IntPtr stream);

        /* stream refers to an SDL_AudioStream*.
         * Only available in 2.0.7 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_AudioStreamClear(IntPtr stream);

        /* stream refers to an SDL_AudioStream*.
         * Only available in 2.0.7 or higher.
         */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_FreeAudioStream(IntPtr stream);
    }
}