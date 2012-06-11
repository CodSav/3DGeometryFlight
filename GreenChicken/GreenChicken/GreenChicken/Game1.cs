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
        public Overlay Overlay;

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
            Overlay = new Overlay(this);
            Components.Add(Overlay);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            var p = new PlayerModel();
            BasicManager.AddBasic(p);

//            var e = new SimpleEnemy {Position = new Vector3(-10, 2, 10)};
//            BasicManager.AddBasic(e);

            Camera.Following = p;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };

            base.Draw(gameTime);
        }

        public void GetZoneOfPosition(Vector3 position)
        {
            //TODO GET ZONE
            //throw new NotImplementedException();
        }
    }
}