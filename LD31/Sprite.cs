using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD31
{
    public class Sprite
    {
        public Texture2D Texture;
        public SpriteAnimSet AnimSet;
        public Rectangle Collision;
        
        public Sprite(Texture2D p_Texture, Rectangle p_Collision, SpriteAnimSet p_AnimSet)
        {
            this.Texture = p_Texture;
            this.Collision = p_Collision;
            this.AnimSet = p_AnimSet;
        }

        public void Draw(SpriteBatch sb, Vector2 p_Position, Rectangle p_RoomBounds)
        {
            sb.Draw(Texture, new Rectangle((int)p_Position.X - p_RoomBounds.X, (int)p_Position.Y - p_RoomBounds.Y, AnimSet.CellSize, AnimSet.CellSize), AnimSet.GetAnimationRect("default", 0.0f), Color.White);
        }

    }
}
