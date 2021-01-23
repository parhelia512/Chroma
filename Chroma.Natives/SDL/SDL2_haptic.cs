using System;
using System.Runtime.InteropServices;

namespace Chroma.Natives.SDL
{
    internal static partial class SDL2
    {
        /* SDL_HapticEffect type */
        public const ushort SDL_HAPTIC_CONSTANT = 1 << 0;
        public const ushort SDL_HAPTIC_SINE = 1 << 1;
        public const ushort SDL_HAPTIC_LEFTRIGHT = 1 << 2;
        public const ushort SDL_HAPTIC_TRIANGLE = 1 << 3;
        public const ushort SDL_HAPTIC_SAWTOOTHUP = 1 << 4;
        public const ushort SDL_HAPTIC_SAWTOOTHDOWN = 1 << 5;
        public const ushort SDL_HAPTIC_SPRING = 1 << 7;
        public const ushort SDL_HAPTIC_DAMPER = 1 << 8;
        public const ushort SDL_HAPTIC_INERTIA = 1 << 9;
        public const ushort SDL_HAPTIC_FRICTION = 1 << 10;
        public const ushort SDL_HAPTIC_CUSTOM = 1 << 11;
        public const ushort SDL_HAPTIC_GAIN = 1 << 12;
        public const ushort SDL_HAPTIC_AUTOCENTER = 1 << 13;
        public const ushort SDL_HAPTIC_STATUS = 1 << 14;
        public const ushort SDL_HAPTIC_PAUSE = 1 << 15;

        /* SDL_HapticDirection type */
        public const byte SDL_HAPTIC_POLAR = 0;
        public const byte SDL_HAPTIC_CARTESIAN = 1;
        public const byte SDL_HAPTIC_SPHERICAL = 2;

        /* SDL_HapticRunEffect */
        public const uint SDL_HAPTIC_INFINITY = 4294967295U;

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SDL_HapticDirection
        {
            public byte type;
            public fixed int dir[3];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_HapticConstant
        {
            // Header
            public ushort type;

            public SDL_HapticDirection direction;

            // Replay
            public uint length;

            public ushort delay;

            // Trigger
            public ushort button;

            public ushort interval;

            // Constant
            public short level;

            // Envelope
            public ushort attack_length;
            public ushort attack_level;
            public ushort fade_length;
            public ushort fade_level;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_HapticPeriodic
        {
            // Header
            public ushort type;

            public SDL_HapticDirection direction;

            // Replay
            public uint length;

            public ushort delay;

            // Trigger
            public ushort button;

            public ushort interval;

            // Periodic
            public ushort period;
            public short magnitude;
            public short offset;

            public ushort phase;

            // Envelope
            public ushort attack_length;
            public ushort attack_level;
            public ushort fade_length;
            public ushort fade_level;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SDL_HapticCondition
        {
            // Header
            public ushort type;

            public SDL_HapticDirection direction;

            // Replay
            public uint length;

            public ushort delay;

            // Trigger
            public ushort button;

            public ushort interval;

            // Condition
            public fixed ushort right_sat[3];
            public fixed ushort left_sat[3];
            public fixed short right_coeff[3];
            public fixed short left_coeff[3];
            public fixed ushort deadband[3];
            public fixed short center[3];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_HapticRamp
        {
            // Header
            public ushort type;

            public SDL_HapticDirection direction;

            // Replay
            public uint length;

            public ushort delay;

            // Trigger
            public ushort button;

            public ushort interval;

            // Ramp
            public short start;

            public short end;

            // Envelope
            public ushort attack_length;
            public ushort attack_level;
            public ushort fade_length;
            public ushort fade_level;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_HapticLeftRight
        {
            // Header
            public ushort type;

            // Replay
            public uint length;

            // Rumble
            public ushort large_magnitude;
            public ushort small_magnitude;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_HapticCustom
        {
            // Header
            public ushort type;

            public SDL_HapticDirection direction;

            // Replay
            public uint length;

            public ushort delay;

            // Trigger
            public ushort button;

            public ushort interval;

            // Custom
            public byte channels;
            public ushort period;
            public ushort samples;

            public IntPtr data; // Uint16*

            // Envelope
            public ushort attack_length;
            public ushort attack_level;
            public ushort fade_length;
            public ushort fade_level;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_HapticEffect
        {
            [FieldOffset(0)] public ushort type;
            [FieldOffset(0)] public SDL_HapticConstant constant;
            [FieldOffset(0)] public SDL_HapticPeriodic periodic;
            [FieldOffset(0)] public SDL_HapticCondition condition;
            [FieldOffset(0)] public SDL_HapticRamp ramp;
            [FieldOffset(0)] public SDL_HapticLeftRight leftright;
            [FieldOffset(0)] public SDL_HapticCustom custom;
        }

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_HapticClose(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SDL_HapticDestroyEffect(
            IntPtr haptic,
            int effect
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticEffectSupported(
            IntPtr haptic,
            ref SDL_HapticEffect effect
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticGetEffectStatus(
            IntPtr haptic,
            int effect
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticIndex(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, EntryPoint = "SDL_HapticName", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr INTERNAL_SDL_HapticName(int device_index);

        public static string SDL_HapticName(int device_index)
        {
            return UTF8_ToManaged(INTERNAL_SDL_HapticName(device_index));
        }

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNewEffect(
            IntPtr haptic,
            ref SDL_HapticEffect effect
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumAxes(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumEffects(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticNumEffectsPlaying(IntPtr haptic);

        /* IntPtr refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_HapticOpen(int device_index);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticOpened(int device_index);

        /* IntPtr refers to an SDL_Haptic*, joystick to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_HapticOpenFromJoystick(
            IntPtr joystick
        );

        /* IntPtr refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SDL_HapticOpenFromMouse();

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticPause(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SDL_HapticQuery(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRumbleInit(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRumblePlay(
            IntPtr haptic,
            float strength,
            uint length
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRumbleStop(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRumbleSupported(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticRunEffect(
            IntPtr haptic,
            int effect,
            uint iterations
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticSetAutocenter(
            IntPtr haptic,
            int autocenter
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticSetGain(
            IntPtr haptic,
            int gain
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticStopAll(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticStopEffect(
            IntPtr haptic,
            int effect
        );

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticUnpause(IntPtr haptic);

        /* haptic refers to an SDL_Haptic* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_HapticUpdateEffect(
            IntPtr haptic,
            int effect,
            ref SDL_HapticEffect data
        );

        /* joystick refers to an SDL_Joystick* */
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_JoystickIsHaptic(IntPtr joystick);

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_MouseIsHaptic();

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SDL_NumHaptics();
    }
}