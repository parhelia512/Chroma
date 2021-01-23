using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Chroma.Graphics.Batching;
using Chroma.Graphics.TextRendering;
using Chroma.Graphics.TextRendering.Bitmap;
using Chroma.Natives.GL;
using Chroma.Natives.SDL;
using Chroma.Windowing;

namespace Chroma.Graphics
{
    public class RenderContext
    {
        private float _lineThickness;

        private Rectangle _scissor = System.Drawing.Rectangle.Empty;

        internal List<BatchInfo> BatchBuffer { get; }

        internal Window Owner { get; }
        internal IntPtr CurrentRenderTarget => TargetStack.Peek();

        internal Stack<IntPtr> TargetStack { get; }

        public float LineThickness
        {
            get => _lineThickness;
            set
            {
                _lineThickness = value;
                Gl.LineWidth(value);
            }
        }

        public Rectangle Scissor
        {
            get => _scissor;
            set
            {
                _scissor = value;
        
                if (_scissor == System.Drawing.Rectangle.Empty)
                {
                    SDL2.SDL_RenderSetClipRect(
                        GraphicsManager.Renderer,
                        IntPtr.Zero
                    );
                }
                else
                {
                    var rect = new SDL2.SDL_Rect
                    {
                        x = (int)_scissor.X,
                        y = (int)_scissor.Y,
                        w = (int)_scissor.Width,
                        h = (int)_scissor.Height
                    };

                    SDL2.SDL_RenderSetClipRect(
                        GraphicsManager.Renderer,
                        ref rect
                    );
                }
            }
        }

        internal RenderContext(Window owner)
        {
            Owner = owner;

            TargetStack = new Stack<IntPtr>();
            BatchBuffer = new List<BatchInfo>();
        }

        public void Clear(Color color)
        {
            SetColor(color);
            SDL2.SDL_RenderClear(GraphicsManager.Renderer);
        }

        public void Line(Vector2 start, Vector2 end, Color color)
        {
            SetColor(color);
            SDL2.SDL_RenderDrawLineF(GraphicsManager.Renderer, start.X, start.Y, end.X, end.Y);
        }

        public void Pixel(Vector2 position, Color color)
        {
            SetColor(color);
            SDL2.SDL_RenderDrawPointF(GraphicsManager.Renderer, position.X, position.Y);
        }

        public void Polyline(List<Point> vertices, Color color, bool closeLoop)
        {
            for (var i = 0; i < vertices.Count; i++)
            {
                if (i + 1 >= vertices.Count)
                    break;

                Line(
                    new Vector2(vertices[i].X, vertices[i].Y),
                    new Vector2(vertices[i + 1].X, vertices[i + 1].Y),
                    color
                );
            }

            if (closeLoop)
            {
                Line(
                    new Vector2(vertices[0].X, vertices[0].Y),
                    new Vector2(vertices[vertices.Count - 1].X, vertices[vertices.Count - 1].Y),
                    color
                );
            }
        }

        public void Rectangle(ShapeMode mode, Vector2 position, float width, float height, Color color)
        {
            var rect = new SDL2.SDL_FRect
            {
                x = position.X,
                y = position.Y,
                w = width,
                h = height
            };

            SetColor(color);
            if (mode == ShapeMode.Stroke)
            {
                SDL2.SDL_RenderDrawRectF(
                    GraphicsManager.Renderer,
                    ref rect
                );
            }
            else if (mode == ShapeMode.Fill)
            {
                SDL2.SDL_RenderFillRectF(
                    GraphicsManager.Renderer,
                    ref rect
                );
            }
        }

        public void Rectangle(ShapeMode mode, Vector2 position, Size size, Color color)
            => Rectangle(mode, position, size.Width, size.Height, color);

        public void Rectangle(ShapeMode mode, Rectangle rectangle, Color color)
            => Rectangle(
                mode,
                new Vector2(rectangle.X, rectangle.Y),
                rectangle.Width,
                rectangle.Height,
                color
            );

