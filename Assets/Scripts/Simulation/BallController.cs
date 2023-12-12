using UnityEngine;

namespace Simulation
{
    public class BallController: MonoBehaviour
    {
        public Rigidbody rigid;
        
        public void Start()
        {
            Debug.Log("BallController started");
            rigid.useGravity = false;
        }
        
        public void ReleaseBall()
        {
            Debug.Log("Releasing ball");
            rigid.useGravity = true;
            transform.parent = null;
            transform.rotation = Quaternion.Euler(-15, 75, 0);
            rigid.AddForce(transform.forward * 110000);
        }
    }
}