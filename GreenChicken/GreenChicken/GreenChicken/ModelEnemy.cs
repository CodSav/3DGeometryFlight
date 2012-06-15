using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    internal class ModelEnemy : Enemy
    {
        public static PlayerModel player;
        private static BasicEffect effect;
        protected float _boundingSphereSize = 3.0f;
        protected float speed;
        protected Vector3 target;
        protected Vector3 Color;

        public ModelEnemy(float spd, Model model, bool collidable = true)
        {
            IsCollidable = collidable;
            if(IsCollidable)
                CollisionManager.GetInstance(null).AddToCollidables(this);
            this.model = model;
            speed = spd;
            if(effect == null)
                effect = new BasicEffect(Game1.GameInstance.GraphicsDevice);
            Random gen = new Random();
            Color = new Vector3((float) gen.NextDouble(), (float) gen.NextDouble(), (float) gen.NextDouble());
            switch(gen.Next(0,6))
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

        public Model model { get; protected set; }
        
        #region Implemented from Basic

        public override void Draw(Camera camera)
        {
            var old = Game1.GameInstance.GraphicsDevice.RasterizerState;
            Game1.GameInstance.GraphicsDevice.RasterizerState = new RasterizerState { FillMode = FillMode.WireFrame };

            effect.World = World;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.LightingEnabled = true;
            
            effect.EmissiveColor = Color;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart mmp in mesh.MeshParts)
                {
                    mmp.Effect = effect;
                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                    }
                }
                mesh.Draw();
            }
            Game1.GameInstance.GraphicsDevice.RasterizerState = old;
        }

        protected override BoundingSphere GetBoundingSphere()
        {
            return new BoundingSphere(Position, _boundingSphereSize);
        }

        #endregion
    }
}