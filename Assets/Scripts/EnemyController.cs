using Assets.Scripts.Utils;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    [RequireComponent(typeof(WaypointAgent))]
    [RequireComponent(typeof(EnemyEyes))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float rotateSpeed = 150f;
        [SerializeField]
        private float movementSpeed = 1f;

        private WaypointAgent waypointAgent;
        private EnemyEyes enemyEyes;
        private Tween mainTween;

        private void Awake()
        {
            waypointAgent = GetComponent<WaypointAgent>();
            enemyEyes = GetComponent<EnemyEyes>();

            //�������� �� ������� ���������� ����� � ����������� ������
            waypointAgent.OnPositionReached.AddListener(OnPositionReached);
            enemyEyes.OnPlayerVisible.AddListener(OnPlayerVisible);
        }

        private void OnPositionReached(Waypoint waypoint)
        {
            RotateTo(waypoint.Point.position);
        }

        private void OnPlayerVisible(GameObject detectBy)
        {
            RotateTo(enemyEyes.Target.position, false);

            GameManager.Instance.Lose();

            Debug.Log("���, ����� �����!");
        }

        // ������� � �����
        private void RotateTo(Vector3 target, bool moveAfter = true)
        {
            var direction = (target - transform.position).normalized;
            direction.y = 0f;

            // ������� ����������� (� ��������� x z) � ���� (y)
            var angle = 90f - Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            if (mainTween != null)
            {
                mainTween.Kill(); //���������� ���������� �������� ���� ��� ����
            }

            //������� � ����������� � ����������. ������������ ������������� � ����������� �� �������� ����
            mainTween = transform.DORotate(Vector3.up * angle, Mathf.Abs(angle - transform.eulerAngles.y) / rotateSpeed).SetEase(Ease.OutSine);

            if (moveAfter)
            {
                mainTween.OnComplete(() => MoveTo(target)); // ������ �������� ����� ��������� ��������
            }
        }

        //������ �������� � �����
        private void MoveTo(Vector3 target)
        {
            target.y = transform.position.y;

            if (mainTween != null)
            {
                mainTween.Kill(); //���������� ���������� �������� ���� ��� ����
            }

            //�������� � ����������� � ����������. ������������ ������������� � ����������� �� ����������� ��� ����������� ���������
            mainTween = transform.DOMove(target, target.magnitude / movementSpeed).SetEase(Ease.InOutSine); 
        }
    }
}