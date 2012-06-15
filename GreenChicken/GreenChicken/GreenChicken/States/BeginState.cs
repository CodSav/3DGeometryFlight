using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenChicken.States
{
    public class BeginState : State
    {
        public String gameMode = "Normal";
        public override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            Game1.GameInstance.Overlay.Update(gt);
            
            if (InputManager.GetInstance(null).KeyPressed(InputManager.GameKeyCodes.ENTER))
            {
                StateManager.GetInstance(null).ChangeState(StateManager.PlayState);
            }

            if(InputManager.GetInstance(null).KeyPressed(InputManager.GameKeyCodes.SHOOT_UP))
            {
                Game1.GameInstance.difficulty = 200;
                gameMode = "Easy";
            }
            if (InputManager.GetInstance(null).KeyPressed(InputManager.GameKeyCodes.SHOOT_DOWN))
            {
                Game1.GameInstance.difficulty = 100;
                gameMode = "Normal";
            }
            if (InputManager.GetInstance(null).KeyPressed(InputManager.GameKeyCodes.SHOOT_LEFT))
            {
                Game1.GameInstance.difficulty = 60;
                gameMode = "Hard";
            }
            if (InputManager.GetInstance(null).KeyPressed(InputManager.GameKeyCodes.SHOOT_RIGHT))
            {
                Game1.GameInstance.difficulty = 1;
                gameMode = "Expert";
            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt)
        {
            Game1.GameInstance.Overlay.Draw(gt);
        }

        public override void Initiliaze()
        {
            BasicManager.GetInstance(null).Initialize();

            Game1.GameInstance.Overlay.Initialize();
            CollisionManager.GetInstance(null).Initialize();

            if (Game1.GameInstance.useBloom)
                Game1.GameInstance.bloom.Initialize();

            Game1.GameInstance.Camera.Initialize();

        }

        public override void LoadContent()
        {
            BasicManager.GetInstance(null).LoadContent();


            if (Game1.GameInstance.useBloom)
                Game1.GameInstance.bloom.LoadContent();



        }

        public override void UnloadContent()
        {
        }
    }
}
