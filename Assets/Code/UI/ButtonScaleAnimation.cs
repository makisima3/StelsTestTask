using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace StelsTestTask.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonScaleAnimation : MonoBehaviour
    {
        [SerializeField]
        private Vector3 force;
        [SerializeField]
        private float duration;
        [SerializeField]
        private Ease ease;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void Start()
        {
            RestartAnimations();
        }

        [ContextMenu(nameof(RestartAnimations))]
        public void RestartAnimations()
        {
            transform.DOKill(true);
            transform.DOPunchScale(force, duration, 1, 0).SetLoops(-1).SetEase(ease);
        }

        private void OnClick()
        {
            transform.DOKill(true);
            transform.localScale = Vector3.one;
            transform.DOScale(force, 0.1f).SetEase(Ease.InBack);
        }
    }
}