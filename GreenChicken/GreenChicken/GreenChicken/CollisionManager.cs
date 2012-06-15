using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public struct Triangle
    {
        public Vector3 V0;
        public Vector3 V1;
        public Vector3 V2;

        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;
        }
    }



    public class CollisionManager : GameComponent
    {
        private static CollisionManager cm;
        private readonly ArrayList collidables = new ArrayList();

        private CollisionManager(Game game)
            : base(game)
        {
        }

        public static CollisionManager GetInstance(Game game)
        {
            return cm ?? (cm = new CollisionManager(game));
        }

        public override void Update(GameTime gameTime)
        {
            var collidables2 = (ArrayList)collidables.Clone();
            var finishedColliding = new ArrayList();
            foreach (Basic c in collidables2)
            {
                if(!c.IsCollidable)
                    continue;
                foreach (Basic c2 in collidables2)
                {
                    var t = new Tuple<Basic, Basic>(c,c2);
                    if (finishedColliding.Contains(t))
                        continue;
                    finishedColliding.Add(new Tuple<Basic, Basic>(c, c2));
                    finishedColliding.Add(new Tuple<Basic, Basic>(c2, c));

                    if (c == c2 || !c.IsCollidable || !c2.IsCollidable)
                        continue;
                    
                    if (!c.BoundingSphere.Intersects(c2.BoundingSphere)) 
                        continue;

                    c.CollidesWith(c2);
                    c2.CollidesWith(c);
                }
            }
            base.Update(gameTime);
        }

        public void AddToCollidables(Basic c)
        {
            collidables.Add(c);
        }

        public void RemoveFromCollidables(Basic c)
        {
            collidables.Remove(c);
        }

        #region Collision helpers

        public static ContainmentType Contains(ref BoundingSphere sphere, ref Vector3 v0, ref Vector3 v1,
                                               ref Vector3 v2)
        {
            float r2 = sphere.Radius*sphere.Radius;
            if (Vector3.DistanceSquared(v0, sphere.Center) <= r2 &&
                Vector3.DistanceSquared(v1, sphere.Center) <= r2 &&
                Vector3.DistanceSquared(v2, sphere.Center) <= r2)
                return ContainmentType.Contains;

            return Intersects(ref sphere, ref v0, ref v1, ref v2)
                       ? ContainmentType.Intersects
                       : ContainmentType.Disjoint;
        }

        public static ContainmentType Contains(ref BoundingSphere sphere, ref Triangle triangle)
        {
            return Contains(ref sphere, ref triangle.V0, ref triangle.V1, ref triangle.V2);
        }

        public static ContainmentType Contains(ref BoundingSphere s1, ref BoundingSphere s2)
        {
            return s1.Contains(s2);
        }

        public static ContainmentType Contains(ref BoundingSphere s1, ref BoundingBox b1)
        {
            return s1.Contains(b1);
        }

        public static ContainmentType Contains(ref BoundingBox b1, ref BoundingSphere s1)
        {
            return b1.Contains(s1);
        }

        private static Vector3 NearestPointOnTriangle(ref Vector3 p, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
        {
            float distance0 = Vector3.DistanceSquared(v0, p);
            float distance1 = Vector3.DistanceSquared(v1, p);
            float distance2 = Vector3.DistanceSquared(v2, p);

            if (distance0 < distance1 && distance0 < distance2)
                return v0;
            if (distance1 < distance2 && distance1 < distance0)
                return v1;
            return v2;
        }

        private static bool Intersects(ref BoundingSphere sphere, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
        {
            Vector3 p = NearestPointOnTriangle(ref sphere.Center, ref v0, ref v1, ref v2);
            return Vector3.DistanceSquared(sphere.Center, p) < sphere.Radius*sphere.Radius;
        }

        #endregion
    }
}