using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LD31
{
    public class HUD
    {
        private SpriteFont HUDSpriteFont;
        private SpriteFont HUDBigFont;

        private Dungeon m_Dungeon;

        public HUD(Dungeon p_Dungeon)
        {
            this.m_Dungeon = p_Dungeon;
        }

        public void Initialize(ContentManager p_Content)
        {
            this.HUDSpriteFont = p_Content.Load<SpriteFont>("Fonts\\ARC1X");
            this.HUDBigFont = p_Content.Load<SpriteFont>("Fonts\\ARC2X");

        }

        public void Draw(SpriteBatch sb, DungeonState state)
        {
            Vector2 vHeartPosition = new Vector2(m_Dungeon.CurrentRoom.Bounds.Width - 56.0f, 6.0f);
            Vector2 vCrystalPosition = new Vector2(0.0f, 6.0f);


            LD31.SpriteLibrary.Sprites[102].Draw(sb, vHeartPosition, Rectangle.Empty);
            LD31.SpriteLibrary.Sprites[103].Draw(sb, vCrystalPosition, Rectangle.Empty);

            sb.DrawString(this.HUDSpriteFont, "X" + m_Dungeon.Player.Life.ToString(), vHeartPosition + new Vector2(24.0f, 4.0f), Color.White);
            sb.DrawString(this.HUDSpriteFont, "X" + m_Dungeon.RemainingCrystals.ToString(), vCrystalPosition + new Vector2(28.0f, 4.0f), Color.White);

            Vector2 size, Bsize;

            switch (state)
            {
                case DungeonState.GameOver: size = HUDBigFont.MeasureString("GAME OVER");
                    sb.DrawString(HUDBigFont, "GAME OVER", new Vector2(m_Dungeon.CurrentRoom.Bounds.Width / 2.0f - size.X / 2.0f, m_Dungeon.CurrentRoom.Bounds.Height / 2.0f - size.Y / 2.0f)+new Vector2(4.0f,4.0f), Color.Black); 
                    sb.DrawString(HUDBigFont, "GAME OVER", new Vector2(m_Dungeon.CurrentRoom.Bounds.Width / 2.0f - size.X / 2.0f, m_Dungeon.CurrentRoom.Bounds.Height / 2.0f - size.Y / 2.0f), Color.Red); 
                    break;
                case DungeonState.Completed: size = HUDBigFont.MeasureString("YOU WIN!");
                    sb.DrawString(HUDBigFont, "YOU WIN!", new Vector2(m_Dungeon.CurrentRoom.Bounds.Width / 2.0f - size.X / 2.0f, m_Dungeon.CurrentRoom.Bounds.Height / 2.0f - size.Y / 2.0f) + new Vector2(4.0f, 4.0f), Color.Black);
                    sb.DrawString(HUDBigFont, "YOU WIN!", new Vector2(m_Dungeon.CurrentRoom.Bounds.Width / 2.0f - size.X / 2.0f, m_Dungeon.CurrentRoom.Bounds.Height / 2.0f - size.Y / 2.0f), Color.White); 
                    break;
                case DungeonState.Pause :
                    size = HUDBigFont.MeasureString("PAUSED");
                    Bsize = HUDSpriteFont.MeasureString("START TO UNPAUSE - A TO QUIT");
                    sb.DrawString(HUDBigFont, "PAUSED", new Vector2(m_Dungeon.CurrentRoom.Bounds.Width / 2.0f - size.X / 2.0f, m_Dungeon.CurrentRoom.Bounds.Height / 2.0f - size.Y / 2.0f) + new Vector2(4.0f, 4.0f), Color.Black);
                    sb.DrawString(HUDBigFont, "PAUSED", new Vector2(m_Dungeon.CurrentRoom.Bounds.Width / 2.0f - size.X / 2.0f, m_Dungeon.CurrentRoom.Bounds.Height / 2.0f - size.Y / 2.0f), Color.White);
                    sb.DrawString(HUDSpriteFont, "START TO UNPAUSE - A TO QUIT", new Vector2(m_Dungeon.CurrentRoom.Bounds.Width / 2.0f - Bsize.X / 2.0f, size.Y + m_Dungeon.CurrentRoom.Bounds.Height / 2.0f - Bsize.Y / 2.0f) + new Vector2(4.0f, 4.0f), Color.Black);
                    sb.DrawString(HUDSpriteFont, "START TO UNPAUSE - A TO QUIT", new Vector2(m_Dungeon.CurrentRoom.Bounds.Width / 2.0f - Bsize.X / 2.0f, size.Y + m_Dungeon.CurrentRoom.Bounds.Height / 2.0f - Bsize.Y / 2.0f), Color.White); 
                    break;
            }

        }
    }
}
