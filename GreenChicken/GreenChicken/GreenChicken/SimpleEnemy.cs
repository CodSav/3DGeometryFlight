using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    public class SimpleEnemy : BasicPrimitive
    {
        public SimpleEnemy(bool isCollidable = true)
        {
            IsCollidable = isCollidable;
            LoadPrimitive();
            switch (new Random().Next(0, 6))
            {
                case 0:
                    moveType = MoveType.Static;
                    break;
                case 1:
                    moveType = MoveType.Simple2;
                    break;
                case 2:
                    moveType = MoveType.Simple;
                    break;
                case 3:
                    moveType = MoveType.Random;
                    break;
                case 4:
                    moveType = MoveType.Simple3;
                    break;
                case 5:
                    moveType = MoveType.Follow;
                    break;
            }
        }

        #region Overrides of BasicPrimitive

        protected override void CreateVertexArray()
        {
            Type = PrimitiveType.TriangleStrip;
            ColorVerts = new VertexPositionColor[5];
            ColorVerts[0] = new VertexPositionColor(new Vector3(4, 4, 0), Color.BlueViolet);
            ColorVerts[1] = new VertexPositionColor(new Vector3(4, 4, -4), Color.BlueViolet);
            ColorVerts[2] = new VertexPositionColor(new Vector3(7, 0, 7), Color.Green);
            ColorVerts[3] = new VertexPositionColor(new Vector3(-4, 4, 0), Color.BlueViolet);
            ColorVerts[4] = new VertexPositionColor(new Vector3(-4, 4, -4), Color.BlueViolet);
        }

        #endregion
    }
}