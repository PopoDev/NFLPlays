using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Formations;

namespace UI
{
	public class FormationGenerator : MonoBehaviour
	{
		private static bool _debug = false;
		private static bool ShowZones { get; set; } = true;
		private static Box _formationOffensive, _formationOffensiveFront, _formationOffensiveBack;
		private static Box _formationDefensive, _formationDefensiveDL, _formationDefensiveLB;
		
		private static List<PlayerMarker> _offensePlayers = new List<PlayerMarker>();
		private static List<PlayerMarker> _defensePlayers = new List<PlayerMarker>();

		public static void Enable(VisualElement root)
		{
			Debug.Log("FormationGenerator enabled");
			_formationOffensive = root.Q<Box>("formation__offensive");
		    _formationOffensiveFront = root.Q<Box>("formation__offensive--front");
		    _formationOffensiveBack = root.Q<Box>("formation__offensive--back");
		    
		    _formationDefensive = root.Q<Box>("formation__defensive");
		    _formationDefensiveDL = root.Q<Box>("formation__defensive--dl");
		    _formationDefensiveLB = root.Q<Box>("formation__defensive--lb");
		    
		    _offensePlayers = LoadFormation(OffensiveFormations.DefaultName, true);
		    _defensePlayers = LoadFormation(DefensiveFormations.DefaultName, false);
		}
		
		public static List<PlayerMarker> LoadFormation(string formationName, bool isOffense)
		{
			ClearFormation(isOffense);
			
			var players = isOffense ? 
				OffensiveFormations.Get(formationName).Init(_formationOffensiveFront, _formationOffensiveBack, _formationOffensive) : 
				DefensiveFormations.Get(formationName).Init(_formationDefensiveDL, _formationDefensiveLB, _formationDefensive);

			if (isOffense) _offensePlayers = players; 
			else _defensePlayers = players;
			
			players.ForEach(MainMenuUIManager.RegisterCallbackPlayerMarker);
			return players;
		}
		
		public static List<PlayerMarker> GetPlayers(bool isOffense)
		{
			return isOffense ? _offensePlayers : _defensePlayers;
		}
		
		private static void ClearFormation(bool isOffense)
		{
			if (isOffense)
			{
				_formationOffensiveFront.Clear();
				_formationOffensiveBack.Clear();
				_formationOffensive.Children().OfType<PlayerMarker>().ToList().ForEach(button => button.RemoveFromHierarchy());
			}
			else
			{
				_formationDefensiveDL.Clear();
				_formationDefensiveLB.Clear();
				_formationDefensive.Children().OfType<PlayerMarker>().ToList().ForEach(button => button.RemoveFromHierarchy());
			}
		}

		public static void ToggleShowZones(Button showZonesButton)
		{
			ShowZones = !ShowZones;
			showZonesButton.text = ShowZones ? "Hide Zones" : "Show Zones";
			
			_formationOffensive.style.backgroundColor = ShowZones ? Color.blue :Color.clear;
			_formationOffensiveFront.style.backgroundColor = ShowZones ? Color.black :Color.clear;
			_formationOffensiveBack.style.backgroundColor = ShowZones ? Color.yellow :Color.clear;
			
			_formationDefensive.style.backgroundColor = ShowZones ? Color.red :Color.clear;
			_formationDefensiveDL.style.backgroundColor = ShowZones ? Color.grey :Color.clear;
			_formationDefensiveLB.style.backgroundColor = ShowZones ? Color.magenta :Color.clear;
			
			if (_debug) Debug.Log("Toggle Show Zones: " + ShowZones);
		}
		
		public static void DisableZones(Button showZonesButton)
		{
			if (ShowZones) ToggleShowZones(showZonesButton);
		}
	}
}
