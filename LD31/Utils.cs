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
        /// <returns></returns>
        public static Vector2 Collide(Rectangle Mover, Rectangle Collider, Vector2 Direction)
        {
            Vector2 Result = Direction;
            bool collideVertical = false;
            Rectangle vSimulated = new Rectangle(Mover.X + (int)Direction.X, Mover.Y + (int)Direction.Y, Mover.Width, Mover.Height);
            if (vSimulated.Intersects(Collider))
            {
                if (Mover.Top + Result.Y < Collider.Bottom) { Result.Y = 0.0f; collideVertical = true; }
                else if (Mover.Bottom + Result.Y > Collider.Top) { Result.Y = 0.0f; collideVertical = true; } 
                
                if (Mover.Left + Result.X < Collider.Right && collideVertical) Result.X = 0.0f; 
                else if (Mover.Right + Result.X > Collider.Left && collideVertical) Result.X = 0.0f; 
            }
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
