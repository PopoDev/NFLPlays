using UnityEngine;

namespace Simulation
{
    public class FieldLineGenerator: MonoBehaviour
    {
        public GameObject fieldLinePrefab;
        public GameObject fieldLineDottedPrefab;
        public void Start()
        {
            for (int x = -50; x <= 50; x += 1)
            {
                var yardLine = (x % 5 == 0) ? fieldLinePrefab : fieldLineDottedPrefab;
                var fieldLine = Instantiate(yardLine, new Vector3(x*10, 0.1f, 0), Quaternion.identity);
                fieldLine.name = $"FieldLine{x+50}";
            }
        }
    }
}