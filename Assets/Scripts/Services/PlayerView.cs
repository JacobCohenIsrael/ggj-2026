using System;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Playables;

namespace Overcrowded.Services
{
    public class PlayerView : MonoBehaviour
    {
        [Serializable]
        public class TransitionMask
        {
            [SerializeField] private Mask _mask;
            public Mask Mask => _mask;

            [SerializeField] private GameObject _maskView;
            public GameObject MaskView => _maskView;
        }

        [SerializeField] private TransitionMask[] _transitionMasks;

        [SerializeField] private Transform mesh;
        [SerializeField] private CapsuleCollider capsule;
        [SerializeField] private PlayableDirector _maskTransitionDirector;

        [Inject] private MaskChanger _maskChanger;

        private void Awake()
        {
#if UNITY_EDITOR
            mesh.gameObject.SetActive(true);
#endif

            _maskChanger.OnMaskChanged += HandleMaskChanged;
        }

        private void HandleMaskChanged(Mask mask)
        {
            var index = Array.FindIndex(_transitionMasks, t => t.Mask == mask);

            for (var i = 0; i < _transitionMasks.Length; i++)
            {
                var transitionMask = _transitionMasks[i];
                transitionMask.MaskView.SetActive(i == index);
            }

            if(index == -1)
                return;
            
            _maskTransitionDirector.Play();
        }
    }
}