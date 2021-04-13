using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField]
        private Waypoint next;

        public Transform Point => transform;

        public Waypoint Next => next;
    }
}
