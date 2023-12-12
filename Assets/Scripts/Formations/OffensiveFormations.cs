using System.Collections.Generic;
using UI;

namespace Formations
{
    /**
     * List of american football offensive formations
     * https://en.wikipedia.org/wiki/List_of_formations_in_American_football#Offensive_formations
     */
    public static class OffensiveFormations
    {
        public const string DefaultName = "I-Formation";
        
        private static Dictionary<string, FormationOffensive> _offensiveFormations = new()
        {
            {
                "I-Formation", new FormationOffensive()
                    .AddFront(new PlayerMarker() { position = "LT", isOffense = true, x = 100, y = 20 })
                    .AddFront(new PlayerMarker() { position = "LG", isOffense = true, x = 200, y = 20 })
                    .AddFront(new PlayerMarker() { position = "C", isOffense = true, x = 300, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RG", isOffense = true, x = 400, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RT", isOffense = true, x = 500, y = 20 })
                    .AddFront(new PlayerMarker() { position = "TE", isOffense = true, x = 600, y = 20 })
                    .AddBack(new PlayerMarker() { position = "QB", isOffense = true, x = 120, y = 0 })
                    .AddBack(new PlayerMarker() { position = "FB", isOffense = true, x = 120, y = 60 })
                    .AddBack(new PlayerMarker() { position = "HB", isOffense = true, x = 120, y = 120 })
                    .AddRest(new PlayerMarker() { position = "WR", isOffense = true, x = 100, y = 10 })
                    .AddRest(new PlayerMarker() { position = "WR", isOffense = true, x = 1050, y = 50 })
            },
            {
                "T-Formation", new FormationOffensive()
                    .AddFront(new PlayerMarker() { position = "TE", isOffense = true, x = 0, y = 20 })
                    .AddFront(new PlayerMarker() { position = "LT", isOffense = true, x = 100, y = 20 })
                    .AddFront(new PlayerMarker() { position = "LG", isOffense = true, x = 200, y = 20 })
                    .AddFront(new PlayerMarker() { position = "C", isOffense = true, x = 300, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RG", isOffense = true, x = 400, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RT", isOffense = true, x = 500, y = 20 })
                    .AddFront(new PlayerMarker() { position = "TE", isOffense = true, x = 600, y = 20 })
                    .AddBack(new PlayerMarker() { position = "QB", isOffense = true, x = 120, y = 0 })
                    .AddBack(new PlayerMarker() { position = "FB", isOffense = true, x = 120, y = 60 })
                    .AddBack(new PlayerMarker() { position = "HB", isOffense = true, x = 60, y = 60 })
                    .AddBack(new PlayerMarker() { position = "HB", isOffense = true, x = 180, y = 60 })
            },
            {
                "Maryland I", new FormationOffensive()
                    .AddFront(new PlayerMarker() { position = "TE", isOffense = true, x = 0, y = 20 })
                    .AddFront(new PlayerMarker() { position = "LT", isOffense = true, x = 100, y = 20 })
                    .AddFront(new PlayerMarker() { position = "LG", isOffense = true, x = 200, y = 20 })
                    .AddFront(new PlayerMarker() { position = "C", isOffense = true, x = 300, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RG", isOffense = true, x = 400, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RT", isOffense = true, x = 500, y = 20 })
                    .AddFront(new PlayerMarker() { position = "TE", isOffense = true, x = 600, y = 20 })
                    .AddBack(new PlayerMarker() { position = "QB", isOffense = true, x = 120, y = 0 })
                    .AddBack(new PlayerMarker() { position = "FB", isOffense = true, x = 120, y = 60 })
                    .AddBack(new PlayerMarker() { position = "HB", isOffense = true, x = 120, y = 120 })
                    .AddBack(new PlayerMarker() { position = "HB", isOffense = true, x = 120, y = 180 })
            },
            {
                "Power I", new FormationOffensive()
                    .AddFront(new PlayerMarker() { position = "TE", isOffense = true, x = 0, y = 20 })
                    .AddFront(new PlayerMarker() { position = "LT", isOffense = true, x = 100, y = 20 })
                    .AddFront(new PlayerMarker() { position = "LG", isOffense = true, x = 200, y = 20 })
                    .AddFront(new PlayerMarker() { position = "C", isOffense = true, x = 300, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RG", isOffense = true, x = 400, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RT", isOffense = true, x = 500, y = 20 })
                    .AddFront(new PlayerMarker() { position = "TE", isOffense = true, x = 600, y = 20 })
                    .AddBack(new PlayerMarker() { position = "QB", isOffense = true, x = 120, y = 0 })
                    .AddBack(new PlayerMarker() { position = "FB", isOffense = true, x = 120, y = 60 })
                    .AddBack(new PlayerMarker() { position = "FB", isOffense = true, x = 180, y = 60 })
                    .AddBack(new PlayerMarker() { position = "HB", isOffense = true, x = 120, y = 120 })
            },
            {
                "Shotgun", new FormationOffensive()
                    .AddFront(new PlayerMarker() { position = "LT", isOffense = true, x = 100, y = 20 })
                    .AddFront(new PlayerMarker() { position = "LG", isOffense = true, x = 200, y = 20 })
                    .AddFront(new PlayerMarker() { position = "C", isOffense = true, x = 300, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RG", isOffense = true, x = 400, y = 20 })
                    .AddFront(new PlayerMarker() { position = "RT", isOffense = true, x = 500, y = 20 })
                    .AddFront(new PlayerMarker() { position = "TE", isOffense = true, x = 600, y = 20 })
                    .AddBack(new PlayerMarker() { position = "QB", isOffense = true, x = 120, y = 0 })
                    .AddBack(new PlayerMarker() { position = "HB", isOffense = true, x = 120, y = 75 })
                    .AddRest(new PlayerMarker() { position = "WR", isOffense = true, x = 50, y = 10 })
                    .AddRest(new PlayerMarker() { position = "WR", isOffense = true, x = 1050, y = 50 })
                    .AddRest(new PlayerMarker() { position = "WR", isOffense = true, x = 150, y = 50 })
            }
        };
        
        public static FormationOffensive GetDefault()
        {
            return _offensiveFormations[DefaultName];
        }
        
        public static FormationOffensive Get(string name)
        {
            return _offensiveFormations.GetValueOrDefault(name, GetDefault());
        }
        
        public static List<string> GetNames()
        {
            return new List<string>(_offensiveFormations.Keys);
        }
    }
}