using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.CustomUI.Windows
{
    public class AnimatedWindow : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        void Start()
        {
            _animator.SetTrigger(Show);
        }
        public void Close()
        {
            _animator.SetTrigger(Hide);
        }

        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}
