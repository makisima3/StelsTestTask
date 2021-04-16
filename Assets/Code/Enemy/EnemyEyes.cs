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

        //����� ���� ��� ����� ��������� (�����, �����) � ��� ���� ���� �� ����� (���������� ��������)
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
        //���� ������ (�����)
        public Transform Target {get => target; set => target = value;}

        private void Awake()
        {
            //�������� ������������ ���������� ��������� ��� ������� �������
            meshRenderer.material = meshRenderer.material;
        }

        private void Start()
        {
           target = FindObjectOfType<Player>().gameObject.transform;
        }

        private void OnValidate()
        {
            //���������� ���������� ��������� ��� ��������� �������� ����� � Unity            
            meshRenderer.sharedMaterial.SetFloat("_view_angle", viewAngle);
        }

        void Update()
        {
            FindTarget();
        }

        // ���������� ������ �������� � ���� ��� ��������� Gizmos
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
                //���� �� ������
                testAngle = DirectionToAngle(direction);

                //����������� ������ �������. �� �������� �������� + � - �������� ���� ������ ��������������
                rightBorder = transform.eulerAngles.y + (viewAngle / 2f);
                leftBorder = transform.eulerAngles.y - (viewAngle / 2f);

                //���� �� ������ ������ � �������� �������
                if (testAngle > leftBorder && testAngle < rightBorder)
                {
                    //�������� ��������� ������ �� �������
                    if (CheckPlayer(ray))
                    {
                        onPlayerVisible.Invoke(gameObject); //����� �������
                    }
                }
            }
        }

        private float DirectionToAngle(Vector3 direction)
        {
            // 90 ����� ��� z (forward vector) ��������������� �������� �������������� �� ��� x
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