        public void DrawTexture(Texture texture, Vector2 position, Vector2 scale, Vector2 origin, float rotation)
        {
            // todo scale
            var srcRect = new SDL2.SDL_Rect
            {
                x = 0,
                y = 0,
                w = texture.Width,
                h = texture.Height
            };
            
            var dstRect = new SDL2.SDL_FRect
            {
                x = position.X,
                y = position.Y,
                w = texture.Width,
                h = texture.Height
            };

            var center = new SDL2.SDL_FPoint
            {
                x = origin.X,
                y = origin.Y,
            };

            SDL2.SDL_RenderCopyExF(
                GraphicsManager.Renderer,
                texture.Handle,
                ref srcRect,
                ref dstRect,
                rotation,
                ref center,
                SDL2.SDL_RendererFlip.SDL_FLIP_NONE
            );
        }

        public void DrawTexture(Texture texture, Vector2 position, Vector2 scale, Vector2 origin, float rotation,
            Rectangle sourceRectangle)
        {
            var srcRect = new SDL2.SDL_Rect()
            {
                x = sourceRectangle.X,
                y = sourceRectangle.Y,
                w = sourceRectangle.Width,
                h = sourceRectangle.Height
            };

            var dstRect = new SDL2.SDL_FRect
            {
                x = position.X,
                y = position.Y,
                w = sourceRectangle.Width,
                h = sourceRectangle.Height
            };

            var center = new SDL2.SDL_FPoint
            {
                x = origin.X,
                y = origin.Y,
            };

            SDL2.SDL_RenderCopyExF(
                GraphicsManager.Renderer,
                texture.Handle,
                ref srcRect,
                ref dstRect,
                rotation,
                ref center,
                SDL2.SDL_RendererFlip.SDL_FLIP_NONE
            );
        }

