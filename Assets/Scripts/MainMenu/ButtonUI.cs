using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Overcrowded.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 mouseOverTargetScale;
        [SerializeField] private float mouseOverScaleDuration;
        [SerializeField] private AudioSource fadeAudioSource;
        [SerializeField] private AudioSource clickAudioSource;
        
        private Vector3 originScale;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            originScale = button.transform.localScale;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            if (clickAudioSource != null)
                clickAudioSource.Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            button.transform.DOScale(mouseOverTargetScale, mouseOverScaleDuration).SetUpdate(true);
            if (fadeAudioSource != null)
                fadeAudioSource.Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            button.transform.DOScale(originScale, mouseOverScaleDuration).SetUpdate(true);
        }
    }
}