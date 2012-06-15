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
        private List<Basic> _shots = new List<Basic>();
        private const float SHOT_BOUND = 200;

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

        public void RemoveFromShots(Basic b)
        {
            _shots.Remove(b);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateBasic();
            UpdateShots();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.GameInstance.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            DrawBasic();
            DrawShots();
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
                bm.Draw(((Game1) Game).Camera);
            }
        }

        private void DrawShots()
        {
            foreach (var projectile in _shots)
            {
                projectile.Draw(((Game1) Game).Camera);
            }
        }

        private void UpdateBasic()
        {
            foreach (Basic b in _basicObjects)
            {
                b.Update();
            }
        }

        protected void UpdateShots()
        {
            for (int i = 0; i < _shots.Count; ++i)
            {
                _shots[i].Update();
                if (_shots[i].Position.Z < -SHOT_BOUND || _shots[i].Position.Y < -SHOT_BOUND || _shots[i].Position.X < -SHOT_BOUND ||
                    _shots[i].Position.Z > SHOT_BOUND || _shots[i].Position.Y > SHOT_BOUND || _shots[i].Position.X > SHOT_BOUND)
                {
                    var s = _shots[i];
                    _shots.RemoveAt(i);
                    CollisionManager.GetInstance(null).RemoveFromCollidables(s);
                }
            }
        }

        public void AddShot(Vector3 position, Vector3 direction)
        {
            //TODO: Make direction/rotation more right.
            if (direction == Vector3.Zero)
            {
                direction = new Vector3(0, 0, 1);
            }
            _shots.Add(new Projectile(position, direction, 0, 0, 0, true));
        }

        public int GetNumberOfShots()
        {
            return _shots.Count;
        }
    }
}