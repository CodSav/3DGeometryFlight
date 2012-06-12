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
        }

        #region Overrides of Basic

        public override void Update()
        {
        }

        #endregion

        #region Overrides of BasicPrimitive

        protected override void CreateVertexArray()
        {

            Type = PrimitiveType.TriangleStrip;
            ColorVerts = new VertexPositionColor[5];
            ColorVerts[0] = new VertexPositionColor(new Vector3(2, 2, 0), Color.BlueViolet);
            ColorVerts[1] = new VertexPositionColor(new Vector3(2, 2, -2), Color.BlueViolet);
            ColorVerts[2] = new VertexPositionColor(new Vector3(0, 0, 1), Color.Green);
            ColorVerts[3] = new VertexPositionColor(new Vector3(-2, 2, 0), Color.BlueViolet);
            ColorVerts[4] = new VertexPositionColor(new Vector3(-2, 2, -2), Color.BlueViolet);
        }

        #endregion
    }
}