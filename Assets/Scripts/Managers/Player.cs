using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Player
    {
        public string Position { get; }
        public Vector3 Location { get; set; }  // Relative location
        
        public LinkedList<PlayerMove> Moves { get; }

        public Player(string position, Vector3 location, LinkedList<PlayerMove> moves)
        {
            Position = position;
            Location = location;
            Moves = moves;
        }
        
        /**
         * Get the absolute position of the player on the field
         * Field: x = vertical up, y = height, z = horizontal right
         * Need to flip the x axis
         * @param length Length of the field
         * @param width Width of the field
         * @return absolute position of the player on the field
         */
        public Vector3 GetFieldLocation(float length, float width)
        {
            return new Vector3(-Location.y * length, 0, Location.x * width) * NFLPlays.ScaleFactorField;
        }
    }
}