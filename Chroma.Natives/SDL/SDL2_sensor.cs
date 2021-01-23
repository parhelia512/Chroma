using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        /* This region is only available in 2.0.9 or higher. */

        public enum SDL_SensorType
        {
            SDL_SENSOR_INVALID = -1,
            SDL_SENSOR_UNKNOWN,
            SDL_SENSOR_ACCEL,
            SDL_SENSOR_GYRO
        }

        public const float SDL_STANDARD_GRAVITY = 9.80665f;

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumSensors();

        [DllImport(nativeLibName, EntryPoint = "SDL_SensorGetDeviceName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_SensorGetDeviceName(int device_index);

        public static string SDL_SensorGetDeviceName(int device_index)
        {
            return UTF8_ToManaged(INTERNAL_SDL_SensorGetDeviceName(device_index));
        }

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_SensorType SDL_SensorGetDeviceType(int device_index);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetDeviceNonPortableType(int device_index);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetDeviceInstanceID(int device_index);

        /* IntPtr refers to an SDL_Sensor* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_SensorOpen(int device_index);

        /* IntPtr refers to an SDL_Sensor* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_SensorFromInstanceID(
            int instance_id
        );

        /* sensor refers to an SDL_Sensor* */
        [DllImport(nativeLibName, EntryPoint = "SDL_SensorGetName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_SensorGetName(IntPtr sensor);

        public static string SDL_SensorGetName(IntPtr sensor)
        {
            return UTF8_ToManaged(INTERNAL_SDL_SensorGetName(sensor));
        }

        /* sensor refers to an SDL_Sensor* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern SDL_SensorType SDL_SensorGetType(IntPtr sensor);

        /* sensor refers to an SDL_Sensor* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetNonPortableType(IntPtr sensor);

        /* sensor refers to an SDL_Sensor* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetInstanceID(IntPtr sensor);

        /* sensor refers to an SDL_Sensor* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_SensorGetData(
            IntPtr sensor,
            float[] data,
            int num_values
        );

        /* sensor refers to an SDL_Sensor* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SensorClose(IntPtr sensor);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_SensorUpdate();
    }
}