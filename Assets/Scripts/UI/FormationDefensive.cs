using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UI
{
    public class FormationDefensive
    {
        public List<PlayerMarker> DL { get; set; } = new ();
        public List<PlayerMarker> LB { get; set; } = new();
        
        public List<PlayerMarker> DB { get; set; } = new();
        
        public FormationDefensive() { }
        
        public FormationDefensive AddDL(PlayerMarker player)
        {
            DL.Add(player);
            return this;
        }
        
        public FormationDefensive AddLB(PlayerMarker player)
        {
            LB.Add(player);
            return this;
        }
        
        public FormationDefensive AddDB(PlayerMarker player)
        {
            DB.Add(player);
            return this;
        }
        
        public List<PlayerMarker> Init(Box dlZone, Box lbZone, Box dbZone)
        {
            DL.ForEach(dlZone.Add);
            LB.ForEach(lbZone.Add);
            DB.ForEach(dbZone.Add);
            
            // Enable Drag
            DL.ForEach(player => player.EnableDrag(dlZone));
            LB.ForEach(player => player.EnableDrag(lbZone));
            DB.ForEach(player => player.EnableDrag(dbZone));
            
            var players = new List<PlayerMarker>();
            players.AddRange(DL);
            players.AddRange(LB);
            players.AddRange(DB);
            return players;
        }
    }
}