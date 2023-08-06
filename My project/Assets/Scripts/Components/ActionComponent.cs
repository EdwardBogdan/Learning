using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class ActionComponent : MonoBehaviour
    {
        [SerializeField] UnityEvent _action;

        public void Action()
        {
            _action?.Invoke();
        }
    }
}
