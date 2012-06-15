using System;
using System.Collections.Generic;
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
        public SpriteBatch SpriteBatch;
        public ParticleEngine ParticleEngine;

        public AudioEngine AudioEngine;
        public WaveBank WaveBank;
        public SoundBank SoundBank;
        public Cue TrackCue;

        private const float PROJECTILE_SPEED = 8;
        private const int PROJECTILE_DELAY = 108;
        private int _projectileCountdown;
        private int PreferredBackBufferWidth = 1920;
        private int PreferredBackBufferHeight = 1200;

        public readonly bool useBloom = true;
        public readonly bool fullscreen = false;
        public bool gameover = false;
        private int gameOverCount = 0;
        private int enemyCounter = 0;
        private int difficulty = 100;
        private int overTimeDifficulty = 200;

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
            StateManager = StateManager.GetInstance(this);

            Components.Add(InputManager);
            //Components.Add(BasicManager);
            //Components.Add(CollisionManager);
            Components.Add(StateManager);
            //if (useBloom)
            //    Components.Add(bloom);
            //Components.Add(Overlay);
            //Components.Add(Camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            AudioEngine = new AudioEngine(@"Content\audio\GameAudio.xgs");
            WaveBank = new WaveBank(AudioEngine, @"Content\audio\Wave Bank.xwb");
            SoundBank = new SoundBank(AudioEngine, @"Content\audio\Sound Bank.xsb");
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("circle"));
            textures.Add(Content.Load<Texture2D>("star"));
            textures.Add(Content.Load<Texture2D>("diamond"));
            ParticleEngine = new ParticleEngine(textures, new Vector2(400, 240));

            var w = new WorldGrid();
            BasicManager.AddBasic(w);

            var p = new PlayerModel { Position = new Vector3(0, 0, 0) };
            BasicManager.AddBasic(p);

            var e = new SimpleEnemy { Position = new Vector3(-10, 2, 10) };
            BasicManager.AddBasic(e);

            var e2 = new ModelEnemy(1.0f, Content.Load<Model>(@"Models\Dodeca")) { Position = new Vector3(-100, 2, 10) };
            BasicManager.AddBasic(e2);

            Camera.Following = p;

            TrackCue = SoundBank.GetCue("music");
            TrackCue.Play();
            StateManager.StupidLoadContent();
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
            base.Draw(gameTime);
        }

        private void CreateRandomEnemies()
        {
            if(enemyCounter++ % (difficulty+overTimeDifficulty) == 0)
            {
                enemyCounter = 1;
                Random gen = new Random();
                var pos = new Vector3(gen.Next(-190, 190), gen.Next(-190, 190), gen.Next(-190, 190));
                switch(gen.Next(0, 3))
                {
                    case 0:
                        var e1 = new ModelEnemy(1.0f, Content.Load<Model>(@"Models\Cube")) { Position = pos};
                        BasicManager.AddBasic(e1);
                        break;
                    case 1:
                        var e2 = new ModelEnemy(1.0f, Content.Load<Model>(@"Models\Dodeca")) { Position = pos };
                        BasicManager.AddBasic(e2);
                        break;
                    case 2:
                        var e = new SimpleEnemy { Position = pos };
                        BasicManager.AddBasic(e);
                        break;
                }
                
                overTimeDifficulty--;
            }
        }


        public void Update2(GameTime gameTime)
        {
            if (InputManager.KeyPressed(InputManager.GameKeyCodes.QUIT))
                this.Exit();
            
            FireShots(gameTime);
            CreateRandomEnemies();

            AudioEngine.Update();
            //   base.Update(gameTime);
        }

        public void Draw2(GameTime gameTime)
        {
            if (useBloom)
                bloom.BeginDraw();
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };

            //   base.Draw(gameTime);
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
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Backward * PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                    SoundBank.PlayCue("phasers");
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_DOWN)))
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Forward * PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                    SoundBank.PlayCue("phasers");
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_LEFT)))
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Right * PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                    SoundBank.PlayCue("phasers");
                }
                else if (InputManager.KeyDown((InputManager.GameKeyCodes.SHOOT_RIGHT)) || Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    BasicManager.AddShot(Camera.Following.Position, Matrix.CreateFromQuaternion(Camera.Following.Rotation).Left * PROJECTILE_SPEED);
                    _projectileCountdown = PROJECTILE_DELAY;
                    SoundBank.PlayCue("phasers");
                }
            }
            else
                _projectileCountdown -= gameTime.ElapsedGameTime.Milliseconds;
        }

        internal void BeginGameOver()
        {
            gameover = true;
        }

        public void GameOverUpdate()
        {
            if (gameOverCount++ < 200)
            {
                ParticleEngine.EmitterLocation = new Vector2(GraphicsDevice.Viewport.Bounds.Width/2,
                                                             GraphicsDevice.Viewport.Bounds.Height/2);
                ParticleEngine.Update();
            }


        }

        public void GameOverDraw()
        {
            ParticleEngine.Draw(SpriteBatch);
        }
    }
}