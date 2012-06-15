using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenChicken.States
{
    public class BeginState : State
    {
        public override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            Game1.GameInstance.Overlay.Update(gt);
            
            if (InputManager.GetInstance(null).KeyPressed(InputManager.GameKeyCodes.ENTER))
            {
                StateManager.GetInstance(null).ChangeState(StateManager.PlayState);
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
