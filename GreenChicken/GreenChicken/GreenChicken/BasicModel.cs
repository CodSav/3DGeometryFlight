using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    public abstract class BasicModel : Basic
    {
        public Model Model { get { return GetModel(); } protected set { _model = value; } }
        protected abstract Model GetModel();
        protected Model _model;

        public bool Render = true;

        #region Implemented from Basic

        protected override BoundingSphere GetBoundingSphere()
        {
            var boneTransforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(boneTransforms);
            Vector3 modelCenter = (from mesh in Model.Meshes
                                   let meshBounds = mesh.BoundingSphere
                                   let transform = boneTransforms[mesh.ParentBone.Index]
                                   select Vector3.Transform(meshBounds.Center, transform)).Aggregate(Vector3.Zero,
                                                                                                     (current,
                                                                                                      meshCenter) =>
                                                                                                     current +
                                                                                                     meshCenter);

            modelCenter /= Model.Meshes.Count;

            float modelRadius = (from mesh in Model.Meshes
                                 let meshBounds = mesh.BoundingSphere
                                 let transform = boneTransforms[mesh.ParentBone.Index]
                                 let meshCenter = Vector3.Transform(meshBounds.Center, transform)
                                 let transformScale = transform.Forward.Length()
                                 select (meshCenter - modelCenter).Length() + (meshBounds.Radius * transformScale)).Concat
                (new[] { 0f }).Max();

            return new BoundingSphere { Center = modelCenter, Radius = modelRadius };
        }

        #endregion
    }
}