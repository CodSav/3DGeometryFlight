using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    public class WorldGrid : BasicPrimitive
    {
        private const int GridSize = 20;
        private const int CubeSize = 20;
        private static readonly Color OuterColor = Color.Blue;
        private static readonly Color InnerColor = Color.DarkCyan;

        public WorldGrid(bool isCollidable = false)
        {
            IsCollidable = isCollidable;
            LoadPrimitive();
        }

        #region Overrides of Basic

        public override void CollidesWith(Basic b)
        {
        }

        public override void Update()
        {
        }

        #endregion

        #region Overrides of BasicPrimitive

        protected override void CreateVertexArray()
        {
            Type = PrimitiveType.LineList;
            ColorVerts = new VertexPositionColor[GridSize*25+4];

            for (int i = 0; i <= GridSize; i++)
            {
                int j = i - 10;
                Color color = InnerColor;

                if (i == 0 || i == GridSize)
                    color = OuterColor;

                ColorVerts[i * 24] = new VertexPositionColor(new Vector3(j * CubeSize, CubeSize * GridSize / -2, 10 * GridSize), color);
                ColorVerts[i * 24 + 1] = new VertexPositionColor(new Vector3(j * CubeSize, CubeSize * GridSize / 2, 10 * GridSize), color);
                ColorVerts[i * 24 + 2] = new VertexPositionColor(new Vector3(CubeSize * GridSize / -2, j * CubeSize, 10 * GridSize), color);
                ColorVerts[i * 24 + 3] = new VertexPositionColor(new Vector3(CubeSize * GridSize / 2, j * CubeSize, 10 * GridSize), color);
                ColorVerts[i * 24 + 4] = new VertexPositionColor(new Vector3(j * CubeSize, CubeSize * GridSize / -2, -10 * GridSize), color);
                ColorVerts[i * 24 + 5] = new VertexPositionColor(new Vector3(j * CubeSize, CubeSize * GridSize / 2, -10 * GridSize), color);
                ColorVerts[i * 24 + 6] = new VertexPositionColor(new Vector3(CubeSize * GridSize / -2, j * CubeSize, -10 * GridSize), color);
                ColorVerts[i * 24 + 7] = new VertexPositionColor(new Vector3(CubeSize * GridSize / 2, j * CubeSize, -10 * GridSize), color);

                ColorVerts[i * 24 + 8] = new VertexPositionColor(new Vector3(10 * GridSize, CubeSize * GridSize / -2, j * CubeSize), color);
                ColorVerts[i * 24 + 9] = new VertexPositionColor(new Vector3(10 * GridSize, CubeSize * GridSize / 2, j * CubeSize), color);
                ColorVerts[i * 24 + 10] = new VertexPositionColor(new Vector3(10 * GridSize, j * CubeSize, CubeSize * GridSize / -2), color);
                ColorVerts[i * 24 + 11] = new VertexPositionColor(new Vector3(10 * GridSize, j * CubeSize, CubeSize * GridSize / 2), color);
                ColorVerts[i * 24 + 12] = new VertexPositionColor(new Vector3(-10 * GridSize, CubeSize * GridSize / -2, j * CubeSize), color);
                ColorVerts[i * 24 + 13] = new VertexPositionColor(new Vector3(-10 * GridSize, CubeSize * GridSize / 2, j * CubeSize), color);
                ColorVerts[i * 24 + 14] = new VertexPositionColor(new Vector3(-10 * GridSize, j * CubeSize, CubeSize * GridSize / -2), color);
                ColorVerts[i * 24 + 15] = new VertexPositionColor(new Vector3(-10 * GridSize, j * CubeSize, CubeSize * GridSize / 2), color);

                ColorVerts[i * 24 + 16] = new VertexPositionColor(new Vector3(CubeSize * GridSize / -2,10 * GridSize, j * CubeSize ), color);
                ColorVerts[i * 24 + 17] = new VertexPositionColor(new Vector3(CubeSize * GridSize / 2,10 * GridSize, j * CubeSize), color);
                ColorVerts[i * 24 + 18] = new VertexPositionColor(new Vector3(j * CubeSize, 10 * GridSize, CubeSize * GridSize / -2), color);
                ColorVerts[i * 24 + 19] = new VertexPositionColor(new Vector3(j * CubeSize, 10 * GridSize, CubeSize * GridSize / 2), color);
                ColorVerts[i * 24 + 20] = new VertexPositionColor(new Vector3(CubeSize * GridSize / -2, -10 * GridSize, j * CubeSize), color);
                ColorVerts[i * 24 + 21] = new VertexPositionColor(new Vector3(CubeSize * GridSize / 2, -10 * GridSize, j * CubeSize), color);
                ColorVerts[i * 24 + 22] = new VertexPositionColor(new Vector3(j * CubeSize, -10 * GridSize, CubeSize * GridSize / -2), color);
                ColorVerts[i * 24 + 23] = new VertexPositionColor(new Vector3(j * CubeSize, -10 * GridSize, CubeSize * GridSize / 2), color);
            }


        }

        #endregion
    }
}