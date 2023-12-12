using System.Collections.Generic;
using UI;

namespace Formations
{
    /**
     * List of american football defensive formations
     * https://en.wikipedia.org/wiki/List_of_formations_in_American_football#Defensive_formations
     */
    public static class DefensiveFormations
    {
        public const string DefaultName = "4-3";
        
        private static Dictionary<string, FormationDefensive> _defensiveFormations = new ()
        {
            {
                "4-3", new FormationDefensive()
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 150, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 250, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 350, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 450, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 100, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 300, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 500, y = 20 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 50, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 1050, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 350, y = 10 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 800, y = 10 })
            },
            {
                "3-4", new FormationDefensive()
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 200, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 300, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 400, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 150, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 250, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 350, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 450, y = 20 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 50, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 1050, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 350, y = 10 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 800, y = 10 })
            },
            {
                "4–2–5 nickel", new FormationDefensive()
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 150, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 250, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 350, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 450, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 200, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 400, y = 20 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 50, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 1100, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 1000, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 350, y = 10 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 800, y = 10 })
            },
            {
                "3–3–5 nickel", new FormationDefensive()
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 200, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 300, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 400, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 150, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 300, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 450, y = 20 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 50, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 1100, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 1000, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 350, y = 10 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 800, y = 10 })
            },
            {
                "4–1–6 dime", new FormationDefensive()
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 150, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 250, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 350, y = 20 })
                    .AddDL(new PlayerMarker() { position = "DL", isOffense = false, x = 450, y = 20 })
                    .AddLB(new PlayerMarker() { position = "LB", isOffense = false, x = 300, y = 20 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 0, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 100, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 1100, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 1000, y = 350 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 350, y = 10 })
                    .AddDB(new PlayerMarker() { position = "DB", isOffense = false, x = 800, y = 10 })
            }
        };
        
        public static FormationDefensive GetDefault()
        {
            return _defensiveFormations[DefaultName];
        }
        
        public static FormationDefensive Get(string name)
        {
            return _defensiveFormations.GetValueOrDefault(name, GetDefault());
        }
        
        public static List<string> GetNames()
        {
            return new List<string>(_defensiveFormations.Keys);
        }
    }
}