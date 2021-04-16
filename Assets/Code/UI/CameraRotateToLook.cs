using Cinemachine;
using StelsTestTask.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StelsTestTask.UI
{
    //Реализуем интерфейс IPointerClickHandler для обработки тапа на элемент
    public class CameraRotateToLook : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Player player;
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        private float radius = 4f;

        private CinemachineTransposer transposer;

        private void Awake()
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        
        public void OnPointerClick(PointerEventData eventData)
        {
            //Получаем текущий поворт игрока
            var angle = player.transform.eulerAngles.y;

            //Перводим угол в направление с установкой радиуса
            var dir = -AngleToDirection(angle) * radius;
            //Игнорируем направление по оси Y
            dir.y = transposer.m_FollowOffset.y;

            //Обновление смещения камеры
            transposer.m_FollowOffset = dir;
        }

        public Vector3 AngleToDirection(float angleInDegrees)
        {
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}