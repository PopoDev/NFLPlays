using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simulation
{
    public class AnimationTimeController: MonoBehaviour
    {
        
        public bool Playing = true;
        private List<Animator> _animators;
        
        public void Start()
        {
            Debug.Log("AnimationTimeController enabled");
            var players = GameObject.FindGameObjectsWithTag("Player").ToList();
            _animators = players.Select(player => player.GetComponent<Animator>()).ToList();
        }
        
        public void Update()
        {
            if (Playing)
            {
                var maxTime = PlayersManager.MaxTime;
                var time = Time.timeSinceLevelLoad;
                if (time > maxTime)
                {
                    Pause();
                }
            }
        }
        
        private void Pause()
        {
            if (!Playing) return;
            
            Playing = false;
            _animators.ForEach(animator => animator.speed = 0);
            Time.timeScale = 0;
        }
        
        private void Play()
        {
            if (Playing) return;
            
            Playing = true;
            _animators.ForEach(animator => animator.speed = 1);
            Time.timeScale = 1;
        }
        
        public void TogglePlay()
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
    }
}