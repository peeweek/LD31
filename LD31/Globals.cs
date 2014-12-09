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

        // ROOM GENERATION CONSTANTS
        public static ushort CELLSIZE = 32;
        public static byte ROOM_CHANCE = 180;
        public static byte ROOM_MAX_CHANCE = 255;
        public static byte CRYSTAL_CHANCE = 20;
        public static byte HEART_CHANCE = 80;
        public static byte MAX_ENEMIES = 20;

        public static ushort SMALLROOM_MAX_SIZE = 5;
        public static ushort SMALLROOM_MAX_ENEMIES = 5;
        public static ushort CORRIDOR_MAX_SIZE = 5;
        public static ushort CORRIDOR_MAX_ENEMIES = 3;
        public static ushort BIGROOM_MIN_SIZE = 60;
        public static ushort BIGROOM_OBSTACLE_SPAWN_CHANCE = 5;
        
        
        // Min / Max room size in tiles (1 tile = 16px)
        public static ushort MIN_ROOM_WIDTH = 4;
        public static ushort MIN_ROOM_HEIGHT = 4;
        public static ushort MAX_ROOM_WIDTH = 20;
        public static ushort MAX_ROOM_HEIGHT = 20;
        public static float MIN_ENEMY_SPAWN_DISTANCE = 90.0f;

        

        // Player
        public static float DASH_FACTOR = 2.5f;
        public static float PLAYER_SPEED = 200.0f;
        // Collisions
        public static float SLOWDOWN_FACTOR = 0.2f;
    }

    public enum Cardinals { North, South, East, West, Null}
}
