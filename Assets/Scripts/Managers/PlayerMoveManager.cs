
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class PlayerMoveManager
    {
        private static PlayerMoveManager _instance;
        private bool _debug = true;
        
        public const float SecondsYardDash40 = 4.5f;
        public const float YardsPerSecond = 40f / SecondsYardDash40;
        
        private readonly Label _instructionsLabel;
        private const string InstructionsDefault = "Click Simulate to view the play in 3D";
        
        private PlayerMoves _playerMoves = PlayerMoves.NONE;
        private Vector2 _WRRushEnd = Vector2.zero;
        private Vector2 _WRCurlEnd = Vector2.zero;
        private Vector2 _WRSlantEnd = Vector2.zero;
        
        private LinkedList<Action> _drawings = new();
        
        private PlayerMoveManager(Label instructionsLabel)
        {
            Debug.Log("PlayerMoveManager enabled");
            _instructionsLabel = instructionsLabel;
        }
        
        public static PlayerMoveManager GetInstance(Label instructionsLabel)
        {
            return _instance ??= new PlayerMoveManager(instructionsLabel);
        }
        
        /**
         * Process the player move and add it to the player's moves list
         * @param whiteboardPosition The position on the whiteboard where the player move ended
         * @param playerMarker The player marker
         * @return Whether the canvas should be redrawn
         */
        public bool ProcessMove(Vector2 whiteboardPosition, PlayerMarker playerMarker)
        {
            var speedFactor = 1f;
            var animationTime = 0f;
            switch (_playerMoves)
            {
                case PlayerMoves.WR_RUSH:
                    _WRRushEnd = whiteboardPosition;
                    speedFactor = 0.8f;
                    break;
                case PlayerMoves.WR_CURL:
                    _WRCurlEnd = whiteboardPosition;
                    speedFactor = 0.4f;
                    break;
                case PlayerMoves.WR_SLANT:
                    _WRSlantEnd = whiteboardPosition;
                    speedFactor = 0.8f;
                    break;
                case PlayerMoves.QB_PASS:
                    animationTime = 7f;
                    speedFactor = 0.4f;
                    break;
                case PlayerMoves.QB_RUSH:
                    break;
                case PlayerMoves.NONE:
                    return false;  // No action
            }
            
            var distancePixels = Vector2.Distance(playerMarker.GetPositionCenter(), whiteboardPosition);
            var distanceYards = distancePixels * MainMenuUIManager.WhiteboardYardsPerPixel;
            var duration = distanceYards / (YardsPerSecond * speedFactor);
            
            if (_debug) Debug.Log($"PlayerMoveManager: ProcessMove {_playerMoves}, distancePixels {distancePixels}, distanceYards {distanceYards}, duration {duration}");
            playerMarker.Moves.AddLast(new PlayerMove(_playerMoves, duration + animationTime));
            
            _instructionsLabel.text = InstructionsDefault;
            return true;
        }
        
        public void DrawMove(Painter2D paint2D, Vector2 origin, Vector2 start)
        {
            var wrRushEnd = _WRRushEnd - origin; 
            var wrCurlEnd = _WRCurlEnd - origin;
            var wrSlantEnd = _WRSlantEnd - origin;
            
            switch (_playerMoves)
            {
                case PlayerMoves.WR_RUSH:
                    _drawings.AddLast(() => paint2D.MoveTo(start));
                    _drawings.AddLast(() => paint2D.LineTo(wrRushEnd));
                    if (_debug) Debug.Log($"WR_RUSH: Stroke from {start} to {wrRushEnd}");
                    break;
                case PlayerMoves.WR_SLANT:
                    _drawings.AddLast(() => paint2D.LineTo(wrSlantEnd));
                    if (_debug) Debug.Log($"WR_SLANT: Stroke to {wrSlantEnd}");
                    break;
                case PlayerMoves.WR_CURL:
                    // Calculate the control point for the arc
                    Vector2 diff = (wrCurlEnd - wrRushEnd) / 2;
                    float radius = diff.magnitude;
                    Vector2 rushDir = (wrRushEnd - start) * 0.25f;
                    Vector2 controlPoint = wrRushEnd + diff + rushDir;
                    
                    _drawings.AddLast(() => paint2D.ArcTo(controlPoint, wrCurlEnd, radius));
                    if (_debug) Debug.Log($"WR_CURL: ArcTo from {wrRushEnd} to {wrCurlEnd} with control point {controlPoint}");
                    break;
            }
            
            /*
            switch (_playerMoves)
            {
                case PlayerMoves.WR_RUSH: case PlayerMoves.WR_CURL:
                    var wrRushEnd = _WRRushEnd - origin;
                    paint2D.LineTo(wrRushEnd);
                    if (_debug) Debug.Log($"WR_RUSH: Stroke from {start} to {wrRushEnd}");

                    if (_playerMoves == PlayerMoves.WR_CURL)
                    {
                        var wrCurlEnd = _WRCurlEnd - origin;
                        
                        // Calculate the control point for the arc
                        Vector2 diff = (wrCurlEnd - wrRushEnd) / 2;
                        float radius = diff.magnitude;
                        Vector2 rushDir = (wrRushEnd - start) * 0.25f;
                        Vector2 controlPoint = wrRushEnd + diff + rushDir;
                        if (_debug) Debug.Log($"WR_CURL: diff {diff}, radius {radius}, rushDir {rushDir}, controlPoint {controlPoint}");
                        
                        paint2D.ArcTo(
                            controlPoint,
                            wrCurlEnd,
                            radius
                        );
                        if (_debug) Debug.Log($"WR_CURL: ArcTo from {wrRushEnd} to {wrCurlEnd} with control point {controlPoint}");
                    }
                    break;
            }
            */
            paint2D.BeginPath();
            foreach (var draw in _drawings)
            {
                draw();
            }
            paint2D.Stroke();
        }
        
        public void ResetPlayerMoves()
        {
            _playerMoves = PlayerMoves.NONE;
        }

        public void Reset()
        {
            ResetPlayerMoves();
            _drawings.Clear();
        }
        
        private Button MoveButton(string text)
        {
            return new Button() { text = text, style = { width = 250, height = 50, fontSize = 24, marginBottom = 10, color = Color.white, opacity = 0.75f,}};
        }
        
        public Button WRRushButton()
        {
            var button = MoveButton("Rush");
            button.clicked += () =>
            {
                _instructionsLabel.text = "Select the end point of the rush";
                _playerMoves = PlayerMoves.WR_RUSH;
            };
            return button;
        }

        public Button WRCurlButton()
        {
            var button = MoveButton("Curl");
            button.clicked += () =>
            {
                _instructionsLabel.text = "Select the end point of the curl";
                _playerMoves = PlayerMoves.WR_CURL;
            };
            return button;
        }
        
        public Button WRSlantButton()
        {
            var button = MoveButton("Slant");
            button.clicked += () =>
            {
                _instructionsLabel.text = "Select the end point of the slant";
                _playerMoves = PlayerMoves.WR_SLANT;
            };
            return button;
        }
        
        public Button QBPassButton()
        {
            var button = MoveButton("Pass");
            button.clicked += () =>
            {
                _instructionsLabel.text = "Select the end point of the pass";
                _playerMoves = PlayerMoves.QB_PASS;
            };
            return button;
        }
        
        public Button QBRushButton()
        {
            var button = MoveButton("Rush");
            button.clicked += () =>
            {
                _instructionsLabel.text = "Select the end point of the rush";
                _playerMoves = PlayerMoves.QB_RUSH;
            };
            return button;
        }
        
        public Button BlockButton()
        {
            var button = MoveButton("Block");
            return button;
        }
        
        public Button TackleButton()
        {
            var button = MoveButton("Tackle");
            return button;
        }
    }
}