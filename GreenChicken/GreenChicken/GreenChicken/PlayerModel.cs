using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    class PlayerModel : BasicPrimitive
    {
        private Vector3 _playerPosition = Vector3.Zero;
        private const float PLAYER_SPEED = 1.5f;
        private InputManager inputManager;

        public PlayerModel(bool isCollidable = true)
        {
            IsCollidable= isCollidable;
            inputManager = InputManager.GetInstance();
            CreateVertexArray();
        }

        public override void Update()
        {
            if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_LEFT))
            {
                _playerPosition.X -= PLAYER_SPEED;
            }
            else if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_RIGHT))
            {
                _playerPosition.X += PLAYER_SPEED;
            }

            if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_BACKWARD))
            {
                _playerPosition.Z -= PLAYER_SPEED;
            }
            else if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_FORWARD))
            {
                _playerPosition.Z += PLAYER_SPEED;
            }

            if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_UP))
            {
                _playerPosition.Y += PLAYER_SPEED;
            }
            else if (inputManager.KeyDown(InputManager.GameKeyCodes.MOVE_DOWN))
            {
                _playerPosition.Y -= PLAYER_SPEED;
            }
        }

        #region Overrides of BasicPrimitive

        protected override void CreateVertexArray()
        {
            Type = PrimitiveType.LineList;
            ColorVerts = new VertexPositionColor[12];
            ColorVerts[0] = new VertexPositionColor(
                new Vector3(5, 0, 0), Color.White);
            ColorVerts[1] = new VertexPositionColor(
                new Vector3(-5, 0, 0), Color.White);
            ColorVerts[2] = new VertexPositionColor(
                new Vector3(5, 0, 0), Color.White);
            ColorVerts[3] = new VertexPositionColor(
                new Vector3(0, 5, 0), Color.White);
            ColorVerts[4] = new VertexPositionColor(
                new Vector3(5, 0, 0), Color.White);
            ColorVerts[5] = new VertexPositionColor(
                new Vector3(0, 2.5f, 5), Color.White);
            ColorVerts[6] = new VertexPositionColor(
                new Vector3(0, 2.5f, 5), Color.White);
            ColorVerts[7] = new VertexPositionColor(
                new Vector3(-5, 0, 0), Color.White);
            ColorVerts[8] = new VertexPositionColor(
                new Vector3(0, 2.5f, 5), Color.White);
            ColorVerts[9] = new VertexPositionColor(
                new Vector3(0, 5, 0), Color.White);
            ColorVerts[10] = new VertexPositionColor(
                new Vector3(0, 5, 0), Color.White);
            ColorVerts[11] = new VertexPositionColor(
                new Vector3(-5, 0, 0), Color.White);

        }

        #endregion
    }
}
