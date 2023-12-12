using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Simulation
{
    public class PlayersManager: MonoBehaviour
    {
        private bool _debug = true;
        public GameObject playerOffencePrefab;
        public GameObject playerDefencePrefab;
        public GameObject field;
        public GameObject ball;

        private static Dictionary<GameObject, LinkedList<PlayerMove>> _playerMoves = new();

        public const float InitTime = 4.5f;  // Initial animations time
        public static float MaxTime;

        private void Start()
        {
            Debug.Log("PlayersManager enabled");
            Vector3 size = field.transform.localScale;
            
            // Field: x = length (vertical), y = height, z = width (horizontal)
            float length = size.x;
            float width = size.z;

            var offensivePlayers = NFLPlays.GetOffensivePlayers();
            var defensivePlayers = NFLPlays.GetDefensivePlayers();
            
            //var ballObject = Instantiate(ball, new Vector3(0, 2f, 0), Quaternion.identity);
            
            offensivePlayers.ForEach(player =>
            {
                var playerObject = Instantiate(playerOffencePrefab, player.GetFieldLocation(length, width), Quaternion.Euler(0, 90, 0));
                playerObject.SetActive(true);
                playerObject.name = player.Position;
                if (player.Position == "QB")
                {
                    Transform[] children = playerObject.GetComponentsInChildren<Transform>();
                    foreach (Transform child in children)
                    {
                        if (child.name == "mixamorig_RightHand")
                        {
                            ball.transform.parent = child;
                            ball.transform.localPosition = new Vector3(0.2f, 2.0f, 1.0f);
                            ball.transform.localRotation = Quaternion.Euler(0, 0, 0);
                            break;
                        }
                    }
                }
                _playerMoves.Add(playerObject, player.Moves);
            });
            defensivePlayers.ForEach(player =>
            {
                var playerObject = Instantiate(playerDefencePrefab, player.GetFieldLocation(length, width), Quaternion.Euler(0, 270, 0));
                playerObject.name = player.Position;
                _playerMoves.Add(playerObject, player.Moves);
            });
            if (_debug) Debug.Log($"[PlayersManager] Offensive {offensivePlayers.Count}, Defensive {defensivePlayers.Count}");
            
            MaxTime = MaxPlayerMovesTime() + InitTime;
            
            if (_debug) Debug.Log($"[PlayersManager] Max time {MaxTime}");
        }
        
        public static LinkedList<PlayerMove> GetPlayerMoves(GameObject player)
        {
            return _playerMoves[player];
        }
        
        private float MaxPlayerMovesTime()
        {
            return _playerMoves.Values.Select(playerMoves => playerMoves.Sum(playerMove => playerMove.Duration)).Prepend(0).Max();
        }
    }
}