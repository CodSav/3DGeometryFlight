using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GreenChicken
{
    public class Overlay : DrawableGameComponent
    {
        public int Score { get; private set; }
        private SpriteBatch _spriteBatch;
        private Game _game;
        private SpriteFont _scoreFont;


        public Overlay(Game game) : base(game)
        {
            _game = game;
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            _scoreFont = game.Content.Load<SpriteFont>(@"fonts/scoreFont");
            Score = 0;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            _spriteBatch.Begin();

            _spriteBatch.DrawString(_scoreFont, "Score: " + Score, new Vector2(10, 10), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void IncreaseScore(int addedValue)
        {
            if (addedValue > 0)
            {
                Score += addedValue;
            }
        }
    }
}