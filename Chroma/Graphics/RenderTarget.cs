using System;
using System.Drawing;
using Chroma.Diagnostics.Logging;
using Chroma.Natives.SDL;

namespace Chroma.Graphics
{
    public class RenderTarget : Texture
    {
        private Log Log { get; } = LogManager.GetForCurrentAssembly();
        
        public RenderTarget(int width, int height)
            : base(width, height, TextureType.RenderTarget)
        {
            if (Handle == IntPtr.Zero)
            {
                throw new FrameworkException("Failed to create render target handle.", true);
            }
        }

        public RenderTarget(Size size)
            : this(size.Width, size.Height) { }
    }
}