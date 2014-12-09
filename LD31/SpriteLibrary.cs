using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LD31
{
    public class SpriteLibrary
    {
        public Dictionary<byte, Sprite> Sprites;

        public SpriteLibrary()
        {
            this.Sprites = new Dictionary<byte, Sprite>();
        }

        public void Initialize(ContentManager p_Content)
        {
            Texture2D ENV_Test = p_Content.Load<Texture2D>("Sprites\\ENV_Test");
            Texture2D HUD_Game = p_Content.Load<Texture2D>("Sprites\\HUD_Game");
            Texture2D NME_Slime = p_Content.Load<Texture2D>("Sprites\\NME_Slime");
            Texture2D NME_Slime2 = p_Content.Load<Texture2D>("Sprites\\NME_Slime2");
            

            // Ground
            this.AddSprite(00, ENV_Test, 3, 32, false);
            this.AddSprite(01, ENV_Test, 4, 32, false);
            this.AddSprite(02, ENV_Test, 5, 32, false);
            this.AddSprite(03, ENV_Test, 6, 32, false);
            this.AddSprite(04, ENV_Test, 7, 32, false);
            this.AddSprite(05, ENV_Test, 8, 32, false);

            // Wall
            this.AddSprite(10, ENV_Test, 0, 32, true);
            this.AddSprite(11, ENV_Test, 1, 32, true);
            this.AddSprite(12, ENV_Test, 2, 32, true);
            

            // GUI
            this.AddSprite(100, HUD_Game, 0, 32, false);
            this.AddSprite(101, HUD_Game, 1, 32, false);
            this.AddSprite(102, HUD_Game, 2, 32, false);
            this.AddSprite(103, HUD_Game, 3, 32, false);

            // Enemies
            this.AddAnimatedSprite(50, NME_Slime, new byte[6] { 0, 1, 2, 3, 4, 5 }, 25, 32, true);
            this.AddAnimatedSprite(51, NME_Slime2, new byte[6] { 0, 1, 2, 3, 4, 5 }, 25, 32, true);

        }

        public void AddSprite(byte p_ID, Texture2D p_Texture, byte p_Frame, ushort p_Size, bool p_bCollides)
        {
            Rectangle vCollision;
            if (p_bCollides) vCollision = new Rectangle(0,0,p_Size,p_Size); else vCollision = Rectangle.Empty;
            this.Sprites.Add(p_ID, new Sprite(p_Texture, vCollision, new SpriteAnimSet(p_Texture, p_Size, p_Frame)));
        }

        public void AddAnimatedSprite(byte p_ID, Texture2D p_Texture, byte[] p_Frames, byte p_FrameRate, ushort p_Size, bool p_bCollides)
        {
            Rectangle vCollision;
            if (p_bCollides) vCollision = new Rectangle(0, 0, p_Size, p_Size); else vCollision = Rectangle.Empty;
            this.Sprites.Add(p_ID, new Sprite(p_Texture, vCollision, new SpriteAnimSet(p_Texture, p_Size, p_Frames, p_FrameRate)));
        }

    }

    public struct SpriteInstance
    {
        public Sprite Sprite;
        public Vector2 Location;

        public SpriteInstance(Sprite p_Sprite, Vector2 p_Location)
        {
            this.Sprite = p_Sprite;
            this.Location = p_Location;
        }
    }
}
