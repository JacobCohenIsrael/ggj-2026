using UnityEngine;
using DG.Tweening;

public class Hover : MonoBehaviour
{
    [Header("Hover Settings")]
    public float hoverHeight = 0.5f;
    public float hoverDuration = 2f;

    private Vector3 startPosition;
    private Tween hoverTween;

    void Start()
    {
        startPosition = transform.position;

        hoverTween = transform
            .DOMoveY(startPosition.y + hoverHeight, hoverDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void OnDisable()
    {
        hoverTween?.Kill();
    }
}