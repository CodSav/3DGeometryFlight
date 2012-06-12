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
        public Projectile(bool isCollidable = true)
        {
            IsCollidable = isCollidable;
            LoadPrimitive();
        }

        public override void Update()
        {
            World *= Rotation;
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
    }
}
