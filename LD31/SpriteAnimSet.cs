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
    public class SpriteAnimSet
    {
        private Texture2D m_ReferenceTexture;
        public Dictionary<string, LD31SpriteAnimation> Animations;
        public ushort CellSize { get { return m_CellSize; } }
        private ushort m_CellSize;
        private ushort m_numX;
        private ushort m_numY;


        public SpriteAnimSet(Texture2D p_ReferenceTexture, ushort p_CellSize)
        {
            this.m_ReferenceTexture = p_ReferenceTexture;
            this.m_CellSize = p_CellSize;
            this.Animations = new Dictionary<string, LD31SpriteAnimation>();
            this.m_numX = (ushort)Math.Floor((double)p_ReferenceTexture.Width / p_CellSize);
            this.m_numY = (ushort)Math.Floor((double)p_ReferenceTexture.Height / p_CellSize);
            
        }

        /// <summary>
        /// Proxy for default sprites (no animation)
        /// </summary>
        /// <param name="p_ReferenceTexture"></param>
        /// <param name="p_CellSize"></param>
        /// <param name="p_DefaultFrame"></param>
        public SpriteAnimSet(Texture2D p_ReferenceTexture, ushort p_CellSize, byte p_DefaultFrame)
        {
            this.m_ReferenceTexture = p_ReferenceTexture;
            this.m_CellSize = p_CellSize;
            this.Animations = new Dictionary<string, LD31SpriteAnimation>();
            this.m_numX = (ushort)Math.Floor((double)p_ReferenceTexture.Width / p_CellSize);
            this.m_numY = (ushort)Math.Floor((double)p_ReferenceTexture.Height / p_CellSize);
            this.Animations.Add("default", new LD31SpriteAnimation(1, new byte[1] { p_DefaultFrame }));
        }
        /// <summary>
        /// Proxy for default animated sprites (single animation)
        /// </summary>
        /// <param name="p_ReferenceTexture"></param>
        /// <param name="p_CellSize"></param>
        /// <param name="p_DefaultFrames"></param>
        /// <param name="p_FrameRate"></param>
        public SpriteAnimSet(Texture2D p_ReferenceTexture, ushort p_CellSize, byte[] p_DefaultFrames, byte p_FrameRate)
        {
            this.m_ReferenceTexture = p_ReferenceTexture;
            this.m_CellSize = p_CellSize;
            this.Animations = new Dictionary<string, LD31SpriteAnimation>();
            this.m_numX = (ushort)Math.Floor((double)p_ReferenceTexture.Width / p_CellSize);
            this.m_numY = (ushort)Math.Floor((double)p_ReferenceTexture.Height / p_CellSize);
            this.Animations.Add("default", new LD31SpriteAnimation(p_FrameRate, p_DefaultFrames));
        }
        

        /// <summary>
        /// Gets the frame rect
        /// </summary>
        /// <param name="AnimationName"></param>
        /// <param name="p_Time"></param>
        /// <returns></returns>
        public Rectangle GetAnimationRect(string AnimationName, float p_Time)
        {
            ushort v_FrameNumber = Animations[AnimationName].GetFrame(p_Time);
            return new Rectangle(
                (v_FrameNumber % m_numX) * m_CellSize,
                ((ushort)Math.Floor((double)v_FrameNumber / m_numX)) * m_CellSize,
                m_CellSize,
                m_CellSize
                );
        }

        public void Add(string p_AnimationName, byte p_Framerate, byte[] p_Framenumbers)
        {
            this.Animations.Add(p_AnimationName, new LD31SpriteAnimation(p_Framerate, p_Framenumbers));
        }
    }

    public class LD31SpriteAnimation {

        private byte[] m_Frames;
        private float m_Interval;

        public LD31SpriteAnimation(byte p_Framerate, byte[] p_Frames) {
            this.m_Frames = p_Frames;
            this.m_Interval = 1.0f/(float)p_Framerate;
        }

        public ushort GetFrame(float p_Time)
        {
            return m_Frames[(int)Math.Floor(p_Time / m_Interval) % (m_Frames.Length)];
        }

    }


}
