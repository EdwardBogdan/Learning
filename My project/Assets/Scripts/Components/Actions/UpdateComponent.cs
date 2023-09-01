using MyProject.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class UpdateComponent : MonoBehaviour, INaming
    {
        [SerializeField] UnityEvent _action;
        public string NameElement => "Upd.";
        void Update()
        {
            _action.Invoke();
        }
    }
}
