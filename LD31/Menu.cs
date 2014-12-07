using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LD31
{
    public class Menu
    {
        private SpriteFont menuItemFont;
        private Texture2D menuBG;
        private Texture2D instructionsBG;

        private List<string> MenuItems;

        private Color menuSelectColor = Color.White;
        private Color menuTextColor = Color.Gray;
        private int menuSelectedItem;

        public int ViewWidth { get { return menuBG.Width; } }
        public int ViewHeight { get { return menuBG.Height; } }

        private bool bShowInstructions;

        public Menu(ContentManager p_ContentManager)
        {
            this.bShowInstructions = false;
            this.MenuItems = new List<string>();
            this.MenuItems.Add("START GAME");
            this.MenuItems.Add("WHAT HOW WHY");
            this.MenuItems.Add("EXIT");

            this.menuSelectedItem = 0;

            this.menuItemFont = p_ContentManager.Load<SpriteFont>("Fonts\\ARC2X");
            this.menuBG = p_ContentManager.Load<Texture2D>("GUI\\MainMenu");
            this.instructionsBG = p_ContentManager.Load<Texture2D>("GUI\\HowTo");

        }

        public void Initialize()
        {
            LD31.GameAudio.SetMusic("Menu");
            this.menuSelectedItem = 0;
        }

        public void Update()
        {
            if (bShowInstructions)
            {
                if (LD31.Input.A == InputState.JustPressed)
                {
                    bShowInstructions = false; 
                    LD31.GameAudio.PlaySFX("Select");
                }
            }
            else
            {
                if (LD31.Input.A == InputState.JustPressed)
                {
                    ExecuteMenu(menuSelectedItem);
                    LD31.GameAudio.PlaySFX("Validate");
                }
                else
                {
                    if (LD31.Input.Up == InputState.JustPressed)
                    {
                        menuSelectedItem--;
                        LD31.GameAudio.PlaySFX("Select");
                    }
                    else if (LD31.Input.Down == InputState.JustPressed)
                    {
                        menuSelectedItem++;
                        LD31.GameAudio.PlaySFX("Select");
                    }

                    if (menuSelectedItem < 0) menuSelectedItem = MenuItems.Count - 1;
                    else if (menuSelectedItem >= MenuItems.Count) menuSelectedItem = 0;
                }
            }
        }

        private void ExecuteMenu(int menuItem)
        {
            switch (menuItem)
            {
                case 0: LD31.Game.StartGame(); break;
                case 1: bShowInstructions = true; break;
                case 2: LD31.Game.Exit();
                    
                    break;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.Texture, BlendState.Opaque);

            if (bShowInstructions)
            {
                sb.Draw(instructionsBG, instructionsBG.Bounds, Color.White);
            }
            else
            {
                Vector2 vPosition = new Vector2(120.0f, 280.0f);
                sb.Draw(menuBG, menuBG.Bounds, Color.White);
                sb.End();

                sb.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    if (i == menuSelectedItem)
                    {
                        sb.DrawString(menuItemFont, MenuItems[i], vPosition, menuSelectColor);
                    }
                    else
                    {
                        sb.DrawString(menuItemFont, MenuItems[i], vPosition, menuTextColor);
                    }

                    vPosition.Y += 24.0f;
                }
            }
            sb.End();
        }

    }
}
