using System.Collections;
using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public abstract class Basic
    {
        #region World

        protected Matrix _world = Matrix.Identity;

        public Matrix World
        {
            get { return GetWorld(); }
            protected set { _world = value; }
        }

        protected virtual Matrix GetWorld()
        {
            return _world*Matrix.CreateTranslation(Position);
        }

        #endregion

        #region BoundingSphere

        protected BoundingSphere _boundingSphere;

        public BoundingSphere BoundingSphere
        {
            get { return GetBoundingSphere(); }
            protected set { _boundingSphere = value; }
        }

        protected abstract BoundingSphere GetBoundingSphere();

        #endregion

        #region Collision

        //Properties for describing if the object can collide with other objects
        //and a list of objects it has collided with
        public ArrayList CollidesWith = new ArrayList();
        public bool IsCollidable { get; set; }

        #endregion

        #region Position

        protected Vector3 _position = new Vector3(0, 0, 0);

        public Vector3 Position
        {
            get { return GetPosition(); }
            set { SetPosition(value); }
        }

        protected virtual Vector3 GetPosition()
        {
            return _position;
        }

        protected virtual void SetPosition(Vector3 pos)
        {
            _position = pos;
            UpdateZone();
        }

        #endregion

        #region Rotation

        public Quaternion Rotation { get { return _rotation; } set { _rotation = value; } }

        protected Quaternion _rotation = Quaternion.Identity;

        #endregion

        #region Zone

        //Zone is a specific area of space in which to detect collisions. 
        //Each time movement is updated, the zone should be updated.
        protected BoundingBox _zone;

        public BoundingBox Zone
        {
            get { return GetZone(); }
            protected set { SetZone(value); }
        }

        protected virtual BoundingBox GetZone()
        {
            return _zone;
        }

        protected virtual void SetZone(BoundingBox box)
        {
            _zone = box;
        }

        protected virtual void UpdateZone()
        {
            Game1.GameInstance.GetZoneOfPosition(Position);
        }

        #endregion

        #region GameComponent Helpers

        public abstract void Update();
        public abstract void Draw(Camera camera);

        #endregion
    }
}