using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenChicken.States
{
    public class EndState : State
    {
        public override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
             Game1.GameInstance.Overlay.Update(gt);
             if(InputManager.GetInstance(null).KeyPressed(InputManager.GameKeyCodes.ENTER))
             {
                 Game1.GameInstance.Exit();
             }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt)
        {
            Game1.GameInstance.Overlay.Draw(gt);
        }

        public override void Initiliaze()
        {
            Game1.GameInstance.Overlay.Initialize();
            CollisionManager.GetInstance(null).Initialize();
        }

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
        }
    }
}
