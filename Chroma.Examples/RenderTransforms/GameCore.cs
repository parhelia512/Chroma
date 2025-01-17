using System;
using System.IO;
using System.Numerics;
using Chroma;
using Chroma.ContentManagement;
using Chroma.ContentManagement.FileSystem;
using Chroma.Graphics;

namespace RenderTransforms
{
    public class GameCore : Game
    {
        private Texture _burger;
        private RenderTarget _rt;
        
        public GameCore() : base(new(false, false))
        {
        }

        protected override IContentProvider InitializeContentPipeline()
        {
            return new FileSystemContentProvider(
                Path.Combine(AppContext.BaseDirectory, "../../../../_common")
            );
        }

        protected override void LoadContent()
        {
            _burger = Content.Load<Texture>("Textures/burg.png");
            _rt = new RenderTarget(_burger.Width * 2, _burger.Height * 2);

            RenderTransform.SetMatrixMode(MatrixMode.Projection, Window);
            RenderTransform.LoadIdentity();
            RenderTransform.Orthographic(
                0,
                0,
                Window.Width,
                Window.Height,
                1,
                -1
            );
        }

        private float angle = 0;
        protected override void Update(float delta)
        {
            angle += 2f;
        }

        protected override void Draw(RenderContext context)
        {          
            context.RenderTo(_rt, () =>
            {
                context.Clear(Color.Aqua);

                RenderTransform.SetMatrixMode(MatrixMode.Model, _rt);
                RenderTransform.Push();
                RenderTransform.LoadIdentity();
                
                RenderTransform.Translate(_burger.Center);
                RenderTransform.Rotate(angle, new Vector3(0, 0, 1f));
                RenderTransform.Translate(-_burger.Center);
                
                context.DrawTexture(_burger, Vector2.Zero, Vector2.One, Vector2.Zero, 0);
                RenderTransform.Pop();
            });
            
            context.DrawTexture(_rt, Window.Center, Vector2.One, _rt.Center, 0);
        }
    }
}