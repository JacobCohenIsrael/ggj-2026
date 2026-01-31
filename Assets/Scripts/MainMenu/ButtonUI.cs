using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Overcrowded
{
    [RequireComponent(typeof(Button))]
    public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 mouseOverTargetScale;
        [SerializeField] private float mouseOverScaleDuration;
        
        private Vector3 originScale;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            originScale = button.transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            button.transform.DOScale(mouseOverTargetScale, mouseOverScaleDuration).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            button.transform.DOScale(originScale, mouseOverScaleDuration).SetUpdate(true);
        }
    }
}