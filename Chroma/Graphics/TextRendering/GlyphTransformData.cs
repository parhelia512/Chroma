using System.Numerics;

namespace Chroma.Graphics.TextRendering
{
    public class GlyphTransformData
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }

        public GlyphTransformData()
            => Clear(Vector2.Zero);

        internal void Clear(Vector2 basePosition)
        {
            Position = basePosition;
            Scale = Vector2.One;
            Origin = Vector2.Zero;
            Color = Color.White;
            Rotation = 0;
        }
    }
}