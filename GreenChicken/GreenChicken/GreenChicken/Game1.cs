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
        public StateManager StateManager;
        public Overlay Overlay;

        private float _projectileSpeed = 1;
        private int _projectileDelay = 42;
        private int _projectileCountdown;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameInstance = this;
        }

        public Camera Camera { get; set; }

        protected override void Initialize()
        {
            InputManager = InputManager.GetInstance(this);
            Components.Add(InputManager);

            BasicManager = BasicManager.GetInstance(this);
            Components.Add(BasicManager);
            Overlay = new Overlay(this);
            Components.Add(Overlay);

            Camera = new Camera(this);
            Components.Add(Camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            var w = new WorldGrid();
            BasicManager.AddBasic(w);

            var p = new PlayerModel{Position = new Vector3(0,0,0)};
            BasicManager.AddBasic(p);

            var e = new SimpleEnemy {Position = new Vector3(-10, 2, 10)};
            BasicManager.AddBasic(e);

            var projectile = new Projectile {Position = new Vector3(-6, 2, 10)};
            BasicManager.AddBasic(projectile);

            Camera.Following = p;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            FireShots(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.RasterizerState = new RasterizerState {CullMode = CullMode.None};

            base.Draw(gameTime);
        }

        public void GetZoneOfPosition(Vector3 position)
        {
            //TODO GET ZONE
            //throw new NotImplementedException();
        }

        protected void FireShots(GameTime gameTime)
        {
            if (_projectileCountdown <= 0)
            {
                //TODO: Use input manager
                if (InputManager.KeyDown(InputManager.GameKeyCodes.FIRE))
                {
                    BasicManager.AddShot(Camera.Following.Position + new Vector3(0, 0, -1), Camera.Following.Rotation.Translation*_projectileSpeed);
                    _projectileCountdown = _projectileDelay;
                }
            }
            else
                _projectileCountdown -= gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}