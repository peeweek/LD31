using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace LD31
{
    public class Room
    {
        public Rectangle Bounds;
        public byte[,] data;

        public List<SpriteInstance> SpriteInstances;
        public List<Rectangle> Collisions;
        private Player m_Player;

        public Crystal Crystal;
        public Heart Heart;


        private List<RoomExit> m_RoomExits;

        private List<Monster> m_Monsters;

        public Room(Player p_Player, RoomExit p_LastRoomExit, byte p_Difficulty)
        {
            this.m_RoomExits = new List<RoomExit>();
            this.m_Player = p_Player;
            this.m_Monsters = new List<Monster>();
            Rectangle r;
            byte numX, numY, minX, minY;

            // WANTED SIZE (WILL BE REDUCED LATER IF GOING OUT OF SCREEN
            numX = (byte)Utils.RNG.Next(Globals.MIN_ROOM_WIDTH, Globals.MAX_ROOM_WIDTH);
            numY = (byte)Utils.RNG.Next(Globals.MIN_ROOM_HEIGHT, Globals.MAX_ROOM_HEIGHT);

            if (p_LastRoomExit.ExitLocation == Cardinals.Null)
            {
                // INITIALIZATION
                minX =  (byte)Utils.RNG.Next(Globals.MIN_ROOM_WIDTH, Globals.MAX_ROOM_WIDTH);
                minY =  (byte)Utils.RNG.Next(Globals.MIN_ROOM_HEIGHT, Globals.MAX_ROOM_HEIGHT);

                // RESIZE IF OUT OF SCREEN
                if (numX + minX > Globals.SCREEN_MAX_CELLS_X) numX = (byte)(Globals.SCREEN_MAX_CELLS_X - minX);
                if (numY + minY > Globals.SCREEN_MAX_CELLS_Y) numY = (byte)(Globals.SCREEN_MAX_CELLS_Y - minY);

            }
            else
            {
                
                switch (p_LastRoomExit.ExitLocation)
                {
                    case Cardinals.North: 
                        r = Utils.GetRect(0, (byte)Globals.SCREEN_MAX_CELLS_X, 0, p_LastRoomExit.Y, p_LastRoomExit.X, p_LastRoomExit.Y, Cardinals.North);
       
                        break;
                    case Cardinals.South:
                        r = Utils.GetRect(0, (byte)Globals.SCREEN_MAX_CELLS_X, p_LastRoomExit.Y, (byte)Globals.SCREEN_MAX_CELLS_Y, p_LastRoomExit.X, p_LastRoomExit.Y, Cardinals.South);
       
                        break;
                    case Cardinals.East:
                        r = Utils.GetRect(p_LastRoomExit.X, (byte)Globals.SCREEN_MAX_CELLS_X, 0, (byte)Globals.SCREEN_MAX_CELLS_Y, p_LastRoomExit.X, p_LastRoomExit.Y, Cardinals.East);
       
                        break;
                    case Cardinals.West:
                        r = Utils.GetRect(0, p_LastRoomExit.X, 0, (byte)Globals.SCREEN_MAX_CELLS_Y, p_LastRoomExit.X, p_LastRoomExit.Y, Cardinals.West);
       
                        break;
                    default: r = Rectangle.Empty; break;
                }

                minX = (byte)r.X;
                minY = (byte)r.Y;
                numX = (byte)r.Width;
                numY = (byte)r.Height;
            }


            this.Bounds = new Rectangle(minX * Globals.CELLSIZE, minY * Globals.CELLSIZE, numX * Globals.CELLSIZE, numY * Globals.CELLSIZE);

            ///////////////////////////////////////////////////////////////
            // Filling Ground Data & Walls
            ///////////////////////////////////////////////////////////////
            
            data = new byte[numX, numY];
            for(ushort i = 0 ; i < numX; i++)
                for (ushort j = 0; j < numY; j++)
                {
                    if (i == 0 || i == numX - 1 || j == 0 || j == numY - 1)
                    data[i, j] = (byte)Utils.RNG.Next(10, 13); // Wall
                    else data[i, j] = (byte)Utils.RNG.Next(0, 6); // Ground
                }
            ///////////////////////////////////////////////////////////////
            // .. Gaps to Other Rooms
            ///////////////////////////////////////////////////////////////

            // .. first the last room's exit
            if (p_LastRoomExit.ExitLocation != Cardinals.Null)
            {
                switch (p_LastRoomExit.ExitLocation)
                {
                    case Cardinals.North: this.m_RoomExits.Add(new RoomExit(Cardinals.South, (byte)(p_LastRoomExit.X), (byte)(p_LastRoomExit.Y+1)));
                        data[p_LastRoomExit.X - minX, p_LastRoomExit.Y- minY] = (byte)Utils.RNG.Next(0, 5);
                        break;
                    case Cardinals.South: this.m_RoomExits.Add(new RoomExit(Cardinals.North, (byte)(p_LastRoomExit.X), (byte)(p_LastRoomExit.Y-1)));
                        data[p_LastRoomExit.X - minX, p_LastRoomExit.Y- minY] = (byte)Utils.RNG.Next(0, 5);
                        break;
                    case Cardinals.East: this.m_RoomExits.Add(new RoomExit(Cardinals.West, (byte)(p_LastRoomExit.X-1), (byte)(p_LastRoomExit.Y)));
                        data[p_LastRoomExit.X- minX, p_LastRoomExit.Y - minY] = (byte)Utils.RNG.Next(0, 5);
                        break;
                    case Cardinals.West: this.m_RoomExits.Add(new RoomExit(Cardinals.East, (byte)(p_LastRoomExit.X+1), (byte)(p_LastRoomExit.Y)));
                        data[p_LastRoomExit.X- minX, p_LastRoomExit.Y - minY] = (byte)Utils.RNG.Next(0, 5);
                        break;
                    default: break;
                }
                
                
            }

            // .. then other exits, if relevant
            byte vCurrent;
            if (p_LastRoomExit.ExitLocation != Cardinals.South)
            {
                if (Utils.RNG.Next(Globals.ROOM_MAX_CHANCE) < Globals.ROOM_CHANCE && minY > Globals.MIN_ROOM_HEIGHT) //Chance GAP NORTH
                {
                    vCurrent = (byte)Utils.RNG.Next(1, numX - 2);
                    data[vCurrent, 0] = (byte)Utils.RNG.Next(0, 5);
                    this.m_RoomExits.Add(new RoomExit(Cardinals.North, (byte)(vCurrent + minX), (byte)(minY - 1)));
                }
            }

            if (p_LastRoomExit.ExitLocation != Cardinals.North)
            {
                if (Utils.RNG.Next(Globals.ROOM_MAX_CHANCE) < Globals.ROOM_CHANCE && minY+numY+Globals.MIN_ROOM_HEIGHT < Globals.SCREEN_MAX_CELLS_Y) // Chance GAP SOUTH
                {
                    vCurrent = (byte)Utils.RNG.Next(1, numX - 2);
                    data[vCurrent, numY - 1] = (byte)Utils.RNG.Next(0, 5);
                    this.m_RoomExits.Add(new RoomExit(Cardinals.South, (byte)(vCurrent + minX), (byte)(minY+numY+1)));
                }
            }

            if (p_LastRoomExit.ExitLocation != Cardinals.East)
            {
                if (Utils.RNG.Next(Globals.ROOM_MAX_CHANCE) < Globals.ROOM_CHANCE && minX > Globals.MIN_ROOM_WIDTH) // Chance GAP WEST
                {
                    vCurrent = (byte)Utils.RNG.Next(1, numY - 2);
                    data[0, vCurrent] = (byte)Utils.RNG.Next(0, 5);
                    this.m_RoomExits.Add(new RoomExit(Cardinals.West, (byte)(minX - 1), (byte)(vCurrent + minY)));
                }
            }

            if (p_LastRoomExit.ExitLocation != Cardinals.West)
            {
                if (Utils.RNG.Next(Globals.ROOM_MAX_CHANCE) < Globals.ROOM_CHANCE && minX + numX + Globals.MIN_ROOM_WIDTH < Globals.SCREEN_MAX_CELLS_X) // Chance GAP EAST
                {
                    vCurrent = (byte)Utils.RNG.Next(1, numY - 2);
                    data[numX - 1, vCurrent] = (byte)Utils.RNG.Next(0, 5);
                    this.m_RoomExits.Add(new RoomExit(Cardinals.East, (byte)(minX + numX + 1), (byte)(vCurrent + minY)));
                }
            }

            
                ////////////////////////////////////////////////////////////////////////////
                // .. Props & Pickups (Hearts? Crystals?)                                 //
                ////////////////////////////////////////////////////////////////////////////
                if (Utils.RNG.Next(255) < Globals.CRYSTAL_CHANCE + (byte)(p_Difficulty / 16))
                {
                    this.Crystal = new Crystal();
                    this.Crystal.Initialize(new Vector2(this.Bounds.X + this.Bounds.Width / 2, this.Bounds.Y + this.Bounds.Height / 2));
                }
                else if (Utils.RNG.Next(255) < Globals.HEART_CHANCE + (byte)(p_Difficulty / 8))
                {
                    this.Heart = new Heart();
                    this.Heart.Initialize(new Vector2(this.Bounds.X + this.Bounds.Width / 2, this.Bounds.Y + this.Bounds.Height / 2));
                }


                ////////////////////////////////////////////////////////////////////////////
                // .. Enemies                                                             //
                ////////////////////////////////////////////////////////////////////////////
                int minCount = (int)Math.Min(Math.Round((float)(p_Difficulty + Utils.RNG.Next(5)) / 8.0f), Globals.MAX_ENEMIES);
                int maxCount = minCount + 3;

                int monstercount = Utils.RNG.Next(minCount, maxCount);
                while (monstercount != 0)
                {
                    this.m_Monsters.Add(LD31.MonsterFactory.Spawn((byte)Utils.RNG.Next(0, 2), this.m_Player.Dungeon.Difficulty, this.Bounds));
                    monstercount--;
                }

            

            ////////////////////////////////////////////////////////////////////////////
            //                      THEN, BUILDING THE ROOM                           //
            ////////////////////////////////////////////////////////////////////////////
            this.Collisions = new List<Rectangle>();
            this.SpriteInstances = new List<SpriteInstance>();
            for (ushort i = 0; i < numX; i++)
                for (ushort j = 0; j < numY; j++)
                {
                    if (data[i, j] >= 10 && data[i, j] < 20) this.Collisions.Add(new Rectangle((i + minX) * Globals.CELLSIZE, (j + minY) * Globals.CELLSIZE, LD31.SpriteLibrary.Sprites[data[i, j]].Collision.Width, LD31.SpriteLibrary.Sprites[data[i, j]].Collision.Height)); 
                    this.SpriteInstances.Add(new SpriteInstance(LD31.SpriteLibrary.Sprites[data[i, j]], new Vector2((i+minX)*Globals.CELLSIZE,(j+minY)*Globals.CELLSIZE )));
                }
            
            
        }

        public void Update(float p_DeltaTime)
        {
            float dashFactor= 1.0f;
            if (LD31.Input.A == InputState.JustPressed || LD31.Input.A == InputState.Pressed)
            {
                dashFactor = Globals.DASH_FACTOR;
            }

            Vector2 vDirection = new Vector2(p_DeltaTime * LD31.Input.Direction.X * Globals.PLAYER_SPEED * dashFactor, p_DeltaTime * LD31.Input.Direction.Y * Globals.PLAYER_SPEED * dashFactor);

            foreach (Rectangle i_Collision in this.Collisions)
            {
                vDirection = Utils.Collide(m_Player.CollisionBounds, i_Collision, vDirection);
            }

            foreach (RoomExit i_Exit in this.m_RoomExits)
            {
                if (m_Player.CollisionBounds.Intersects(i_Exit.Collision) && !m_Player.CollisionBounds.Intersects(this.Bounds))
                {
                    m_Player.Dungeon.CreateNewRoom(i_Exit);
                }
            }

            foreach (Monster i_Monster in this.m_Monsters)
            {
                i_Monster.Update(p_DeltaTime, this.m_Player);
                if(!this.m_Player.IsInvincible)
                if (i_Monster.CollisionBounds.Intersects(this.m_Player.CollisionBounds))
                {
                    this.m_Player.Hurt();
                }
            }

            if (this.Crystal != null) Crystal.Update(p_DeltaTime, this.m_Player);
            if (this.Heart != null) Heart.Update(p_DeltaTime, this.m_Player);

            m_Player.Update(p_DeltaTime, vDirection);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (SpriteInstance i_Sprite in this.SpriteInstances)
            {
                i_Sprite.Sprite.Draw(sb, i_Sprite.Location, this.Bounds);
            }

            foreach (Monster i_Monster in this.m_Monsters)
            {
                i_Monster.Draw(this.Bounds, sb);
            }
            if (this.Crystal != null) Crystal.Draw(this.Bounds, sb);
            if (this.Heart != null) Heart.Draw(this.Bounds, sb);
        }
    }


    public struct RoomExit
    {
        public readonly Cardinals ExitLocation;
        public readonly Rectangle Collision;
        public readonly byte X;
        public readonly byte Y;

        /// <summary>
        /// RoomExitInformation, provides information about a new cell next to the opening that will serve as a new room's exit
        /// </summary>
        /// <param name="p_ExitLocation">whether this is a north, south, etc. exit from the current room</param>
        /// <param name="p_X">Dungeon-coord cell coordinate X</param>
        /// <param name="p_Y">Dungeon-coord cell coordinate Y</param>
        public RoomExit(Cardinals p_ExitLocation, byte p_X, byte p_Y)
        {
            this.ExitLocation = p_ExitLocation;
            this.X = p_X;
            this.Y = p_Y;
            Collision = new Rectangle(p_X * Globals.CELLSIZE, p_Y * Globals.CELLSIZE, Globals.CELLSIZE, Globals.CELLSIZE);
        }

        public static RoomExit Null { get { return new RoomExit(Cardinals.Null, 0, 0); } }
    }







}


