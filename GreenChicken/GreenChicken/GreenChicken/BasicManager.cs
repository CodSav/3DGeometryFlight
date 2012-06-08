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
        private BasicManager _basicManager;

        private BasicManager(Game game)
            : base(game)
        {
        }

        public BasicManager GetInstance(Game game)
        {
            if(_basicManager == null)
                _basicManager = new BasicManager(game);
            return _basicManager;
        }

        public override void Initialize()
        {
            InitializeBasic();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadContentBasic();
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

        private void DrawBasic()
        {
            // Loop through and draw each model
            foreach (BasicModel bm in _basicObjects)
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

        private void LoadContentBasic()
        {
            foreach (Basic b in _basicObjects)
            {
                b.Update();
            }
        }

        private void InitializeBasic()
        {
            foreach (Basic b in _basicObjects)
            {
                b.Update();
            }
        }
    }
}