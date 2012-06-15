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
        private static StateManager _sm;
        public static PauseState PauseState = new PauseState();
        public static PlayState PlayState = new PlayState();
        public static EndState EndState = new EndState();
        public static BeginState BeginState = new BeginState();

        public static State CurrentState;

        public static StateManager GetInstance(Game game)
        {
            return _sm ?? (_sm = new StateManager(game));
        }


        private StateManager(Game game) : base(game)
        {
            CurrentState = BeginState;

        }

        public void ChangeState(State toState)
        {
            CurrentState = toState;

        }

        protected override void UnloadContent()
        {
            CurrentState.UnloadContent();
        }

        public override void Initialize()
        {
            CurrentState.Initiliaze();
        }


        public void StupidLoadContent()
        {
            CurrentState.LoadContent();
            
        }

        public override void Update(GameTime gt)
        {
            CurrentState.Update(gt);

        }

        public override void Draw(GameTime gt)
        {
            CurrentState.Draw(gt);
        }




        

    }
}
