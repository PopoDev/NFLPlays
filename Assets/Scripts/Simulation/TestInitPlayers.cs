using UnityEngine;

namespace Simulation
{
    public class TestInitPlayers: MonoBehaviour
    {
        public GameObject PlayerOffencePrefab;
        public GameObject PlayerDefencePrefab;

        private void Start()
        {
            for (int i = 0; i < 11; i++)
            {
                var position = new Vector3(0, 0, (i-5) * 20);
                var offset = new Vector3(20, 0, 0);
                Instantiate(PlayerOffencePrefab, position - offset, Quaternion.Euler(0, 90, 0));
                Instantiate(PlayerDefencePrefab, position + offset, Quaternion.Euler(0, 270, 0));
            }
        }
    }
}