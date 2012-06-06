using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public class BasicModel : CollidableModel
    {
        public Game Game;
        public bool Render = true;
        protected EffectTechnique currentEffect;
        protected Effect effect;
        protected Texture2D texture;

        public BasicModel(Model m, Game game, bool isCollidable = false)
        {
        }

        public virtual void Update()
        {
        }

        public void Draw(Camera camera)
        {
            if (!Render) return;
            var transforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    mesh.Draw();
                }
            }
        }
    }
}