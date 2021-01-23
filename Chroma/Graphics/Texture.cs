using System;
using System.IO;
using System.Numerics;
using Chroma.Diagnostics.Logging;
using Chroma.MemoryManagement;
using Chroma.Natives.SDL;
using Chroma.Threading;

namespace Chroma.Graphics
{
    public class Texture : DisposableResource
    {
        private readonly Log _log = LogManager.GetForCurrentAssembly();
        private byte[] _pixelData;

        internal IntPtr Handle { get; private set; }
        internal IntPtr SurfaceHandle { get; private set; }

        public PixelFormat Format { get; private set; }
        
        public int Width
        {
            get
            {
                EnsureNotDisposed();

                // todo error handling
                SDL2.SDL_QueryTexture(
                    Handle, out _, out _, out var width, out _
                );

                return width;
            }
        }

        public int Height
        {
            get
            {
                EnsureNotDisposed();

                // todo error handling
                SDL2.SDL_QueryTexture(
                    Handle, out _, out _, out _, out var height
                );

                return height;
            }
        }

        public Vector2 Center
        {
            get
            {
                EnsureNotDisposed();
                return new Vector2(Width / 2f, Height / 2f);
            }
        }

        public int BytesPerPixel { get; private set; }

        public Color ColorMask
        {
            get
            {
                EnsureNotDisposed();

                // todo error handling
                SDL2.SDL_GetTextureColorMod(
                    Handle,
                    out var r,
                    out var g,
                    out var b
                );

                SDL2.SDL_GetTextureAlphaMod(
                    Handle,
                    out var a
                );

                return new Color(r, g, b, a);
            }

            set
            {
                EnsureNotDisposed();

                // todo
                SDL2.SDL_SetTextureColorMod(
                    Handle,
                    value.R,
                    value.G,
                    value.B
                );

                SDL2.SDL_SetTextureAlphaMod(
                    Handle,
                    value.A
                );
            }
        }

        public int Stride => Width * BytesPerPixel;

        public Color this[int x, int y]
        {
            get => GetPixel(x, y);
            set => SetPixel(x, y, value);
        }

        public TextureType Type { get; }

        public Texture(Stream stream)
        {
            EnsureOnMainThread();

            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            Type = TextureType.Static;
            
            unsafe
            {
                fixed (byte* bp = &bytes[0])
                {
                    var rwops = SDL2.SDL_RWFromMem(new IntPtr(bp), bytes.Length);

                    InitializeWithSurface(
                        SDL_image.IMG_Load_RW(rwops, 1)
                    );
                }
            }

            Flush();
        }

        public Texture(string filePath)
            : this(new FileStream(filePath, FileMode.Open))
        {
        }

        public Texture(Texture other)
        {
            EnsureOnMainThread();

            if (other.Disposed)
                throw new InvalidOperationException("The source texture has been disposed.");

            Type = other.Type;

            if (other.SurfaceHandle != IntPtr.Zero)
            {
                SurfaceHandle = SDL2.SDL_DuplicateSurface(other.SurfaceHandle);
            }

            CreateEmpty(
                other.Width,
                other.Height,
                other.Format
            );

            CopyDataFrom(other);
            Flush();
        }

        public Texture(int width, int height, TextureType type = TextureType.Streaming, PixelFormat pixelFormat = PixelFormat.RGBA)
        {
            EnsureOnMainThread();

            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be negative.");

            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be negative.");
            
            Type = type;

            CreateEmpty(
                (ushort)width,
                (ushort)height,
                pixelFormat
            );

            Flush();
        }

        public Texture(int width, int height, byte[] data, TextureType type, PixelFormat pixelFormat = PixelFormat.RGBA)
        {
            EnsureOnMainThread();

            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be negative.");

            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be negative.");

            Type = type;
            
            CreateEmpty(
                (ushort)width,
                (ushort)height,
                pixelFormat
            );

            data.CopyTo(_pixelData, 0);
            Flush();
        }

        public Texture(IntPtr surfaceHandle)
        {
            InitializeWithSurface(surfaceHandle);
            Flush();
        }

        public void SetBlending(BlendingMode mode)
        {
            // todo error handling
            SDL2.SDL_SetTextureBlendMode(Handle, (SDL2.SDL_BlendMode)mode);
        }

        public void SetPixelData(Color[] colors)
        {
            EnsureNotDisposed();

            var pixelCount = Width * Height;

            if (colors.Length != Width * Height)
                throw new InvalidOperationException("The pixel array must be the same size as texture's.");

            for (var i = 0; i < pixelCount; i++)
                WritePixel(i * BytesPerPixel, colors[i]);
        }

        public void SetPixel(int x, int y, Color color)
        {
            EnsureNotDisposed();

            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                _log.Warning($"Tried to set a texture pixel on out-of-bounds coordinates ({x},{y})");
                return;
            }

            var i = y * Stride + (x * BytesPerPixel);
            WritePixel(i, color);
        }

        public Color GetPixel(int x, int y)
        {
            EnsureNotDisposed();

            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                _log.Warning($"Tried to retrieve a texture pixel on out-of-bounds coordinates ({x},{y})");
                return Color.Black;
            }

