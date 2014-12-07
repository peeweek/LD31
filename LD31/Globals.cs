using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD31
{
    public static class Globals
    {
        // Screen Wi/hi for the game
        public static ushort SCREEN_WIDTH = 0;
        public static ushort SCREEN_HEIGHT = 0;

        public static ushort SCREEN_MAX_CELLS_X { get { return (ushort)((Globals.SCREEN_WIDTH / Globals.CELLSIZE)-2); } }
        public static ushort SCREEN_MAX_CELLS_Y { get { return (ushort)((Globals.SCREEN_HEIGHT / Globals.CELLSIZE)-2); } }

        // Min / Max room size in tiles (1 tile = 16px)
        public static ushort MIN_ROOM_WIDTH = 4;
        public static ushort MIN_ROOM_HEIGHT = 4;
        public static ushort MAX_ROOM_WIDTH = 20;
        public static ushort MAX_ROOM_HEIGHT = 20;

        //Game Constants
        public static ushort CELLSIZE = 32;
        public static byte ROOM_CHANCE = 180;
        public static byte ROOM_MAX_CHANCE = 255;

        public static byte CRYSTAL_CHANCE = 20;
        public static byte HEART_CHANCE = 80;
        public static byte MAX_ENEMIES = 20;
        public static float DASH_FACTOR = 2.5f;
        public static float PLAYER_SPEED = 200.0f;
    }

    public enum Cardinals { North, South, East, West, Null}
}
