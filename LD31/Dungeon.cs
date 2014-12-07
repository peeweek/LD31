using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LD31
{
    public class Dungeon
    {
        public Player Player;
        public byte Difficulty;

        public byte RemainingCrystals;
        public Room CurrentRoom;
        public HUD HUD;
        private DungeonState State;

        public Dungeon()
        {
            
        }

        public void Initialize(ContentManager p_Content)
        {
            LD31.Dungeon = this;
            this.RemainingCrystals = 5;
            this.Player = new Player(this);
            this.InitializeFirstRoom();
            this.Player.Initialize(p_Content, new Vector2(this.CurrentRoom.Bounds.X+48.0f, this.CurrentRoom.Bounds.Y+48.0f));
            this.HUD = new HUD(this);
            HUD.Initialize(p_Content);
            this.State = DungeonState.Running;
        }

        public void InitializeFirstRoom()
        {
            this.CurrentRoom = new Room(Player, RoomExit.Null, Difficulty );
            LD31.Game.SetWindowBounds(this.CurrentRoom.Bounds);
        }


        public void CreateNewRoom(RoomExit p_Exit)
        {
            if (Difficulty < 255) Difficulty++;
            this.CurrentRoom = new Room(Player, p_Exit, Difficulty);
            this.Player.SetLocation(new Vector2(p_Exit.X * Globals.CELLSIZE + 16.0f, p_Exit.Y * Globals.CELLSIZE + 16.0f));
            LD31.Game.SetWindowBounds(this.CurrentRoom.Bounds);
            
        }

        public void PickUpCrystal()
        {
            this.CurrentRoom.Crystal = null;
            if (this.RemainingCrystals == 1)
            {
                this.Completed();
                LD31.GameAudio.SetMusic("Victory");
            }
            else
            {
                this.RemainingCrystals--;
                LD31.GameAudio.PlaySFX("Validate");
            }
        }

        public void PickUpHeart()
        {
            this.CurrentRoom.Heart = null;
            if (this.Player.Life < 255)
            {
                this.Player.Life++;
                LD31.GameAudio.PlaySFX("Validate");
            }
            else LD31.GameAudio.PlaySFX("Select");
        }

        public void Update(float p_DeltaTime)
        {
            switch (this.State)
            {
                case DungeonState.Running: this.CurrentRoom.Update(p_DeltaTime);
                    if (LD31.Input.Pause == InputState.JustPressed) this.State = DungeonState.Pause;
                    break;
                case DungeonState.GameOver: if (LD31.Input.A == InputState.JustPressed) LD31.Game.MainMenu(); break;
                case DungeonState.Completed: if (LD31.Input.A == InputState.JustPressed) LD31.Game.MainMenu(); break;
                case DungeonState.Pause: if (LD31.Input.Pause == InputState.JustPressed) this.State = DungeonState.Running;
                    else if (LD31.Input.A == InputState.JustPressed) LD31.Game.MainMenu();
                    break;
                default: break;
            }
            

        }


        public void GameOver()
        {
            LD31.GameAudio.SetMusic("GameOver");
            this.State = DungeonState.GameOver;
        }

        public void Completed()
        {
            LD31.GameAudio.SetMusic("Victory");
            this.State = DungeonState.Completed;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            this.CurrentRoom.Draw(sb);
            Player.Draw(CurrentRoom.Bounds, sb);
            this.HUD.Draw(sb, this.State);
            sb.End();
        }
    }

    public enum DungeonState
    {
        Running,
        Completed,
        GameOver,
        Pause
    }
}
