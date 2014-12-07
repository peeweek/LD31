using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD31
{
    public class Game
    {
        private Window m_Window;
        public Rectangle CurrentBounds;
        public GameState State;
        private Input m_Input;

        public Game() {

            // Init here
            this.State = GameState.Menu;
            this.m_Input = new Input();
        }


        public void Run()
        {
            LD31.Game = this;
            LD31.Input = this.m_Input;

 
            using (m_Window = new Window())
            {
                m_Window.Run();
            }



        }

        public void StartGame()
        {
            this.State = GameState.Game;
            LD31.GameAudio.SetMusic("Game");
            LD31.Dungeon = new Dungeon();
            LD31.Dungeon.Initialize(this.m_Window.Content);
        }

        public void MainMenu()
        {
            LD31.GameAudio.PlaySFX("Validate"); LD31.GameAudio.SetMusic("Menu"); 
            this.State = GameState.Menu;
            SetWindowBounds(new Rectangle(Globals.SCREEN_WIDTH/2 - 400, Globals.SCREEN_HEIGHT/2 - 240, 800, 480));
            
        }

        public void SetWindowBounds(Rectangle p_Rect)
        {
            this.m_Window.SetBounds(p_Rect);
        }


        public void Exit()
        {
            this.m_Window.Exit();
        }

    }

    public enum GameState
    {
        Menu,
        Game
    }



    public static class LD31
    {
        public static Game Game;
        public static Input Input;
        public static Dungeon Dungeon;
        public static SpriteLibrary SpriteLibrary;
        public static MonsterFactory MonsterFactory;
        public static GameAudio GameAudio;
    }
}
