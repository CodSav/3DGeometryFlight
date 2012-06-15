using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    class Projectile : BasicPrimitive
    {
        private float _yawAngle = 0;
        private float _pitchAngle = 0;
        private float _rollAngle = 0;
        private Vector3 _direction;
        private static int ouch = 0;
        
        public Projectile(Vector3 position, Vector3 direction, float yaw, float pitch, float roll, bool isCollidable = true)
        {
            //World = Matrix.CreateTranslation(position);
            _direction = direction;
            _yawAngle = yaw;
            _pitchAngle = pitch;
            _rollAngle = roll;
            Position = position;
            IsCollidable = isCollidable;
            LoadPrimitive();
        }

        public override void CollidesWith(Basic b)
        {
            if (b.GetType().Name == "SimpleEnemy" || b.GetType().Name == "ModelEnemy")
            {
                Console.WriteLine("ouch" + ouch++);
                CollisionManager.GetInstance(null).RemoveFromCollidables(this);
                BasicManager.GetInstance(null).RemoveFromShots(this);
            }
        }

        public override void Update()
        {
            Rotation *= Quaternion.CreateFromYawPitchRoll(_yawAngle, _pitchAngle, _rollAngle);

            Position += _direction;
        }

        protected override void CreateVertexArray()
        {
            Type = PrimitiveType.TriangleList;
            ColorVerts = new VertexPositionColor[9];
            ColorVerts[0] = new VertexPositionColor(new Vector3(0,0,1), Color.Red);
            ColorVerts[1] = new VertexPositionColor(new Vector3(-1,0,0), Color.Red);
            ColorVerts[2] = new VertexPositionColor(new Vector3(0,0,-1), Color.Red);
            ColorVerts[3] = new VertexPositionColor(new Vector3(1,0,0), Color.Red);
            ColorVerts[4] = new VertexPositionColor(new Vector3(0,0,1), Color.Red);
            ColorVerts[5] = new VertexPositionColor(new Vector3(0,1,0), Color.Red);
            ColorVerts[6] = new VertexPositionColor(new Vector3(0,0,-1), Color.Red);
            ColorVerts[7] = new VertexPositionColor(new Vector3(0,-1,0), Color.Red);
            ColorVerts[8] = new VertexPositionColor(new Vector3(0,0,1), Color.Red);
        }

        protected override BoundingSphere GetBoundingSphere()
        {
            return new BoundingSphere(Position, 1.5f);
        }
    }
}
