using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GreenChicken
{
    public class PlayerModel : BasicPrimitive
    {
        private const float PLAYER_SPEED = 1.5f;
        private readonly InputManager _inputManager;

        public PlayerModel(bool isCollidable = true)
        {
            IsCollidable = isCollidable;
            _inputManager = InputManager.GetInstance();
            LoadPrimitive();
        }

        public override void Update()
        {
            Vector3 playerPosition = Position;
            if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_LEFT))
            {
                playerPosition.X += PLAYER_SPEED;
            }
            else if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_RIGHT))
            {
                playerPosition.X -= PLAYER_SPEED;
            }

            if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_BACKWARD))
            {
                playerPosition.Z -= PLAYER_SPEED;
            }
            else if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_FORWARD))
            {
                playerPosition.Z += PLAYER_SPEED;
            }

            if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_UP))
            {
                playerPosition.Y += PLAYER_SPEED;
            }
            else if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_DOWN))
            {
                playerPosition.Y -= PLAYER_SPEED;
            }

            if (playerPosition.X > 200)
            {
                playerPosition.X = 200;
            }
            else if (playerPosition.X < -200)
            {
                playerPosition.X = -200;
            }

            if (playerPosition.Y > 200)
            {
                playerPosition.Y = 200;
            }
            else if (playerPosition.Y < -200)
            {
                playerPosition.Y = -200;
            }

            if (playerPosition.Z > 200)
            {
                playerPosition.Z = 200;
            }
            else if (playerPosition.Z < -200)
            {
                playerPosition.Z = -200;
            }

            Position = playerPosition;


            //TEMP ROTATION CODE TO TEST
            //TODO REMOVE
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Z))
                Rotation *= Matrix.CreateRotationZ(MathHelper.PiOver4/60);
            if (keyboardState.IsKeyDown(Keys.X))
                Rotation *= Matrix.CreateRotationY(MathHelper.PiOver4/60);
            if (keyboardState.IsKeyDown(Keys.C))
                Rotation *= Matrix.CreateRotationX(MathHelper.PiOver4/60);
        }

        #region Overrides of BasicPrimitive

        protected override void CreateVertexArray()
        {
            Type = PrimitiveType.LineList;
            ColorVerts = new VertexPositionColor[12];
            ColorVerts[0] = new VertexPositionColor(
                new Vector3(2, 0, 0), Color.White);
            ColorVerts[1] = new VertexPositionColor(
                new Vector3(-2, 0, 0), Color.White);
            ColorVerts[2] = new VertexPositionColor(
                new Vector3(2, 0, 0), Color.White);
            ColorVerts[3] = new VertexPositionColor(
                new Vector3(0, 2, 0), Color.White);
            ColorVerts[4] = new VertexPositionColor(
                new Vector3(2, 0, 0), Color.White);
            ColorVerts[5] = new VertexPositionColor(
                new Vector3(0, 1, 4), Color.Red);
            ColorVerts[6] = new VertexPositionColor(
                new Vector3(0, 1, 4), Color.Red);
            ColorVerts[7] = new VertexPositionColor(
                new Vector3(-2, 0, 0), Color.White);
            ColorVerts[8] = new VertexPositionColor(
                new Vector3(0, 1, 4), Color.Red);
            ColorVerts[9] = new VertexPositionColor(
                new Vector3(0, 2, 0), Color.White);
            ColorVerts[10] = new VertexPositionColor(
                new Vector3(0, 2, 0), Color.White);
            ColorVerts[11] = new VertexPositionColor(
                new Vector3(-2, 0, 0), Color.White);
        }

        protected override Matrix GetWorld()
        {
            return _world*Rotation*Matrix.CreateTranslation(Position);
        }

        #endregion
    }
}