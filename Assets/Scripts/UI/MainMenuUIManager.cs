using System.Collections.Generic;
using Formations;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        private static bool _debug = false;
        public const float WhiteboardWidthInPixels = 1536;
        public const float WhiteboardWidthInYards = 53.3f;
        public const float WhiteboardYardsPerPixel = WhiteboardWidthInYards / WhiteboardWidthInPixels;  // 0.0347
        
        private static PlayerMoveManager _playerMoveManager;
        
        private static Button _showZonesButton;
        
        private static PlayerMarker _selectedPlayer;
        private static Box _sideBarPlayerBox;
        private static Label _sideBarPlayerLabel;
        private static List<Button> _sideBarPlayerMoves;
        
        private Box _whiteboard;
        private VisualElement _field;
        private MeshGenerationContext _mgc;
        
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            Debug.Log("MainMenuUIManager enabled");
            
            _playerMoveManager = PlayerMoveManager.GetInstance(root.Q<Label>("instructions__label"));
            _showZonesButton = root.Q<Button>("showZonesButton");
            _sideBarPlayerBox = root.Q<Box>("sidebar__player");
            _sideBarPlayerLabel = root.Q<Label>("sidebar__player--label");
            _field = root.Q("field");
            _whiteboard = root.Q<Box>("whiteboard");
            
            FormationGenerator.Enable(root);
            FormationGenerator.DisableZones(_showZonesButton);
            
            root.Q<Button>("selectOffenseButton").clicked += () => OnClickFormation(root, true);
            root.Q<Button>("selectDefenseButton").clicked += () => OnClickFormation(root, false);   
            
            _showZonesButton.clicked += () => FormationGenerator.ToggleShowZones(_showZonesButton);
            
            _field.generateVisualContent += OnFieldDraw;
            
            _whiteboard.RegisterCallback<ClickEvent>(OnWhiteboardClick);
            
            /*
            var playerMarkerOffense = root.Query<PlayerMarker>("player-marker-offense").ToList();
            var playerMarkerDefense = root.Query<PlayerMarker>("player-marker-defense").ToList();
            playerMarkerOffense.ForEach(RegisterCallbackPlayerMarker);
            playerMarkerDefense.ForEach(RegisterCallbackPlayerMarker);
            */
            
            var playButton = root.Q<Button>("simulateButton");
            playButton.clicked += () =>
            {
                Vector2 playOrigin = _whiteboard.worldBound.position + _whiteboard.worldBound.size / 2;
                
                // Screen: x = horizontal, y = vertical
                float width = _whiteboard.worldBound.size.x;
                float length = _whiteboard.worldBound.size.y;
                if (_debug)
                {
                    Debug.Log($"Whiteboard width {width}, length {length}");
                    Debug.Log($"Play origin at {playOrigin}");
                    /*
                    playerMarkerOffense.ForEach(player => Debug.Log($"Offense {player.position} at {player.worldBound.position - playOrigin}"));
                    playerMarkerDefense.ForEach(player => Debug.Log($"Defense {player.position} at {player.worldBound.position - playOrigin}"));
                    */
                }
                
                NFLPlays.SetPlayersFromMarkers(
                    FormationGenerator.GetPlayers(true), 
                    FormationGenerator.GetPlayers(false),
                    playOrigin, length, width);
                
                SceneManager.LoadScene("Simulation");
            };
        }

        private void OnClickFormation(VisualElement root, bool isOffense)
        {
            FormationGenerator.DisableZones(_showZonesButton);
            FormationWindow.ShowFormations(root, isOffense ? OffensiveFormations.GetNames() : DefensiveFormations.GetNames(), isOffense);
        }
        
        public static void RegisterCallbackPlayerMarker(PlayerMarker playerMarker)
        {
            var button = playerMarker.m_Button;
            button.clicked += () => OnSelectPlayerMarker(playerMarker);
        }
        
        private static void OnSelectPlayerMarker(PlayerMarker playerMarker)
        {
            FormationGenerator.DisableZones(_showZonesButton);
            SelectPlayerMarker(playerMarker);
            ShowSideBarPlayerMoves(playerMarker);
        }
        
        private static void SelectPlayerMarker(PlayerMarker playerMarker)
        {
            if (_selectedPlayer != null) // Unselect the previous player
            {
                _selectedPlayer.m_Button.style.backgroundColor = _selectedPlayer.isOffense ? Color.blue : Color.red;
                _playerMoveManager.ResetPlayerMoves();
                if (_debug) Debug.Log($"Unselected {_selectedPlayer.position}");
            }
            _selectedPlayer = playerMarker;
            playerMarker.m_Button.style.backgroundColor = Color.green;
            if (_debug) Debug.Log($"Selected {_selectedPlayer.position}");
        }
        
        private static void ShowSideBarPlayerMoves(PlayerMarker playerMarker)
        {
            // Clear existing buttons
            _sideBarPlayerMoves?.ForEach(button => _sideBarPlayerBox.Remove(button));
            
            _sideBarPlayerBox.style.backgroundColor = _selectedPlayer.isOffense ? Color.blue : Color.red;
            _sideBarPlayerLabel.text = _selectedPlayer.position;
            _sideBarPlayerMoves = new List<Button>();
            switch (playerMarker.position)
            {
                case "QB":
                    _sideBarPlayerMoves.Add(_playerMoveManager.QBPassButton());
                    _sideBarPlayerMoves.Add(_playerMoveManager.QBRushButton());
                    break;
                case "WR":
                    _sideBarPlayerMoves.Add(_playerMoveManager.WRRushButton());
                    _sideBarPlayerMoves.Add(_playerMoveManager.WRCurlButton());
                    _sideBarPlayerMoves.Add(_playerMoveManager.WRSlantButton());
                    break;
                case "LG": case "RG": case "LT": case "RT": case "C": case "TE":
                    _sideBarPlayerMoves.Add(_playerMoveManager.BlockButton());
                    break;
                case "DB": case "LB": case "DL":
                    _sideBarPlayerMoves.Add(_playerMoveManager.TackleButton());
                    break;
            }
            
            _sideBarPlayerMoves.ForEach(button => _sideBarPlayerBox.Add(button));
        }
        
        private void OnWhiteboardClick(ClickEvent evt)
        {
            if (_debug) Debug.Log("Whiteboard clicked at " + evt.position);

            if (_selectedPlayer == null) return;
            
            if (_playerMoveManager.ProcessMove(evt.position, _selectedPlayer))
            {
                _field.MarkDirtyRepaint();
            }
        }
        
        private void OnFieldDraw(MeshGenerationContext mgc)
        {
            if (_selectedPlayer == null) return;
            
            var paint2D = mgc.painter2D;
            paint2D.strokeColor = Color.cyan;
            paint2D.lineWidth = 5f;
            
            var rect = mgc.visualElement.worldBound;
            var origin = rect.min;
            var start = _selectedPlayer.GetPositionCenter() - origin;

            _playerMoveManager.DrawMove(paint2D, origin, start);
        }
    }
}
