using UnityEngine;

namespace Simulation
{
    public class HelmetController: MonoBehaviour
    {
        public GameObject parentBone;
        private void Start()
        {
            Debug.Log("HelmetController enabled");
            transform.parent = parentBone.transform;
            Debug.Log("HelmetController parent: " + transform.parent.name);
        }
    }
}