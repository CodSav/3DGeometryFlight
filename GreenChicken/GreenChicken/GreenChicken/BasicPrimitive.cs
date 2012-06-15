using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    public enum PrimitiveFormat
    {
        Color,
        ColorTexture,
        NormalTexture,
        Texture
    }

    public abstract class BasicPrimitive : Enemy
    {
        #region Vertex Data

        public static Dictionary<string, BasicPrimitiveSettings> Settings =
            new Dictionary<string, BasicPrimitiveSettings>();

        private static BasicEffect effect;

        protected VertexPositionColorTexture[] ColorTextureVerts;
        protected VertexPositionColor[] ColorVerts;
        protected PrimitiveFormat Format = PrimitiveFormat.Color;
        protected VertexPositionNormalTexture[] NormalTextureVerts;
        protected VertexPositionTexture[] TextureVerts;

        protected PrimitiveType Type = PrimitiveType.TriangleStrip;
        protected VertexBuffer VertexBuffer;

        protected int VertexOffset;
        protected float _boundingSphereSize;
        protected int _primitiveCount;

        protected int PrimitiveCount
        {
            get { return GetPrimitiveCount(); }
            set { _primitiveCount = value; }
        }

        protected virtual int GetPrimitiveCount()
        {
            int vertCount = 0;

            switch (Format)
            {
                case PrimitiveFormat.Color:
                    vertCount = ColorVerts.Length;
                    break;
                case PrimitiveFormat.ColorTexture:
                    vertCount = ColorTextureVerts.Length;
                    break;
                case PrimitiveFormat.NormalTexture:
                    vertCount = NormalTextureVerts.Length;
                    break;
                case PrimitiveFormat.Texture:
                    vertCount = TextureVerts.Length;
                    break;
            }

            switch (Type)
            {
                case PrimitiveType.TriangleList:
                    vertCount /= 3;
                    break;
                case PrimitiveType.TriangleStrip:
                    vertCount = (vertCount - 1)/2;
                    break;
                case PrimitiveType.LineList:
                    vertCount /= 2;
                    break;
                case PrimitiveType.LineStrip:
                    vertCount --;
                    break;
            }

            return vertCount;
        }

        #endregion

        #region Create Primitive Data

        protected void LoadPrimitive()
        {
            if (IsCollidable)
                CollisionManager.GetInstance(null).AddToCollidables(this);
            string className = GetType().Name;

            if (Settings.ContainsKey(className))
            {
                LoadSettings(className);
                return;
            }

            CreateVertexArray();
            _boundingSphereSize = GetLargestDistance();
            switch (Format)
            {
                case PrimitiveFormat.Color:
                    VertexBuffer = new VertexBuffer(Game1.GameInstance.GraphicsDevice, typeof (VertexPositionColor),
                                                    ColorVerts.Length, BufferUsage.None);
                    VertexBuffer.SetData(ColorVerts);
                    break;
                case PrimitiveFormat.ColorTexture:
                    VertexBuffer = new VertexBuffer(Game1.GameInstance.GraphicsDevice,
                                                    typeof (VertexPositionColorTexture),
                                                    ColorTextureVerts.Length, BufferUsage.None);
                    VertexBuffer.SetData(ColorTextureVerts);
                    break;
                case PrimitiveFormat.NormalTexture:
                    VertexBuffer = new VertexBuffer(Game1.GameInstance.GraphicsDevice,
                                                    typeof (VertexPositionNormalTexture),
                                                    NormalTextureVerts.Length, BufferUsage.None);
                    VertexBuffer.SetData(NormalTextureVerts);
                    break;
                case PrimitiveFormat.Texture:
                    VertexBuffer = new VertexBuffer(Game1.GameInstance.GraphicsDevice, typeof (VertexPositionTexture),
                                                    TextureVerts.Length, BufferUsage.None);
                    VertexBuffer.SetData(TextureVerts);
                    break;
            }
            VertexBufferBinding[] buffers = Game1.GameInstance.GraphicsDevice.GetVertexBuffers();
            int bufferSize = buffers.Length;
            Array.Resize(ref buffers, bufferSize + 1);
            buffers[bufferSize] = VertexBuffer;
            effect = new BasicEffect(Game1.GameInstance.GraphicsDevice);

            SaveSettings(className);
        }

        private void SaveSettings(string className)
        {
            var s = new BasicPrimitiveSettings
                        {
                            ColorTextureVerts = ColorTextureVerts,
                            ColorVerts = ColorVerts,
                            Format = Format,
                            NormalTextureVerts = NormalTextureVerts,
                            TextureVerts = TextureVerts,
                            Type = Type,
                            VertexBuffer = VertexBuffer,
                            VertexOffset = VertexOffset
                        };
            Settings.Add(className, s);
        }

        private void LoadSettings(string className)
        {
            BasicPrimitiveSettings s = Settings[className];
            ColorTextureVerts = s.ColorTextureVerts;
            ColorVerts = s.ColorVerts;
            Format = s.Format;
            NormalTextureVerts = s.NormalTextureVerts;
            TextureVerts = s.TextureVerts;
            Type = s.Type;
            VertexBuffer = s.VertexBuffer;
            VertexOffset = s.VertexOffset;
        }

        protected abstract void CreateVertexArray();

        protected float GetLargestDistance()
        {
            float longest = 0.0f;

            switch (Format)
            {
                case PrimitiveFormat.Color:
                    longest =
                        ColorVerts.Select(v => Vector3.Distance(Position, v.Position)).Concat(new[] {longest}).Max()/2;
                    break;
                case PrimitiveFormat.ColorTexture:
                    longest =
                        ColorTextureVerts.Select(v => Vector3.Distance(Position, v.Position)).Concat(new[] {longest})
                            .Max()/2;
                    break;
                case PrimitiveFormat.NormalTexture:
                    longest =
                        NormalTextureVerts.Select(v => Vector3.Distance(Position, v.Position)).Concat(new[]
                                                                                                          {longest})
                            .Max()/2;
                    break;
                case PrimitiveFormat.Texture:
                    longest =
                        TextureVerts.Select(v => Vector3.Distance(Position, v.Position)).Concat(new[] {longest}).Max
                            ()/2;
                    break;
            }

            return longest;
        }

        #endregion

        #region Implemented from Basic

        public override void Draw(Camera camera)
        {
            Game1.GameInstance.GraphicsDevice.SetVertexBuffer(VertexBuffer);


            effect.World = World;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.VertexColorEnabled = true;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                switch (Format)
                {
                    case PrimitiveFormat.Color:
                        Game1.GameInstance.GraphicsDevice.DrawUserPrimitives(Type, ColorVerts, VertexOffset,
                                                                             PrimitiveCount);
                        break;
                    case PrimitiveFormat.ColorTexture:
                        Game1.GameInstance.GraphicsDevice.DrawUserPrimitives(Type, ColorTextureVerts, VertexOffset,
                                                                             PrimitiveCount);
                        break;
                    case PrimitiveFormat.NormalTexture:
                        Game1.GameInstance.GraphicsDevice.DrawUserPrimitives(Type, NormalTextureVerts, VertexOffset,
                                                                             PrimitiveCount);
                        break;
                    case PrimitiveFormat.Texture:
                        Game1.GameInstance.GraphicsDevice.DrawUserPrimitives(Type, TextureVerts, VertexOffset,
                                                                             PrimitiveCount);
                        break;
                }
            }
        }

        protected override BoundingSphere GetBoundingSphere()
        {
            return new BoundingSphere(Position, _boundingSphereSize);
        }

        #endregion
    }
}