using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD31
{
    public static class Utils
    {
        /// <summary>
        /// Collider Test, adjusts a given direction, given a mover and a collider.
        /// </summary>
        /// <param name="Mover">Moving object bounds</param>
        /// <param name="Collider">Collider bounds</param>
        /// <param name="Direction">Initial direction intent</param>
        /// <returns>The Simulated Vector based on collision on the Collider.</returns>
        public static float Collide(Rectangle Mover, Rectangle Collider, Vector2 Direction)
        {
            #region OLD COLLISION ALGORITHM 
            /*bool collideVertical = false;
            Rectangle vSimulated = new Rectangle(Mover.X + (int)Direction.X, Mover.Y + (int)Direction.Y, Mover.Width, Mover.Height);
            if (vSimulated.Intersects(Collider))
            {
                if (Mover.Top + Result.Y < Collider.Bottom) { Result.Y = 0.0f; collideVertical = true; }
                else if (Mover.Bottom + Result.Y > Collider.Top) { Result.Y = 0.0f; collideVertical = true; } 
                
                if (Mover.Left + Result.X < Collider.Right && collideVertical) Result.X = 0.0f; 
                else if (Mover.Right + Result.X > Collider.Left && collideVertical) Result.X = 0.0f;
            }*/
            #endregion

            Vector2 Source = new Vector2(Mover.X + Mover.Width / 2, Mover.Y + Mover.Height / 2);
            Vector2 Target = Source + Direction;

            Rectangle vBroadPhaseSource = new Rectangle((int)Source.X, (int)Source.Y, (int)(Target.X - Source.X), (int)(Target.Y - Source.Y));

            if (vBroadPhaseSource.Intersects(Collider))
            {
                //Rectangle vExpCollider = new Rectangle( Collider.X - Mover.Width / 2, Collider.Y - Mover.Height / 2, Collider.Width + Mover.Width / 2, Collider.Height + Mover.Height / 2 );
                Rectangle vExpCollider = Collider;
                vExpCollider.Inflate(Mover.Width / 2, Mover.Height / 2);

                // Line Testing : based on zachamarz's http://gamedev.stackexchange.com/questions/18436/most-efficient-aabb-vs-ray-collision-algorithms/18459#18459

                Vector2 vDirFrac = new Vector2(1.0f / Direction.X, 1.0f / Direction.Y);
                float t1 = (vExpCollider.Left - Source.X) * vDirFrac.X;
                float t2 = (vExpCollider.Right - Source.X) * vDirFrac.X;
                float t3 = (vExpCollider.Top - Source.Y) * vDirFrac.Y;
                float t4 = (vExpCollider.Bottom - Source.Y) * vDirFrac.Y;

                float tMin = Math.Max(Math.Min(t1, t2), Math.Min(t3, t4));
                float tMax = Math.Min(Math.Max(t1, t2), Math.Max(t3, t4));

                if (tMax < 0.0f) // Collision is behind us 
                    return 1.0f;
                if (tMin > tMax && tMin < 0.0f) // No Intersection whatsoever
                    return 1.0f;
                return tMin;
            }

            return 1.0f;
        }

        /// <summary>
        /// Collision Response along with the direction
        /// </summary>
        /// <param name="pTime"></param>
        /// <param name="Mover"></param>
        /// <param name="Collider"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
        public static Vector2 RespondCollision(float pTime, Rectangle Mover, Rectangle Collider, Vector2 Direction)
        {
            Vector2 Result = Direction * pTime;
            Vector2 Slide = Vector2.Zero;

            Result += Slide;

            return Result;
        }


        // The RNG for the game, one is enough.
        public static Random RNG = new Random();

        /// <summary>
        /// Gets a standard rect including XY (according to rules)
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static Rectangle GetRect(byte p_minX, byte p_maxX, byte p_minY, byte p_maxY, byte p_X, byte p_Y, Cardinals p_Cardinals)
        {
            
            int minX, maxX, minY, maxY, X, Y;
            minX = p_minX;
            maxX = p_maxX;
            minY = p_minY;
            maxY = p_maxY;
            X = p_X;
            Y = p_Y;
            int desiredwidth = Utils.RNG.Next(Globals.MIN_ROOM_WIDTH, Globals.MAX_ROOM_WIDTH);
            int desiredheight = Utils.RNG.Next(Globals.MIN_ROOM_HEIGHT, Globals.MAX_ROOM_HEIGHT);

            int top, left, right, bottom;

            switch (p_Cardinals)
            {
                case Cardinals.North:
                        bottom = 0;
                        top = desiredheight - bottom;
                        left = Utils.RNG.Next(1, desiredwidth - 1);
                        right = desiredwidth - left;
                    break;
                case Cardinals.South:
                        top = 0;
                        bottom = desiredheight - top;
                        left = Utils.RNG.Next(1, desiredwidth-1);
                        right = desiredwidth - left;
                    break;
                case Cardinals.West:
                        top = Utils.RNG.Next(1, desiredheight - 1);
                        bottom = desiredheight - top;
                        right = 0;
                        left = desiredwidth - right;
                    break;
                case Cardinals.East:
                        top = Utils.RNG.Next(1, desiredheight - 1);
                        bottom = desiredheight - top;

                        left = 0;
                        right = desiredwidth - left;
                    break;
                default: 
                        top = Utils.RNG.Next(1, desiredheight - 1);
                        bottom = desiredheight - top;
                        left = Utils.RNG.Next(1, desiredwidth - 1);
                        right = desiredwidth - left;
                    break;

            }


            minY = Math.Max(minY, Y-top);
            maxY = Math.Min(maxY,Y +bottom);
            minX = Math.Max(minX,X-left);
            maxX = Math.Min(maxX,X+right);

            int width = maxX-minX+1;
            int height = maxY - minY + 1;

            return new Rectangle(minX, minY, width, height);
        }

    }
}
