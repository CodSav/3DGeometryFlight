using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    public class BasicPrimitiveSettings
    {
        #region Vertex Data

        public VertexPositionColorTexture[] ColorTextureVerts{get;set;}
        public VertexPositionColor[] ColorVerts{get;set;}
        public PrimitiveFormat Format {get;set;}
        public VertexPositionNormalTexture[] NormalTextureVerts{get;set;}
        public VertexPositionTexture[] TextureVerts{get;set;}
        public PrimitiveType Type {get;set;}
        public VertexBuffer VertexBuffer{get;set;}
        public int VertexOffset{get;set;}

        #endregion
    }
}
