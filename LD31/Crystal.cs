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
    public class Crystal
    {
        private SpriteInstance Sprite;
        private Rectangle m_Bounds;

        public Crystal()
        {

        }

        public void Initialize(Vector2 p_Location)
        {
            this.m_Bounds = Rectangle.Empty;
            this.Sprite = new SpriteInstance(LD31.SpriteLibrary.Sprites[101], p_Location);

            this.m_Bounds.X = (int)(p_Location.X - 32 / 2);
            this.m_Bounds.Y = (int)(p_Location.Y - 32 / 2);
            this.m_Bounds.Width = 32;
            this.m_Bounds.Height = 32;
        }

        public void Update(float DeltaTime, Player p_Player)
        {
            if (this.m_Bounds.Intersects(p_Player.CollisionBounds))
            {
                p_Player.Dungeon.PickUpCrystal();
            }



        }

        public void Draw(Rectangle p_RoomBounds, SpriteBatch sb)
        {
            Rectangle vOutRect = new Rectangle(this.m_Bounds.X - p_RoomBounds.X, this.m_Bounds.Y - p_RoomBounds.Y, this.m_Bounds.Width, this.m_Bounds.Height);
            sb.Draw(this.Sprite.Sprite.Texture, vOutRect, this.Sprite.Sprite.AnimSet.GetAnimationRect("default", 0.0f), Color.White);
        }
    }

  
}
