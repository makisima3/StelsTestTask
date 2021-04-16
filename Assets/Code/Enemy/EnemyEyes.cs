using StelsTestTask.Core;
using StelsTestTask.Events;
using UnityEngine;

namespace StelsTestTask.Enemy
{
    public class EnemyEyes : MonoBehaviour
    {
        [SerializeField]
        private float viewRadius;
        [SerializeField, Range(0, 360)]
        private float viewAngle;

        //маска того что видит противник (стены, игрок) и без того чего не видит (прозрачные элементы)
        [SerializeField]
        private LayerMask obstacleMask;
        [SerializeField]
        private Transform target;

        [SerializeField]
        private OnPlayerVisible onPlayerVisible;
        [SerializeField]
        private MeshRenderer meshRenderer;

        public float ViewRadius => viewRadius;
        public float ViewAngle => viewAngle;
        public OnPlayerVisible OnPlayerVisible => onPlayerVisible;
        //цель поиска (игрок)
        public Transform Target {get => target; set => target = value;}

        private void Awake()
        {
            //создание собственного экземпляра материала для каждого объекта
            meshRenderer.material = meshRenderer.material;
        }

        private void Start()
        {
           target = FindObjectOfType<Player>().gameObject.transform;
        }

        private void OnValidate()
        {
            //обновление параметров материала при изменение значений полей в Unity            
            meshRenderer.sharedMaterial.SetFloat("_view_angle", viewAngle);
        }

        void Update()
        {
            FindTarget();
        }

        // переменные метода вынесены в поля для отрисовки Gizmos
        float rightBorder = 0f;
        float leftBorder = 0f;
        float testAngle = 0f;
        Ray ray;
        private void FindTarget()
        {
            var vectorToTarget = target.position - transform.position;
            var direction = vectorToTarget.normalized;
            direction.y = 0f;

            var distance = vectorToTarget.magnitude;

            ray = new Ray(transform.position, direction);

            if (distance < viewRadius)
            {
                //угол до игрока
                testAngle = DirectionToAngle(direction);

                //определение границ взгляда. от текущего поворота + и - половина угла обзора соответственно
                rightBorder = transform.eulerAngles.y + (viewAngle / 2f);
                leftBorder = transform.eulerAngles.y - (viewAngle / 2f);

                //угол до игрока входит в диапазон взгляда
                if (testAngle > leftBorder && testAngle < rightBorder)
                {
                    //проверка видимости игрока за стенами
                    if (CheckPlayer(ray))
                    {
                        onPlayerVisible.Invoke(gameObject); //вызов события
                    }
                }
            }
        }

        private float DirectionToAngle(Vector3 direction)
        {
            // 90 чтобы ось z (forward vector) соответствовала повороту начитнающемуся от оси x
            return 90f - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        }

        private bool CheckPlayer(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, viewRadius, obstacleMask))
            {
                if (hit.transform.TryGetComponent(out Player player))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(ray);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, viewRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + AngleToDirection(testAngle) * viewRadius);

            Gizmos.color = Color.blue / 2f;
            Gizmos.DrawLine(transform.position, transform.position + AngleToDirection(leftBorder) * viewRadius);
            Gizmos.DrawLine(transform.position, transform.position + AngleToDirection(rightBorder) * viewRadius);
        }


        public Vector3 AngleToDirection(float angleInDegrees)
        {
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}