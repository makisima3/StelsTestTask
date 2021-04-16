using UnityEngine;

namespace StelsTestTask.Utils
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField]
        private float angle = 1;

        private void Update()
        {
            transform.Rotate(0, 0, -angle * Time.deltaTime);
        }
    }
}