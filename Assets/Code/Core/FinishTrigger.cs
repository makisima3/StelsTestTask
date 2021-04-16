using UnityEngine;

namespace StelsTestTask.Core
{
    public class FinishTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                GameManager.Instance.Win();
            }
        }
    }
}