            var i = y * Stride + (x * BytesPerPixel);
            return ReadPixel(i);
        }

        public void Flush()
        {
            EnsureOnMainThread();
            EnsureNotDisposed();

            if (_pixelData.Length < Width * Height * BytesPerPixel)
            {
                _log.Error("Cannot flush. Pixel data size mismatch.");
                return;
            }

            var imgRect = new SDL2.SDL_Rect
            {
                x = 0,
                y = 0,
                w = Width,
                h = Height
            };

            unsafe
            {
                fixed (byte* data = _pixelData)
                {
                    SDL2.SDL_UpdateTexture(
                        Handle,
                        ref imgRect,
                        new IntPtr(data),
                        Stride
                    );
                }
            }
        }
        
        private unsafe void InitializeWithSurface(IntPtr surfaceHandle)
        {
            SDL2.SDL_Surface* rgbaSurface;

            SurfaceHandle = SDL2.SDL_ConvertSurfaceFormat(
                surfaceHandle,
                SDL2.SDL_PIXFMT_ABGR8888, // endianness? pixel order still confuses me sometimes
                0
            );

            SDL2.SDL_FreeSurface(surfaceHandle);
            rgbaSurface = (SDL2.SDL_Surface*)SurfaceHandle.ToPointer();

            CreateEmpty(
                rgbaSurface->w,
                rgbaSurface->h,
                PixelFormat.RGBA
            );

            var pixels = (byte*)rgbaSurface->pixels.ToPointer();
            var format = (SDL2.SDL_PixelFormat*)rgbaSurface->format.ToPointer();
            var dataLength = rgbaSurface->w * rgbaSurface->h * format->BytesPerPixel;

            for (var i = 0; i < dataLength; i++)
                _pixelData[i] = pixels[i];

            Handle = SDL2.SDL_CreateTextureFromSurface(
                GraphicsManager.Renderer,
                SurfaceHandle
            );
        }

        private void CreateEmpty(int width, int height, PixelFormat format)
        {
            var pixelCount = width * height;
            var bytesPerPixel = 0;

            switch (format)
            {
                case PixelFormat.RGB:
                case PixelFormat.BGR:
                    bytesPerPixel = 3;
                    break;

                case PixelFormat.RGBA:
                case PixelFormat.BGRA:
                case PixelFormat.ABGR:
                case PixelFormat.ARGB:
                    bytesPerPixel = 4;
                    break;
            }

            Format = format;
            BytesPerPixel = bytesPerPixel;

            _pixelData = new byte[pixelCount * bytesPerPixel];
            
            Handle = SDL2.SDL_CreateTexture(
                GraphicsManager.Renderer,
                (uint)format,
                (int)Type,
                width,
                height
            );
        }

        private Color ReadPixel(int i)
        {
            var c = new Color {A = 255};

            switch (Format)
            {
                case PixelFormat.BGR:
                    c.B = _pixelData[i + 0];
                    c.G = _pixelData[i + 1];
                    c.R = _pixelData[i + 2];
                    break;

                case PixelFormat.RGB:
                    c.R = _pixelData[i + 0];
                    c.G = _pixelData[i + 1];
                    c.B = _pixelData[i + 2];
                    break;

                case PixelFormat.ABGR:
                    c.A = _pixelData[i + 0];
                    c.B = _pixelData[i + 1];
                    c.G = _pixelData[i + 2];
                    c.R = _pixelData[i + 3];
                    break;

                case PixelFormat.BGRA:
                    c.B = _pixelData[i + 0];
                    c.G = _pixelData[i + 1];
                    c.R = _pixelData[i + 2];
                    c.A = _pixelData[i + 3];
                    break;

                case PixelFormat.RGBA:
                    c.R = _pixelData[i + 3];
                    c.G = _pixelData[i + 2];
                    c.B = _pixelData[i + 1];
                    c.A = _pixelData[i + 0];
                    break;
                
                case PixelFormat.ARGB:
                    c.A = _pixelData[i + 0];
                    c.R = _pixelData[i + 1];
                    c.G = _pixelData[i + 2];
                    c.B = _pixelData[i + 3];
                    break;

                default: throw new InvalidOperationException("Unsupported pixel format.");
            }

            return c;
        }

        private void WritePixel(int i, Color c)
        {
            switch (Format)
            {
                case PixelFormat.BGR:
                    _pixelData[i + 0] = c.B;
                    _pixelData[i + 1] = c.G;
                    _pixelData[i + 2] = c.R;
                    break;

                case PixelFormat.RGB:
                    _pixelData[i + 0] = c.R;
                    _pixelData[i + 1] = c.G;
                    _pixelData[i + 2] = c.B;
                    break;

                case PixelFormat.ABGR:
                    _pixelData[i + 0] = c.A;
                    _pixelData[i + 1] = c.B;
                    _pixelData[i + 2] = c.G;
                    _pixelData[i + 3] = c.R;
                    break;

                case PixelFormat.BGRA:
                    _pixelData[i + 0] = c.B;
                    _pixelData[i + 1] = c.G;
                    _pixelData[i + 2] = c.R;
                    _pixelData[i + 3] = c.A;
                    break;

                case PixelFormat.RGBA:
                    _pixelData[i + 3] = c.R;
                    _pixelData[i + 2] = c.G;
                    _pixelData[i + 1] = c.B;
                    _pixelData[i + 0] = c.A;
                    break;
                
                case PixelFormat.ARGB:
                    _pixelData[i + 0] = c.A;
                    _pixelData[i + 1] = c.R;
                    _pixelData[i + 2] = c.G;
                    _pixelData[i + 3] = c.B;
                    break;

                default: throw new InvalidOperationException("Unsupported pixel format.");
            }
        }

        private void CopyDataFrom(Texture other)
            => other._pixelData.CopyTo(_pixelData, 0);

        private void EnsureOnMainThread()
        {
            if (!Dispatcher.IsMainThread)
                throw new InvalidOperationException(
                    "This operation is not thread-safe and must be scheduled to run on main thread.");
        }

        protected override void FreeNativeResources()
        {
            if (SurfaceHandle != IntPtr.Zero)
            {
                SDL2.SDL_FreeSurface(SurfaceHandle);
            }
            
            SDL2.SDL_DestroyTexture(Handle);
        }
    }
}