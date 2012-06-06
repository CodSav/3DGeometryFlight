using System.Collections;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public abstract class CollidableModel
    {
        public ArrayList CollidesWith = new ArrayList();
        public bool IsCollidable;
        protected Matrix World = Matrix.Identity;
        public Model Model { get; protected set; }
        public Vector3 Position { get; set; }

        public BoundingSphere BoundingSphere
        {
            get { return GetBoundingSphere(); }
        }

        public BoundingSphere GetBoundingSphere()
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
                                 select (meshCenter - modelCenter).Length() + (meshBounds.Radius*transformScale)).Concat
                (new[] {0f}).Max();

            return new BoundingSphere {Center = modelCenter, Radius = modelRadius};
        }

        public virtual Matrix GetWorld()
        {
            return World*Matrix.CreateTranslation(Position);
        }
    }
}