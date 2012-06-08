using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    class PlayerModel : BasicModel
    {
        private Vector3 _playerPosition = Vector3.Zero;
        private const float PLAYER_SPEED = 1.5f;

        public PlayerModel(Model m, Game game, bool isCollidable = true) : base(m, game, isCollidable)
        {
        }
        
        public override void Update()
        {
            //TODO: Change to move with camera once we have it
            InputManager inputManager = ((Game1) Game).InputManager;

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
    }
}
