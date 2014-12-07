using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace LD31
{
    public class GameAudio
    {
        private SoundEffect m_MenuMusic;
        private SoundEffect m_GameMusic;
        private SoundEffect m_GameOverMusic;
        private SoundEffect m_VictoryMusic;

        private SoundEffect m_Select;
        private SoundEffect m_Validade;
        private SoundEffect m_Hurt;

        private SoundEffectInstance m_CurrentMusic;

        public GameAudio()
        {
            
        }

        public void Initialize(ContentManager p_Content)
        {
            this.m_MenuMusic = p_Content.Load<SoundEffect>("Audio\\MenuMusic");
            this.m_GameMusic = p_Content.Load<SoundEffect>("Audio\\GameMusic");
            this.m_GameOverMusic = p_Content.Load<SoundEffect>("Audio\\GameOverMusic");
            this.m_VictoryMusic = p_Content.Load<SoundEffect>("Audio\\VictoryMusic");
            this.m_Select = p_Content.Load<SoundEffect>("Audio\\SelectSFX");
            this.m_Validade = p_Content.Load<SoundEffect>("Audio\\ValidateSFX");
            this.m_Hurt = p_Content.Load<SoundEffect>("Audio\\HurtSFX");
        }

        public void SetMusic(string name)
        {
            if(this.m_CurrentMusic != null) this.m_CurrentMusic.Stop();

            switch (name)
            {
                case "Menu": this.m_CurrentMusic = this.m_MenuMusic.CreateInstance();
                    this.m_CurrentMusic.IsLooped = true;
                    this.m_CurrentMusic.Play();
                    break;
                case "Game": this.m_CurrentMusic = this.m_GameMusic.CreateInstance();
                    this.m_CurrentMusic.IsLooped = true;
                    this.m_CurrentMusic.Play();
                    break;
                case "GameOver": this.m_CurrentMusic = this.m_GameOverMusic.CreateInstance();
                    this.m_CurrentMusic.IsLooped = false;
                    this.m_CurrentMusic.Play();
                    break;
                case "Victory": this.m_CurrentMusic = this.m_VictoryMusic.CreateInstance();
                    this.m_CurrentMusic.IsLooped = false;
                    this.m_CurrentMusic.Play();

                    break;
                
                default: break;
            }

        }


        public void PlaySFX(string name)
        {
            switch (name)
            {
                case "Hurt": this.m_Hurt.Play();
                    break;
                case "Select": this.m_Select.Play();
                                    break;
                case "Validate": this.m_Validade.Play();
                                    break;

                default: break;
            }
        }


    }
}
