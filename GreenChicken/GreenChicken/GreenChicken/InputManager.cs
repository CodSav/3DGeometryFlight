using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    public class InputManager : GameComponent
    {
        public enum GameKeyCodes
        {
            //TODO Create enum values
            MOVE_LEFT = 0,
            MOVE_RIGHT = 1,
            MOVE_UP = 2,
            MOVE_DOWN = 3,
            ZOOM_IN = 4,
            ZOOM_OUT = 5,
            ROTATE_LEFT = 6,
            ROTATE_RIGHT = 7,
            SWITCH_MODEL_MODES = 8,
            SWITCH_TEXTURE_MODE = 9
        }
        
        private readonly int _numOfKeys;
        private readonly bool[] _currentState;
        private readonly bool[] _prevState;
        private readonly Keys[] _registeredKeys;

        private static InputManager im;

        private InputManager(Game game)
            : base(game)
        {
            _numOfKeys = Enum.GetNames(typeof (GameKeyCodes)).Length;
            _prevState = new bool[_numOfKeys];
            _currentState = new bool[_numOfKeys];
            _registeredKeys = new Keys[_numOfKeys];

            for (int i = 0; i < _numOfKeys; i++)
            {
                _prevState[i] = false;
                _currentState[i] = false;
            }

            RegisterKeys();
        }

        private void RegisterKeys()
        {
            // TODO: Construct any child components here
            _registeredKeys[(int)GameKeyCodes.MOVE_LEFT] = Keys.Left;
            _registeredKeys[(int)GameKeyCodes.MOVE_RIGHT] = Keys.Right;
            _registeredKeys[(int)GameKeyCodes.MOVE_UP] = Keys.Up;
            _registeredKeys[(int)GameKeyCodes.MOVE_DOWN] = Keys.Down;
            _registeredKeys[(int)GameKeyCodes.ZOOM_IN] = Keys.W;
            _registeredKeys[(int)GameKeyCodes.ZOOM_OUT] = Keys.S;
            _registeredKeys[(int)GameKeyCodes.ROTATE_LEFT] = Keys.A;
            _registeredKeys[(int)GameKeyCodes.ROTATE_RIGHT] = Keys.D;
            _registeredKeys[(int)GameKeyCodes.SWITCH_MODEL_MODES] = Keys.P;
            _registeredKeys[(int)GameKeyCodes.SWITCH_TEXTURE_MODE] = Keys.O;
        }

        public static InputManager GetInstance(Game game)
        {
            return im ?? (im = new InputManager(game));
        }

        public static InputManager GetInstance()
        {
            return im;
        }

        public bool KeyDown(GameKeyCodes keyID)
        {
            return (_currentState[(int) keyID]);
        }

        public bool KeyPressed(GameKeyCodes keyID)
        {
            return (_currentState[(int) keyID] && !_prevState[(int) keyID]);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _numOfKeys; i++)
            {
                _prevState[i] = _currentState[i];
                _currentState[i] = Keyboard.GetState().IsKeyDown(_registeredKeys[i]);
            }

            base.Update(gameTime);
        }
    }
}