        public void RenderTo(RenderTarget target, Action drawingLogic)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target),
                    "You can't just draw an image to a null render target...");

            TargetStack.Push(target.Handle);
            SDL2.SDL_SetRenderTarget(
                GraphicsManager.Renderer,
                target.Handle
            );
            
            drawingLogic?.Invoke();

            TargetStack.Pop();

            if (TargetStack.Any())
            {
                var previousTarget = TargetStack.Peek();
                SDL2.SDL_SetRenderTarget(
                    GraphicsManager.Renderer,
                    previousTarget
                );
            }
            else
            {
                SDL2.SDL_SetRenderTarget(
                    GraphicsManager.Renderer,
                    IntPtr.Zero
                );
            }
        }

        public void DrawString(BitmapFont font, string text, Vector2 position,
            BitmapGlyphTransform glyphTransform = null)
        {
            var x = position.X;
            var y = position.Y;

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '\n')
                {
                    x = position.X;
                    y += font.Common.LineHeight + font.Info.Spacing.Y;

                    continue;
                }

                if (!font.HasGlyph(c))
                    continue;

                var glyph = font.Glyphs[c];

                var srcRect = new SDL2.SDL_Rect
                {
                    x = glyph.BitmapX,
                    y = glyph.BitmapY,
                    w = glyph.Width,
                    h = glyph.Height
                };

                var dstRect = new SDL2.SDL_FRect
                {
                    x = position.X,
                    y = position.Y,
                    w = srcRect.w,
                    h = srcRect.h
                };

                var pageTexture = font.Pages[glyph.Page].Texture;

                var kerningAmount = 0f;
                if (i != 0 && font.UseKerning)
                {
                    var kerning = font.GetKerning(text[i - 1], text[i]);

                    if (kerning != null)
                        kerningAmount = kerning.Value.Amount;
                }

                var pos = new Vector2(
                    x + glyph.OffsetX + kerningAmount,
                    y + glyph.OffsetY
                );

                var transform = new GlyphTransformData(pos);

                if (glyphTransform != null)
                    transform = glyphTransform(c, i, pos, glyph);

                pageTexture.ColorMask = transform.Color;
                var center = new SDL2.SDL_FPoint
                {
                    x = transform.Origin.X,
                    y = transform.Origin.Y,
                };

                SDL2.SDL_RenderCopyExF(
                    GraphicsManager.Renderer,
                    pageTexture.Handle,
                    ref srcRect,
                    ref dstRect,
                    transform.Rotation,
                    ref center,
                    SDL2.SDL_RendererFlip.SDL_FLIP_NONE
                );

                pageTexture.ColorMask = Color.White;
                x += glyph.HorizontalAdvance;
            }
        }

        public void DrawString(BitmapFont font, string text, Vector2 position, Color color)
            => DrawString(font, text, position, (_, _, p, _) => new GlyphTransformData(p) {Color = color});

        public void DrawString(string text, Vector2 position,
            TrueTypeFontGlyphTransform perCharTransform = null)
            => DrawString(EmbeddedAssets.DefaultFont, text, position, perCharTransform);

        public void DrawString(string text, Vector2 position, Color color)
            => DrawString(EmbeddedAssets.DefaultFont, text, position,
                (_, _, p, _) => new GlyphTransformData(p) {Color = color});

        public void DrawString(TrueTypeFont font, string text, Vector2 position,
            TrueTypeFontGlyphTransform glyphTransform = null)
        {
            var x = position.X;
            var y = position.Y;

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '\n')
                {
                    x = position.X;
                    y += font.ScaledLineSpacing;

                    continue;
                }

                if (!font.HasGlyph(c))
                    continue;

                var info = font.RenderInfo[c];

                var srcRect = new SDL2.SDL_Rect
                {
                    x = (int)info.Position.X,
                    y = (int)info.Position.Y,
                    w = (int)info.BitmapSize.X,
                    h = (int)info.BitmapSize.Y
                };

                var xPos = x + info.Bearing.X;
                var yPos = y - info.Bearing.Y + font.MaxBearing;

                if (font.UseKerning && i != 0)
                {
                    var kerning = font.GetKerning(text[i - 1], text[i]);
                    xPos += kerning.X;
                }

                var pos = new Vector2(xPos, yPos);
                var transform = new GlyphTransformData(pos);

                if (glyphTransform != null)
                    transform = glyphTransform(c, i, pos, info);
                
                var dstRect = new SDL2.SDL_FRect
                {
                    x = transform.Position.X,
                    y = transform.Position.Y,
                    w = srcRect.w,
                    h = srcRect.h
                };

                var center = new SDL2.SDL_FPoint
                {
                    x = transform.Origin.X,
                    y = transform.Origin.Y,
                };
                
                font.Atlas.ColorMask = transform.Color;

                SDL2.SDL_RenderCopyExF(
                    GraphicsManager.Renderer,
                    font.Atlas.Handle,
                    ref srcRect,
                    ref dstRect,
                    transform.Rotation,
                    ref center,
                    SDL2.SDL_RendererFlip.SDL_FLIP_NONE
                );
                font.Atlas.ColorMask = Color.White;

                x += info.Advance.X;
            }
        }

        public void DrawString(TrueTypeFont font, string text, Vector2 position, Color color)
            => DrawString(font, text, position, (_, _, _, _) => new GlyphTransformData {Color = color});

        public void DrawBatch(DrawOrder order = DrawOrder.BackToFront, bool discardBatchAfterUse = true)
        {
            BatchBuffer.Sort(
                (a, b) => order == DrawOrder.BackToFront
                    ? a.Depth.CompareTo(b.Depth)
                    : b.Depth.CompareTo(a.Depth)
            );

            for (var i = 0; i < BatchBuffer.Count; i++)
                BatchBuffer[i].DrawAction.Invoke();

            if (discardBatchAfterUse)
                BatchBuffer.Clear();
        }

        public void Batch(Action drawAction, int depth)
        {
            if (drawAction == null)
                return;

            BatchBuffer.Add(
                new BatchInfo
                {
                    DrawAction = drawAction,
                    Depth = depth
                }
            );
        }

        public void SetColor(Color color)
        {
            SDL2.SDL_SetRenderDrawColor(
                GraphicsManager.Renderer,
                color.R,
                color.G,
                color.B,
                color.A
            );
        }
    }
}