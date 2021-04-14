using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SuccesView : MonoBehaviour
{
    [SerializeField]
    private float startScale = 0;
    [SerializeField]
    private float endScale = 0;

    [SerializeField]
    private Vector2 startPosition;
    [SerializeField]
    private Vector2 endPosition;

    [SerializeField]
    private float showDuration;

    private new RectTransform transform;


    private void Awake()
    {
        transform = GetComponent<RectTransform>();
    }

    public void Show()
    {
        transform.localScale = startScale * Vector3.one;
        transform.localPosition = startPosition;

        transform.DOScale(endScale, showDuration).SetEase(Ease.OutBack);
        transform.DOAnchorPos(endPosition, showDuration).SetEase(Ease.InExpo);
    }
}
