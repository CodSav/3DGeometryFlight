using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace GreenChicken
{
    public abstract class Enemy : Basic
    {
        public int Life { get { return _life; } set { _life = value; } }
        protected int _life = 100;

        public override void CollidesWith(Basic b)
        {
            if (b.GetType().Name == "Projectile")
            {
                Game1.GameInstance.Overlay.IncreaseScore(1);
                Life -= 10;
            }

            if(Life <= 0)
            {
                Game1.GameInstance.SoundBank.PlayCue("enemyHit");
                Game1.GameInstance.Overlay.IncreaseScore(100);
                BasicManager.GetInstance(null).RemoveFromBasic(this);
                CollisionManager.GetInstance(null).RemoveFromCollidables(this);
            }
        }

        public override void Update()
        {
        }
    }
}
