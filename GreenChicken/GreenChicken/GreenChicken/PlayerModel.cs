using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GreenChicken
{
    internal class PlayerModel : BasicPrimitive
    {
        private const float PLAYER_SPEED = 1.5f;
        private readonly InputManager inputManager;

        private int rotateCount = 0;

        public PlayerModel(bool isCollidable = true)
        {
            IsCollidable = isCollidable;
            inputManager = InputManager.GetInstance();
            LoadPrimitive();
        }

        public override void Update()
        {
            PlayerGetInput();
        }


        private void PlayerGetInput()
        {
            bool move = false;
            if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_LEFT))
            {
                Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(2));
            }
            if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_RIGHT))
            {
                Rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(-2));
            }
            if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_UP))
            {
                Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(-2));
            }if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_DOWN))
            {
                Rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(2));
            }
            if ( inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_FORWARD))
                Position += Rotation.Backward* PLAYER_SPEED;
            else if(inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_BACKWARD))
                Position -= Rotation.Backward * PLAYER_SPEED;
        }

        private void OldMovement()
        {
            bool move = false;
            Vector3 _playerPosition = Position;
            

            _playerPosition = Rotation.Backward * -PLAYER_SPEED;
            Position += _playerPosition;


            //TEMP ROTATION CODE TO TEST
            //TODO REMOVE
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Z))
                Rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(90));
            if (keyboardState.IsKeyDown(Keys.X))
                Rotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / 60);
            if (keyboardState.IsKeyDown(Keys.C))
                Rotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / 60);
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
            return _world * Rotation * Matrix.CreateTranslation(Position);
        }

        #endregion
    }
}