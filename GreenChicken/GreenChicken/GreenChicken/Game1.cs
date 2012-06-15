using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        public CollisionManager CollisionManager;

        AudioEngine _audioEngine;
        WaveBank _waveBank;
        SoundBank _soundBank;
        Cue _trackCue;

        private const float PROJECTILE_SPEED = 3;
        private const int PROJECTILE_DELAY = 108;
        private int _projectileCountdown;
        private int PreferredBackBufferWidth = 1920;
        private int PreferredBackBufferHeight = 1200;

        private readonly bool useBloom = false;
        private readonly bool fullscreen = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameInstance = this;
            if (!fullscreen) return;
            graphics.PreferredBackBufferWidth = PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = PreferredBackBufferHeight;
            graphics.IsFullScreen = true;
        }

        public Camera Camera { get; set; }

        protected override void Initialize()
        {
            CollisionManager = CollisionManager.GetInstance(this);
            InputManager = InputManager.GetInstance(this);
            BasicManager = BasicManager.GetInstance(this);
            if (useBloom)
                bloom = new BloomComponent(this);
            
            Overlay = new Overlay(this);
            Camera = new Camera(this);

            Components.Add(InputManager);
            Components.Add(BasicManager);
            Components.Add(CollisionManager);
            if (useBloom)
                Components.Add(bloom);
            Components.Add(Overlay);
            Components.Add(Camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _audioEngine = new AudioEngine(@"Content\audio\GameAudio.xgs");
            _waveBank = new WaveBank(_audioEngine, @"Content\audio\Wave Bank.xwb");
            _soundBank = new SoundBank(_audioEngine, @"Content\audio\Sound Bank.xsb");

            var w = new WorldGrid();
            BasicManager.AddBasic(w);

            var p = new PlayerModel{Position = new Vector3(0,0,0)};
            BasicManager.AddBasic(p);

            var e = new SimpleEnemy {Position = new Vector3(-10, 2, 10)};
            BasicManager.AddBasic(e);

            Camera.Following = p;

            _trackCue = _soundBank.GetCue("music");
            _trackCue.Play();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(InputManager.GameKeyCodes.QUIT))
                this.Exit();

            FireShots(gameTime);

            _audioEngine.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(useBloom)
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
                if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_UP)) || Mouse.GetState().MiddleButton == ButtonState.Pressed || 
                    (Mouse.GetState().RightButton == ButtonState.Pressed))
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Backward*PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                    _soundBank.PlayCue("phasers");
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_LEFT)) )
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Forward*PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                    _soundBank.PlayCue("phasers");
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_DOWN)))
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Right*PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                    _soundBank.PlayCue("phasers");
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_RIGHT)) || Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Left*PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                    _soundBank.PlayCue("phasers");
                }
            }
            else
                _projectileCountdown -= gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}