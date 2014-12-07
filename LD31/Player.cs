using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LD31
{
    public class Player
    {
        public byte Life { get { return m_Life; } set { this.m_Life = value; } }
        public Vector2 Location { get { return this.m_Location; } set { SetLocation(value); } }
        public Rectangle CollisionBounds { get { return this.m_CollisionBounds; } }
        public Dungeon Dungeon { get { return this.m_Dungeon; } }
        public bool IsInvincible { get { return (this.m_InvincibleTTL > 0.0f); } }
        private byte m_Life;
        private Texture2D m_PlayerTexture;
        private SpriteAnimSet Animations;

        private PlayerState m_State;
        private Rectangle m_Bounds;
        private Rectangle m_CollisionBounds;
        private Vector2 m_Location;
        private float m_TTL;

        private string m_AnimName;
        private Dungeon m_Dungeon;
        private float m_InvincibleTTL;

        public Player(Dungeon p_Dungeon)
        {
            this.m_Dungeon = p_Dungeon;
            this.m_State = PlayerState.Moving;
            this.m_Life = 3;
            this.m_Bounds = Rectangle.Empty;
            this.m_InvincibleTTL = 0.0f; 
            
        }


        /// <summary>
        /// Sets the World location of the player
        /// </summary>
        /// <param name="p_Location"></param>
        public void SetLocation(Vector2 p_Location)
        {
            this.m_Bounds.X = (int)(p_Location.X - Animations.CellSize / 2);
            this.m_Bounds.Y = (int)(p_Location.Y - Animations.CellSize / 2);
            this.m_Bounds.Width = Animations.CellSize;
            this.m_Bounds.Height = Animations.CellSize;

            this.m_CollisionBounds.X = (int)(p_Location.X - 8) ;
            this.m_CollisionBounds.Y = (int)(p_Location.Y) ;
            this.m_CollisionBounds.Width = 16;
            this.m_CollisionBounds.Height = 12;

            this.m_Location = p_Location;
        }

        public void Hurt()
        {
            LD31.GameAudio.PlaySFX("Hurt");
            if (this.m_Life == 0) Dungeon.GameOver();
            else this.m_Life--;
            this.m_InvincibleTTL = 1.5f;
        }

        public void Initialize(ContentManager p_Content, Vector2 p_Location)
        {
            this.m_PlayerTexture = p_Content.Load<Texture2D>("Sprites\\PLR_Run");
            this.Animations = new SpriteAnimSet(this.m_PlayerTexture, 32);
            this.Animations.Add("Idle", 1, new byte[1] { 0 });
            this.Animations.Add("Moving", 20, new byte[5] { 0, 1, 2, 3, 4 });
            this.Animations.Add("Hurt", 5, new byte[2] { 0, 5 });
            this.Animations.Add("Dead", 1, new byte[1] { 0 });
            SetState(PlayerState.Moving);
            SetLocation(p_Location);
        }

        public void Update(float p_DeltaTime, Vector2 Direction)
        {
            this.m_TTL += p_DeltaTime;
            if (this.m_InvincibleTTL > 0.0f)
            {
                this.m_InvincibleTTL -= p_DeltaTime;
            }

            SetLocation(new Vector2(
                (float)Math.Floor(this.Location.X + Direction.X),
                (float)Math.Floor(this.Location.Y + Direction.Y)
                )
            );

            if (Direction.Length() == 0 && this.m_State != PlayerState.Idle) SetState(PlayerState.Idle);
            else if (this.m_State == PlayerState.Idle && Direction.Length() > 0) SetState(PlayerState.Moving);


        }

        public void SetState(PlayerState p_State)
        {
            this.m_State = p_State;
            this.m_TTL = 0.0f;
            switch (p_State)
            {
                case PlayerState.Idle: this.m_AnimName = "Idle"; break;
                case PlayerState.Moving: this.m_AnimName = "Moving"; break;
                default: this.m_AnimName = "Idle"; break;
            }
        }
        
        public void Draw(Rectangle p_RoomBounds, SpriteBatch sb)
        {
            if ((m_InvincibleTTL % 0.25f) < 0.125f)
            {
                Rectangle vOutRect = new Rectangle(this.m_Bounds.X - p_RoomBounds.X, this.m_Bounds.Y - p_RoomBounds.Y, this.m_Bounds.Width, this.m_Bounds.Height);
                sb.Draw(this.m_PlayerTexture, vOutRect, Animations.GetAnimationRect(this.m_AnimName, m_TTL), Color.White);
            } 
        }

    }

    public enum PlayerState
    {
        Idle,
        Moving,   
    }
}
