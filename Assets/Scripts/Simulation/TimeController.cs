using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace Simulation
{
    public class TimeController: MonoBehaviour
    {
        public static bool Playing = true;
        private static List<Animator> _animators;
        
        private float _maxTime;
        
        private float actTime;
        private PlayableDirector _playableDirector;

        public void OnEnable()
        {
            Debug.Log("TimeController enabled");
            var players = GameObject.FindGameObjectsWithTag("Player").ToList();
            players.ForEach(player => Debug.Log(player.name));
            _animators = players.Select(player => player.GetComponent<Animator>()).ToList();
            
        }

        private static void Pause()
        {
            if (!Playing) return;
            
            Playing = false;
            _animators.ForEach(animator => animator.speed = 0);
        }
        
        private static void Play()
        {
            if (Playing) return;
            
            Playing = true;
            _animators.ForEach(animator => animator.speed = 1);
        }
        
        public static void TogglePlay()
        {
            if (Playing)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        private void setTime() {
            PlayableDirector playableDirector = GetComponent<PlayableDirector>();
            actTime = 1;
            if (playableDirector.state == PlayState.Paused)
            {
                // this will call RebuildGraph if needed
                playableDirector.Play();
               
                // will set the speed of the graph to 0, so it's always playing but never
                // advancing
                playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
            }
           
            playableDirector.time = actTime;
     
        }

    }
}