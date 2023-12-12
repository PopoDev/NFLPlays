using UnityEngine;

namespace Simulation
{
    public class ThrowController: MonoBehaviour
    {
        public GameObject ball;
        public void Start()
        {
            Debug.Log("ThrowController started");
        }
        
        public void ThrowBall()
        {
            Debug.Log("Throwing ball");
            ball.GetComponent<BallController>().ReleaseBall();
        }
    }
}