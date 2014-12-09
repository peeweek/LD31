using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LD31
{

    public class Monster
    {
        public Rectangle CollisionBounds { get { return this.m_CollisionBounds; } }

        private SpriteInstance Sprite;
        private byte Health;
        private float m_Speed;
        private float m_TrollFactor;
        private float m_TTL;
        private Vector2 m_Location;

        private Rectangle m_Bounds;
        private Rectangle m_CollisionBounds;

        public Monster(byte p_Health, float p_Speed )
        {
            this.Health = p_Health;
            this.m_Speed = p_Speed;
            this.m_TrollFactor = 1.0f;
        }


        /// <summary>
        /// Sets the World location of the monster
        /// </summary>
        /// <param name="p_Location"></param>
        public void SetLocation(Vector2 p_Location)
        {
            this.m_Bounds.X = (int)(p_Location.X - Sprite.Sprite.AnimSet.CellSize / 2);
            this.m_Bounds.Y = (int)(p_Location.Y - Sprite.Sprite.AnimSet.CellSize / 2);
            this.m_Bounds.Width = Sprite.Sprite.AnimSet.CellSize;
            this.m_Bounds.Height = Sprite.Sprite.AnimSet.CellSize;

            this.m_CollisionBounds.X = (int)(p_Location.X - 8);
            this.m_CollisionBounds.Y = (int)(p_Location.Y);
            this.m_CollisionBounds.Width = 16;
            this.m_CollisionBounds.Height = 12;

            this.m_Location = p_Location;
        }


        public void Initialize(byte p_SpriteIndex,Vector2 p_Location)
        {
            this.Sprite = new SpriteInstance(LD31.SpriteLibrary.Sprites[p_SpriteIndex], p_Location);
            this.SetLocation(p_Location);
        }

        public void Update(float DeltaTime, Player p_Player)
        {
            m_TTL += DeltaTime;

            Vector2 vDirection = Vector2.Normalize(p_Player.Location - this.m_Location) * m_Speed * DeltaTime * m_TrollFactor;

            foreach (Rectangle i_Collision in p_Player.Dungeon.CurrentRoom.Collisions)
            {
                vDirection = Utils.Collide(this.m_CollisionBounds, i_Collision, vDirection);
            }

            this.SetLocation(this.m_Location + vDirection );
            
            // (REVISED) Troll Factor
            if(m_TrollFactor < 3.0f) m_TrollFactor *= 1.0f + (0.12f * DeltaTime);


        }

        public void Draw(Rectangle p_RoomBounds, SpriteBatch sb)
        {
            Rectangle vOutRect = new Rectangle(this.m_Bounds.X - p_RoomBounds.X, this.m_Bounds.Y - p_RoomBounds.Y, this.m_Bounds.Width, this.m_Bounds.Height);
            sb.Draw(this.Sprite.Sprite.Texture, vOutRect, this.Sprite.Sprite.AnimSet.GetAnimationRect("default", m_TTL), Color.White);
        }
    }
}
