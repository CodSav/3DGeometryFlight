using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public abstract class Basic
    {
        public Matrix World { get { return GetWorld(); } protected set { _world = value; } }
        protected abstract Matrix GetWorld();
        protected Matrix _world = Matrix.Identity;

        public BoundingSphere BoundingSphere { get { return GetBoundingSphere(); } protected set { _boundingSphere = value; } }
        protected abstract BoundingSphere GetBoundingSphere();
        protected BoundingSphere _boundingSphere;

        //Properties for describing if the object can collide with other objects
        //and a list of objects it has collided with
        public bool IsCollidable { get; set; }
        public ArrayList CollidesWith = new ArrayList();

        public Vector3 Position { get { return GetPosition(); } set { SetPosition(value); } }
        protected abstract Vector3 GetPosition();
        protected virtual void SetPosition(Vector3 pos)
        {
            _position = pos;
            UpdateZone();
        }
        protected Vector3 _position = new Vector3(0,0,0);

        //Zone is a specific area of space in which to detect collisions. 
        //Each time movement is updated, the zone should be updated.
        public BoundingBox Zone { get { return GetZone(); } protected set { SetZone(value); }}
        protected abstract BoundingBox GetZone();
        protected abstract BoundingBox SetZone(BoundingBox box);
        protected abstract void UpdateZone();

        public abstract void Update();
        public abstract void Draw(Camera camera);
    }
}
