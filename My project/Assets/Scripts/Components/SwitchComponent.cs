using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Components
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] bool _state;

        [SerializeField] string _animationKey;

        [ContextMenu("Switch")]
        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animationKey, _state);
        }
    }
}

