using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public class Camera : GameComponent
    {
        #region Constructors

        public Camera(Game game) : base(game)
        {
            View = Matrix.CreateLookAt(new Vector3(0, 30, -30), Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                Game.Window.ClientBounds.Width/
                (float) Game.Window.ClientBounds.Height,
                1, 100);
        }

        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            View = Matrix.CreateLookAt(pos, target, up);
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                Game.Window.ClientBounds.Width/
                (float) Game.Window.ClientBounds.Height,
                1, 100);
        }

        #endregion

        #region View/Projection

        public Matrix View { get; protected set; }
        public Matrix Projection { get; protected set; }

        #endregion

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}