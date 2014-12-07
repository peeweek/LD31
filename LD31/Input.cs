using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace LD31
{
    public class Input
    {
        public InputState Up { get { return m_UpState; } }
        public InputState Down { get { return m_DownState; } }
        public InputState Left { get { return m_LeftState; } }
        public InputState Right { get { return m_RightState; } }
        public InputState A { get { return m_AState; } }
        public InputState B { get { return m_BState; } }
        public InputState Pause { get {return m_PauseState;} }
        public Vector2 Direction { get { return m_Direction; } }


        private InputState m_UpState;
        private InputState m_DownState;
        private InputState m_LeftState;
        private InputState m_RightState;
        private InputState m_AState;
        private InputState m_BState;
        private InputState m_PauseState;

        private Vector2 m_Direction;

        public Input()
        {
            this.m_AState = InputState.Idle;
            this.m_BState = InputState.Idle;
            this.m_PauseState = InputState.Idle;
            this.m_Direction = Vector2.Zero;

            this.m_UpState = InputState.Idle;
            this.m_DownState = InputState.Idle;
            this.m_LeftState = InputState.Idle;
            this.m_RightState = InputState.Idle;
            
        }

        public void Update()
        {

             GamePadState vGPState = GamePad.GetState(PlayerIndex.One);
            KeyboardState vKBState = Keyboard.GetState();

            #region A B Start
            /*
             *  A BUTTON MANAGEMENT
             */
            if (vGPState.Buttons.A == ButtonState.Pressed || vKBState.IsKeyDown(Keys.Space))
            {
                if (A == InputState.JustReleased || A == InputState.Idle) m_AState = InputState.JustPressed;
                else if (A == InputState.JustPressed) m_AState = InputState.Pressed;
            }
            else
            {
                if (A == InputState.JustPressed || A == InputState.Pressed) m_AState = InputState.JustReleased;
                else if (A == InputState.JustReleased) m_AState = InputState.Idle;
            }

            /*
             *  B BUTTON MANAGEMENT
             */
            if (vGPState.Buttons.B == ButtonState.Pressed || vKBState.IsKeyDown(Keys.RightShift))
            {
                if (B == InputState.JustReleased || B == InputState.Idle) m_BState = InputState.JustPressed;
                else if (B == InputState.JustPressed) m_BState = InputState.Pressed;
            }
            else
            {
                if (B == InputState.JustPressed || B == InputState.Pressed) m_BState = InputState.JustReleased;
                else if (B == InputState.JustReleased) m_BState = InputState.Idle;
            }

            /*
             *  PAUSE BUTTON MANAGEMENT
             */
            if (vGPState.Buttons.Start == ButtonState.Pressed || vKBState.IsKeyDown(Keys.Escape))
            {
                if (Pause == InputState.JustReleased || Pause == InputState.Idle) m_PauseState = InputState.JustPressed;
                else if (Pause == InputState.JustPressed) m_PauseState = InputState.Pressed;
            }
            else
            {
                if (Pause == InputState.JustPressed || Pause == InputState.Pressed) m_PauseState = InputState.JustReleased;
                else if (Pause == InputState.JustReleased) m_PauseState = InputState.Idle;
            }
            #endregion
            /*
             * DIRECTIONS
             */
            m_Direction.X = 0.0f;
            m_Direction.Y = 0.0f;

            if (vKBState.IsKeyDown(Keys.Right) || vGPState.ThumbSticks.Left.X > 0.25f || vGPState.DPad.Right == ButtonState.Pressed)
            {
                m_Direction.X += 1.0f;
                if (Right == InputState.JustReleased || Right == InputState.Idle) m_RightState = InputState.JustPressed;
                else if (Right == InputState.JustPressed) m_RightState = InputState.Pressed;

            } else {

                if (Right == InputState.JustPressed || Right == InputState.Pressed) m_RightState = InputState.JustReleased;
                else if (Right == InputState.JustReleased) m_RightState = InputState.Idle;
            }



            if (vKBState.IsKeyDown(Keys.Left) || vGPState.ThumbSticks.Left.X < -0.25f || vGPState.DPad.Left == ButtonState.Pressed)
            {
                m_Direction.X -= 1.0f;
                if (Left == InputState.JustReleased || Left == InputState.Idle) m_LeftState = InputState.JustPressed;
                else if (Left == InputState.JustPressed) m_LeftState = InputState.Pressed;
            }
            else
            {
                if (Left == InputState.JustPressed || Right == InputState.Pressed) m_LeftState = InputState.JustReleased;
                else if (Left == InputState.JustReleased) m_LeftState = InputState.Idle;
            }



            if (vKBState.IsKeyDown(Keys.Down) || vGPState.ThumbSticks.Left.Y < -0.25f || vGPState.DPad.Down == ButtonState.Pressed)
            {
                m_Direction.Y += 1.0f;
                if (Down == InputState.JustReleased || Down == InputState.Idle) m_DownState = InputState.JustPressed;
                else if (Down == InputState.JustPressed) m_DownState = InputState.Pressed;
            }
            else
            {
                if (Down == InputState.JustPressed || Down == InputState.Pressed) m_DownState = InputState.JustReleased;
                else if (Down == InputState.JustReleased) m_DownState = InputState.Idle;
            }




            if (vKBState.IsKeyDown(Keys.Up) || vGPState.ThumbSticks.Left.Y > 0.25f || vGPState.DPad.Up == ButtonState.Pressed)
            {
                m_Direction.Y -= 1.0f;
                if (Up == InputState.JustReleased || Up == InputState.Idle) m_UpState = InputState.JustPressed;
                else if (Up == InputState.JustPressed) m_UpState = InputState.Pressed;
            }
            else
            {
                if (Up == InputState.JustPressed || Up == InputState.Pressed) m_UpState = InputState.JustReleased;
                else if (Up == InputState.JustReleased) m_UpState = InputState.Idle;
            }

            //this.m_Direction = Vector2.Normalize(m_Direction);


        }

        
    }

    public enum InputState { Idle, Pressed, JustPressed, JustReleased }

}
