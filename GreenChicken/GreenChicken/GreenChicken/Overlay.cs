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
            _spriteBatch.DrawString(_scoreFont, "X: " + Game1.GameInstance.Camera.Following.Position.X, new Vector2(10, 25), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            _spriteBatch.DrawString(_scoreFont, "Y: " + Game1.GameInstance.Camera.Following.Position.Y, new Vector2(10, 40), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            _spriteBatch.DrawString(_scoreFont, "Z: " + Game1.GameInstance.Camera.Following.Position.Z, new Vector2(10, 55), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            _spriteBatch.DrawString(_scoreFont, "Shots: " + Game1.GameInstance.BasicManager.GetNumberOfShots(), new Vector2(10, 70), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

//            // Use to see postion vectors
//            _spriteBatch.DrawString(_scoreFont, "Forward: " + Game1.GameInstance.Camera.Following.Rotation.Forward, new Vector2(10, 355), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
//            _spriteBatch.DrawString(_scoreFont, "Backward: " + Game1.GameInstance.Camera.Following.Rotation.Backward, new Vector2(10, 370), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
//            _spriteBatch.DrawString(_scoreFont, "Up: " + Game1.GameInstance.Camera.Following.Rotation.Up, new Vector2(10, 385), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
//            _spriteBatch.DrawString(_scoreFont, "Down: " + Game1.GameInstance.Camera.Following.Rotation.Down, new Vector2(10, 400), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
//            _spriteBatch.DrawString(_scoreFont, "Left: " + Game1.GameInstance.Camera.Following.Rotation.Left, new Vector2(10, 415), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
//            _spriteBatch.DrawString(_scoreFont, "Right: " + Game1.GameInstance.Camera.Following.Rotation.Right, new Vector2(10, 430), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            _spriteBatch.DrawString(_scoreFont, Mouse.GetState().X + ": MouseX", new Vector2(600, 70), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            _spriteBatch.DrawString(_scoreFont, Mouse.GetState().Y + ": MouseY", new Vector2(600, 85), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

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