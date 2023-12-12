using System.Collections.Generic;
using System.Text;
using Managers;
using UnityEngine;

namespace Simulation
{
    public class AnimationStateController: MonoBehaviour
    {
        private Animator _animator;
        private static readonly int SprintingStance = Animator.StringToHash("SprintingStance");
        private static readonly int FootballStance = Animator.StringToHash("FootballStance");
        private static readonly int GoalkeeperStance = Animator.StringToHash("GoalkeeperStance");
        
        private static readonly int SprintingAction = Animator.StringToHash("SprintingAction");
        private static readonly int QBPassAction = Animator.StringToHash("QBPassAction");
        private static readonly int CurlAction = Animator.StringToHash("CurlAction");
        private static readonly int SlantAction = Animator.StringToHash("SlantAction");
        
        private static readonly int SprintingTime = Animator.StringToHash("SprintingTime");
        
        private LinkedList<PlayerMove> _moves;


        public void Start()
        {
            if (gameObject.name == "ModelOffense") return;
            
            Debug.Log("AnimationStateController enabled");
            _animator = GetComponent<Animator>();
            _moves = PlayersManager.GetPlayerMoves(gameObject);
            if (_moves.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var move in _moves)
                {
                    sb.Append($"{move.Move} {move.Duration}s, ");
                }
                
                Debug.Log($"{gameObject.name} moves: {sb}");
            }
            
            var position = _animator.gameObject.name;
            switch (position)
            {
                case "WR":
                    _animator.SetBool(SprintingStance, true);
                    foreach (var move in _moves)
                    {
                        switch (move.Move)
                        {
                            case PlayerMoves.WR_RUSH:
                                _animator.SetBool(SprintingAction, true);
                                _animator.SetFloat(SprintingTime, move.Duration);
                                Debug.Log($"SprintingTime: {move.Duration}");
                                break;
                            case PlayerMoves.WR_CURL:
                                _animator.SetBool(CurlAction, true);
                                Debug.Log($"CurlAction: {move.Duration}");
                                break;
                            case PlayerMoves.WR_SLANT:
                                _animator.SetBool(SlantAction, true);
                                Debug.Log($"SlantAction: {move.Duration}");
                                break;
                        }
                    }
                    break;
                case "LT": case "LG": case "C": case "RG": case "RT": case "TE": case "DL":
                    _animator.SetBool(FootballStance, true);
                    break;
                case "QB":
                    _animator.SetBool(GoalkeeperStance, true);
                    _animator.SetBool(QBPassAction, true);
                    break;
            }
        }

        /*
        public void Update()
        {
            if (gameObject.name == "ModelOffense") return;

            var position = _animator.gameObject.name;
            switch (position)
            {
                case "WR":
                    _animator.SetBool(SprintingStance, true);
                    _animator.SetBool(SprintingAction, true);
                    break;
                case "LT": case "LG": case "C": case "RG": case "RT": case "TE": case "DL":
                    _animator.SetBool(FootballStance, true);
                    break;
                case "QB":
                    _animator.SetBool(GoalkeeperStance, true);
                    _animator.SetBool(QBPassAction, true);
                    break;
            }
        }
        */
    }
}