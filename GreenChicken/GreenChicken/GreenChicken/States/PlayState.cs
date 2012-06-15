using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenChicken.States
{
    public class PlayState : State
    {
        public override void Update(Microsoft.Xna.Framework.GameTime gt)
        {


            Game1.GameInstance.Update2(gt);


            BasicManager.GetInstance(null).Update(gt);

            Game1.GameInstance.Overlay.Update(gt);
            CollisionManager.GetInstance(null).Update(gt);

            if (Game1.GameInstance.useBloom)
            Game1.GameInstance.bloom.Update(gt);

            Game1.GameInstance.Camera.Update(gt);

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
