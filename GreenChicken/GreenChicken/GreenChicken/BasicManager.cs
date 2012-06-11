using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    public class BasicManager : DrawableGameComponent
    {
        private readonly List<Basic> _basicObjects = new List<Basic>();
        private SpriteBatch _spriteBatch;
        private static BasicManager _basicManager;

        private BasicManager(Game game)
            : base(game)
        {
        }

        public static BasicManager GetInstance(Game game)
        {
            return _basicManager ?? (_basicManager = new BasicManager(game));
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateBasic();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawBasic();
            base.Draw(gameTime);
        }

        public void AddBasic(Basic b)
        {
            _basicObjects.Add(b);
        }

        private void DrawBasic()
        {
            // Loop through and draw each model
            foreach (Basic bm in _basicObjects)
            {
                bm.Draw(((Game1)Game).Camera);
            }
        }

        private void UpdateBasic()
        {
            foreach (Basic b in _basicObjects)
            {
                b.Update();
            }
        }
    }
}