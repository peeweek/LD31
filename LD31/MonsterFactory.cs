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
    public class MonsterFactory
    {
        private Dictionary<byte, MonsterTemplate> Templates;

        public MonsterFactory()
        {
            this.Templates = new Dictionary<byte, MonsterTemplate>();
        }

        public void Initialize()
        {
        
            this.Templates.Add(0, new MonsterTemplate(1,5,60.0f, 260.0f, 50));
            this.Templates.Add(1, new MonsterTemplate(3,15,120.0f, 340.0f, 51));
        }

        public Monster Spawn(byte id, byte difficulty, Rectangle gameArea )
        {
            Monster vOut = new Monster(this.Templates[id].getHealth(difficulty), this.Templates[id].getSpeed(difficulty));


            Vector2 vLoc = new Vector2(
                gameArea.X + (int)(1.5f*Globals.CELLSIZE) + (Utils.RNG.Next(gameArea.Width - (int)(3.0f * Globals.CELLSIZE))),
                gameArea.Y + (int)(1.5f*Globals.CELLSIZE)+ (Utils.RNG.Next(gameArea.Height - (int)(3.0f * Globals.CELLSIZE)))
                ); 

            vOut.Initialize(this.Templates[id].SpriteIndex, vLoc);

            return vOut;
        }
    }

    public struct MonsterTemplate
    {
        public byte MinHealth;
        public byte MaxHealth;
        public float MinSpeed;
        public float MaxSpeed;
        public byte SpriteIndex;

        public MonsterTemplate(byte p_MinHealth, byte p_MaxHealth, float p_MinSpeed, float p_MaxSpeed, byte p_SpriteIndex)
        {
            this.MinHealth = p_MinHealth;
            this.MaxHealth = p_MaxHealth;
            this.MinSpeed = p_MinSpeed;
            this.MaxSpeed = p_MaxSpeed;

            this.SpriteIndex = p_SpriteIndex;
        }


        public float getSpeed(byte difficulty)
        {
            return MinSpeed + (((float)difficulty / 255.0f) * (MaxSpeed - MinSpeed));
        }

        public byte getHealth(byte difficulty)
        {
            return (byte)Math.Floor((float)MinHealth + (((float)difficulty / 255.0f) * ((float)MaxHealth - (float)MinHealth)));
        }


    }
}
