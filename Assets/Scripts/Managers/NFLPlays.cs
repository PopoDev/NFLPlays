using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Managers
{
    public static class NFLPlays
    {
        // Scale factor relative to field size
        public const float ScaleFactorField = 5f;
        public const int NumFootballPlayers = 11;
        
        private static List<Player> _offensivePlayers = new();
        private static List<Player> _defensivePlayers = new();

        /**
         * Set the players from the markers
         * @param offensive Offensive player markers
         * @param defensive Defensive player markers
         * @param origin Origin of the whiteboard
         * @param length Length of the whiteboard
         * @param width Width of the whiteboard
         */
        public static void SetPlayersFromMarkers(List<PlayerMarker> offensive, List<PlayerMarker> defensive, Vector2 origin, float length, float width)
        {
            _offensivePlayers = offensive.Select(player => new Player(player.position, ComputeRelativePosition(player.GetPositionCenter(), origin, length, width), player.Moves)).ToList();
            _defensivePlayers = defensive.Select(player => new Player(player.position, ComputeRelativePosition(player.GetPositionCenter(), origin, length, width), player.Moves)).ToList();
        }
        
        public static List<Player> GetOffensivePlayers()
        {
            if (_offensivePlayers.Count != NumFootballPlayers) throw new Exception("Offensive players not set");
            return _offensivePlayers;
        }
        
        public static List<Player> GetDefensivePlayers()
        {
            if (_defensivePlayers.Count != NumFootballPlayers) throw new Exception("Defensive players not set");
            return _defensivePlayers;
        }

        /**
         * Get the relative position of the player on the screen
         * Screen: x = horizontal, y = vertical
         * @param position absolute position on screen
         * @param origin Origin of the whiteboard
         * @param length Length of the whiteboard
         * @param width Width of the whiteboard
         * @return relative position of the player on the screen
         */
        private static Vector2 ComputeRelativePosition(Vector2 position, Vector2 origin, float length, float width)
        {
            return new Vector2((position.x - origin.x) / length, (position.y - origin.y) / width);
        }
    }
}