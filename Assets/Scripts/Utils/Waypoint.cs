using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
