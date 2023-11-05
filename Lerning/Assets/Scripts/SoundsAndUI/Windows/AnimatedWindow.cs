using UnityEngine;
using System;
using Core;

namespace SoundsAndUI.Windows
{
    public class AnimatedWindow : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        protected static Transform CanvasTransform => SystemCore.OverlayCanvas.transform;

        protected Action _closeAction;

        protected virtual void Start()
        {
            _animator.SetTrigger(Show);
        }
        public void TriggerClose()
        {
            _animator.SetTrigger(Hide);
        }

        public void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();

            Destroy(gameObject);
        }
    }
}

