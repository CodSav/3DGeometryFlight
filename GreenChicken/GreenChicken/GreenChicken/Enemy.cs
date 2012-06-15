using System;
using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public enum MoveType
    {
        Static,
        Simple,
        Simple2,
        Simple3,
        Random,
        Follow
    }

    public abstract class Enemy : Basic
    {
        protected int _life = 100;
        protected Vector3 directionSimple = Vector3.Up;
        protected Vector3 directionSimple2 = Vector3.Right;
        protected Vector3 directionSimple3 = Vector3.Forward;
        protected MoveType moveType = MoveType.Static;
        protected Vector3 random = Vector3.Forward;
        protected int randomCount;
        protected float speed;
        protected Vector3 target;

        public int Life
        {
            get { return _life; }
            set { _life = value; }
        }

        public override void CollidesWith(Basic b)
        {
            if (b.GetType().Name == "Projectile")
            {
                Game1.GameInstance.SoundBank.PlayCue("enemyShot");
                Game1.GameInstance.Overlay.IncreaseScore(1);
                Life -= 10;
            }

            if (Life <= 0)
            {
                Game1.GameInstance.SoundBank.PlayCue("enemyHit");
                Game1.GameInstance.Overlay.IncreaseScore(100);
                BasicManager.GetInstance(null).RemoveFromBasic(this);
                CollisionManager.GetInstance(null).RemoveFromCollidables(this);
            }
        }

        public override void Update()
        {
            switch (moveType)
            {
                case MoveType.Static:
                    break;
                case MoveType.Simple:
                    Position += directionSimple;
                    if (Position.Y > 195 || Position.Y < -195)
                        directionSimple.Y *= -1;
                    break;
                case MoveType.Simple2:
                    Position += directionSimple2;
                    if (Position.X > 195 || Position.X < -195)
                        directionSimple2.X *= -1;
                    break;
                case MoveType.Simple3:
                    Position += directionSimple3;
                    if (Position.Z > 195 || Position.Z < -195)
                        directionSimple3.Z *= -1;
                    break;
                case MoveType.Follow:
                    target = Game1.GameInstance.Camera.Following.Position;
                    var temp = speed*Vector3.Normalize(target - Position);
                    Position += temp;
                    break;
                case MoveType.Random:
                    if (randomCount++%100 == 0)
                    {
                        var gen = new Random();
                        random = new Vector3((float) gen.NextDouble()*2 - 1, (float) gen.NextDouble()*2 - 1,
                                             (float) gen.NextDouble()*2 - 1);
                    }
                    Position += random;
                    if (Position.Z > 195 || Position.Z < -195 || Position.X > 195 || Position.X < -195 ||
                        Position.Y > 195 || Position.Y < -195)
                        Position -= random;
                    break;
            }
        }
    }
}