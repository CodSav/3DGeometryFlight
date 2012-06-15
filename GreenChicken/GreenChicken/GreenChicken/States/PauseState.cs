using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenChicken.States
{
    public class PauseState : State
    {
        public override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            Game1.GameInstance.Overlay.Update(gt);
            if (Game1.GameInstance.useBloom)
            {
                if (Game1.GameInstance.useBloom)
                    if (Game1.GameInstance.bloom.BlurAmount < 12)
                        Game1.GameInstance.bloom.BlurAmount += 0.1f;
                Game1.GameInstance.bloom.Update(gt);
            }
            

            if (InputManager.GetInstance(null).KeyPressed(InputManager.GameKeyCodes.PAUSE))
            {
                if(Game1.GameInstance.useBloom)
                    Game1.GameInstance.bloom.BlurAmount = 2.0f;
                StateManager.GetInstance(null).ChangeState(StateManager.PlayState);
            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt)
        {
            Game1.GameInstance.Draw2(gt);
            BasicManager.GetInstance(null).Draw(gt);
            if (Game1.GameInstance.useBloom)
                Game1.GameInstance.bloom.Draw(gt);
            Game1.GameInstance.Overlay.Draw(gt);
        }

        public override void Initiliaze()
        {
        }

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
        }
    }
}
