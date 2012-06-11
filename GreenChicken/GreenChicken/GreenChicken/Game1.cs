using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GreenChicken
{
    public class Game1 : Game
    {
        public static Game1 GameInstance;

        public InputManager InputManager;
        private GraphicsDeviceManager graphics;
        public BasicManager BasicManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameInstance = this;
        }

        public Camera Camera { get; set; }

        protected override void Initialize()
        {
            InputManager.GetInstance(this);
            Components.Add(InputManager);
            Camera = new Camera(this);
            Components.Add(Camera);
            BasicManager = BasicManager.GetInstance(this);
            Components.Add(BasicManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            BasicManager.AddBasic(new PlayerModel());
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        public void GetZoneOfPosition(Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}