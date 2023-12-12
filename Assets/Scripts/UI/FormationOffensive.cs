using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UI
{
    public class FormationOffensive
    {
        public List<PlayerMarker> Front { get; set; } = new ();
        public List<PlayerMarker> Back { get; set; } = new();
        public List<PlayerMarker> Rest { get; set; } = new();
        
        public FormationOffensive() { }
        
        public FormationOffensive AddFront(PlayerMarker player)
        {
            Front.Add(player);
            return this;
        }
        
        public FormationOffensive AddBack(PlayerMarker player)
        {
            Back.Add(player);
            return this;
        }
        
        public FormationOffensive AddRest(PlayerMarker player)
        {
            Rest.Add(player);
            return this;
        }
        
        public List<PlayerMarker> Init(Box frontZone, Box backZone, Box restZone)
        {
            Front.ForEach(frontZone.Add);
            Back.ForEach(backZone.Add);
            Rest.ForEach(restZone.Add);
            
            // Enable Drag
            Front.ForEach(player => player.EnableDrag(frontZone));
            Back.ForEach(player => player.EnableDrag(backZone));
            Rest.ForEach(player => player.EnableDrag(restZone));
            
            var players = new List<PlayerMarker>();
            players.AddRange(Front);
            players.AddRange(Back);
            players.AddRange(Rest);
            return players;
        }
    }
}