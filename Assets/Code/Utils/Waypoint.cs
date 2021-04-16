using UnityEngine;

namespace StelsTestTask.Utils
{
    // Компонент точки пути. В конечном итоге образует однонаправленный связный список с возможностью закольцовыванию
    public class Waypoint : MonoBehaviour
    {
        //ссылка на следующую точку пути
        [SerializeField]
        private Waypoint next;

        //Свойство для доступа к положению точки (добавлено для большей читаемости кода)
        public Transform Point => transform;

        //Свойство для доступа к полю извне
        public Waypoint Next => next;
    }
}
