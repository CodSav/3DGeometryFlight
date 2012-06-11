using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenChicken.States;
using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public class StateManager : DrawableGameComponent
    {

        public static PauseState pause = new PauseState();
        public static PlayState play = new PlayState();
        public static EndState end = new EndState();
        public static BeginState begin = new BeginState();

        public static State current;


        public StateManager(Game game) : base(game)
        {
        }

        public void ChangeState()
        {


        }

        public override void Update(GameTime gt)
        {
            //check if state has changed





            current.Update(gt);

        }

        public override void Draw(GameTime gt)
        {


        }




        

    }
}
