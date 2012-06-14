using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GreenChicken
{
    internal class PlayerModel : BasicPrimitive
    {
        private const float PLAYER_SPEED = 2f;
        private readonly InputManager _inputManager;
        private MouseState currentMouseState, prevMouseState;

        public PlayerModel(bool isCollidable = true)
        {
            IsCollidable = isCollidable;
            _inputManager = InputManager.GetInstance();
            LoadPrimitive();
            prevMouseState = Mouse.GetState();
        }

        public override void Update()
        {
            currentMouseState = Mouse.GetState();
            Vector3 playerPosition = Position;
            if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_LEFT))
            {
                playerPosition += Matrix.CreateFromQuaternion(Rotation).Right*PLAYER_SPEED;
            }
            else if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_RIGHT))
            {
                playerPosition += Matrix.CreateFromQuaternion(Rotation).Left*PLAYER_SPEED;
            }

            if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_BACKWARD))
            {
                playerPosition += Matrix.CreateFromQuaternion(Rotation).Forward*PLAYER_SPEED;
            }
            else if (_inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_FORWARD))
            {
                playerPosition += Matrix.CreateFromQuaternion(Rotation).Backward*PLAYER_SPEED;
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

            //TODO: Handle with InputManager
            float angleX = (prevMouseState.X - currentMouseState.X)*0.005f;
            float angleY = (prevMouseState.Y - currentMouseState.Y)*0.005f;

            Rotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, angleX);
            Rotation *= Quaternion.CreateFromAxisAngle(Vector3.Left, angleY);

            prevMouseState = currentMouseState;
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
            return _world*Matrix.CreateFromQuaternion(Rotation)*Matrix.CreateTranslation(Position);
        }

        #endregion
    }
}