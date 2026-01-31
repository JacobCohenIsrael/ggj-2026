using DG.Tweening;
using UnityEngine;

namespace Overcrowded.Game.UI.MainMenu
{
    public class CreditsView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _scroller;
        [SerializeField] private RectTransform _studioIcon;

        [SerializeField] private float _scrollDuration = 20f;
        [SerializeField] private float _iconScrollDuration = 3f;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private float _studioIconMoveDuration = 2f;
        [SerializeField] private float _studioIconExtraMoveDistance = 100f;

        public Tween CreateCreditsTween()
        {
            _canvasGroup.blocksRaycasts = true;

            _scroller.DOKill();
            _studioIcon.DOKill();

            _scroller.anchoredPosition = Vector2.zero;
            _studioIcon.anchoredPosition = Vector2.zero;

            var sequence = DOTween.Sequence();
            sequence.Join(_canvasGroup.DOFade(1f, _fadeDuration));
            sequence.Join(_scroller.DOAnchorPosY(-(_scroller.sizeDelta.y + 1080), _scrollDuration).From(Vector2.zero).SetEase(Ease.Linear));
            sequence.Join(_studioIcon.DOAnchorPosY(-(_studioIcon.sizeDelta.y + _studioIconExtraMoveDistance), _iconScrollDuration).From(Vector2.zero).SetEase(Ease.OutCubic));
            return sequence;
        }

        public Tween CreateCloseTween()
        {
            _canvasGroup.blocksRaycasts = false;

            return _canvasGroup.DOFade(0f, _fadeDuration);
        }
    }
}