using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
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
}
