using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                Life -= 10;
            }

            if(Life < 0)
            {
                BasicManager.GetInstance(null).RemoveFromBasic(this);
                CollisionManager.GetInstance(null).RemoveFromCollidables(this);
            }
        }

        public override void Update()
        {
        }
    }
}
