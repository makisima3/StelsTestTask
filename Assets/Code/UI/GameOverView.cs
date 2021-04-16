using UnityEngine;
using DG.Tweening;
using StelsTestTask.Core;

namespace StelsTestTask.UI
{
    public class GameOverView : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField]
        private float startScale = 0;
        [SerializeField]
        private float endScale = 0;
        [SerializeField]
        private float showDuration;

        public void Show()
        {
            transform.localScale = startScale * Vector3.one;

            transform.DOScale(endScale, showDuration).SetEase(Ease.OutBack);
        }

        public void Replay()
        {
            LevelBootstrapper.Instance.Replay();
        }
    }
}