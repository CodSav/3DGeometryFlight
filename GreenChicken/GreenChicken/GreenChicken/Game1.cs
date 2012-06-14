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
        public BloomComponent bloom;

        private const float PROJECTILE_SPEED = 3;
        private const int PROJECTILE_DELAY = 108;
        private int _projectileCountdown;
        private int PreferredBackBufferWidth = 1920;
        private int PreferredBackBufferHeight = 1200;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameInstance = this;
            //graphics.PreferredBackBufferWidth = PreferredBackBufferWidth;
            //graphics.PreferredBackBufferHeight = PreferredBackBufferHeight;
            //graphics.IsFullScreen = true;

        }

        public Camera Camera { get; set; }

        protected override void Initialize()
        {
            InputManager = InputManager.GetInstance(this);
            Components.Add(InputManager);

            BasicManager = BasicManager.GetInstance(this);
            Components.Add(BasicManager);

            bloom = new BloomComponent(this);
            Components.Add(bloom);

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

            Camera.Following = p;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(InputManager.GameKeyCodes.QUIT))
                this.Exit();

            FireShots(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            bloom.BeginDraw();
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
                if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_UP)))
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Backward*PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_DOWN)))
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Forward*PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_LEFT)))
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Right*PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_RIGHT)))
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Left*PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                }
            }
            else
                _projectileCountdown -= gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}