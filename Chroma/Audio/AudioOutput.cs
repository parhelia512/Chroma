﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Chroma.Audio.Sources;
using Chroma.Diagnostics.Logging;
using Chroma.Natives.SDL;

namespace Chroma.Audio
{
    public class AudioOutput
    {
        private readonly Log _log = LogManager.GetForCurrentAssembly();

        private static AudioOutput _instance;
        internal static AudioOutput Instance => _instance ?? (_instance = new());

        private List<AudioDevice> _devices = new();
        private List<Decoder> _decoders = new();

        private bool _mixerInitialized;
        private bool _backendInitialized;
        private bool _playbackPaused;

        public IReadOnlyList<AudioDevice> Devices => _devices;
        public IReadOnlyList<Decoder> Decoders => _decoders;

        public int Frequency { get; private set; }
        public int SampleCount { get; private set; }

        public event EventHandler<AudioSourceEventArgs> AudioSourceFinished;

        public float MasterVolume
        {
            get => SDL2_nmix.NMIX_GetMasterGain();
            set
            {
                var vol = value;

                if (vol < 0f)
                    vol = 0f;

                if (vol > 2f)
                    vol = 2f;

                SDL2_nmix.NMIX_SetMasterGain(vol);
            }
        }

        public AudioDevice CurrentOutputDevice => _devices.FirstOrDefault(
            x => x.Index == SDL2_nmix.NMIX_GetAudioDevice()
        );

        private AudioOutput()
        {
        }

        public void PauseAll()
        {
            _playbackPaused = !_playbackPaused;
            SDL2_nmix.NMIX_PausePlayback(_playbackPaused);
        }

        public void Open(AudioDevice device = null, int frequency = 44100, int sampleCount = 1024)
        {
            Close();
            EnumerateDevices();

            Frequency = frequency;
            SampleCount = sampleCount;

            if (SDL2_sound.Sound_Init() < 0)
            {
                _log.Error($"Failed to initialize audio backend: {SDL2.SDL_GetError()}");
                return;
            }
            _backendInitialized = true;
            EnumerateDecoders();

            device = device ?? _devices.FirstOrDefault()
                ?? throw new InvalidOperationException("No output devices found.");

            if (SDL2_nmix.NMIX_OpenAudio(device.Name, Frequency, SampleCount) < 0)
            {
                _log.Error($"Failed to initialize audio mixer: {SDL2.SDL_GetError()}");
                return;
            }
            
            _mixerInitialized = true;
            device.Lock(SDL2_nmix.NMIX_GetAudioDevice());
        }

        public void Close()
        {
            var device = SDL2_nmix.NMIX_GetAudioDevice();

            if (_mixerInitialized)
            {
                if (SDL2_nmix.NMIX_CloseAudio() < 0)
                {
                    _log.Error($"Failed to stop the audio mixer: {SDL2.SDL_GetError()}");
                    return;
                }
                _mixerInitialized = false;
            }

            if (_backendInitialized)
            {
                if (SDL2_sound.Sound_Quit() < 0)
                {
                    _log.Error($"Failed to stop the audio backend: {SDL2.SDL_GetError()}");
                    return;
                }
                _backendInitialized = false;
            }

            if (device > 0)
            {
                _devices.First(x => x.OpenIndex == device).Unlock();
            }
        }

        private void EnumerateDevices()
        {
            _devices.Clear();

            var numberOfOutputDevices = SDL2.SDL_GetNumAudioDevices(0);
            for (var i = 0; i < numberOfOutputDevices; i++)
            {
                _devices.Add(new AudioDevice(i, false));
            }
        }

        private void EnumerateDecoders()
        {
            _decoders.Clear();

            unsafe
            {
                var p = (SDL2_sound.Sound_DecoderInfo**)SDL2_sound.Sound_AvailableDecoders();

                if (p == null)
                    return;

                for (var i = 0;; i++)
                {
                    if (p[i] == null)
                        break;

                    var decoder = Marshal.PtrToStructure<SDL2_sound.Sound_DecoderInfo>(new IntPtr(p[i]));
                    var p2 = (byte**)decoder.extensions;

                    var fmts = new List<string>();

                    if (p2 != null)
                    {
                        for (var j = 0;; j++)
                        {
                            var ext = Marshal.PtrToStringAnsi(new IntPtr(p2[j]));

                            if (ext == null)
                                break;

                            fmts.Add(ext);
                        }
                    }

                    _decoders.Add(
                        new Decoder(
                            Marshal.PtrToStringAnsi(p[i]->description),
                            Marshal.PtrToStringUTF8(p[i]->author),
                            Marshal.PtrToStringAnsi(p[i]->url)
                        ) {SupportedFormats = fmts}
                    );
                }
            }
        }

        public void OnAudioSourceFinished(AudioSource s, bool isLooping)
        {
            AudioSourceFinished?.Invoke(
                this,
                new AudioSourceEventArgs(s, isLooping)
            );
        }

        internal void Initialize()
        {
            Open();
        }
    }
}