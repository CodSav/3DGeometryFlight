using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public class Camera : GameComponent
    {
        #region Constructors

        public Camera(Game game) : base(game)
        {
            View = Matrix.CreateLookAt(reference, Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                Game.Window.ClientBounds.Width/
                (float) Game.Window.ClientBounds.Height,
                1, 1000);
        }

        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            View = Matrix.CreateLookAt(pos, target, up);
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                Game.Window.ClientBounds.Width/
                (float) Game.Window.ClientBounds.Height,
                1, 1000);
        }

        #endregion

        #region View/Projection

        public Matrix View { get; protected set; }
        public Matrix Projection { get; protected set; }
        private Vector3 reference = new Vector3(0,10,-50);
        public Basic Following { get; set; }

        #endregion

        private void CreateLookAt()
        {
            Matrix rotationMatrix = Following.Rotation;

            Vector3 transformedReference =
                Vector3.Transform(reference, rotationMatrix);

            Vector3 cameraPosition = transformedReference + Following.Position;

            Vector3 cameraLookat =  Following.Position;

            View = Matrix.CreateLookAt(cameraPosition, cameraLookat, Vector3.TransformNormal(Vector3.Up, rotationMatrix));
        }

        public override void Update(GameTime gameTime)
        {
            CreateLookAt();
            base.Update(gameTime);
        }
    }
}