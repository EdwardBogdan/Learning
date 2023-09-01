using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components
{
    public class TouchComponent : MonoBehaviour, INaming
    {
        [SerializeField] private bool _isTouched;
        public string NameElement => "Touching";
        public bool IsTouched => _isTouched;
        public void Touch(GameObject hit)
        {
            _isTouched = hit != null;
        }
    }
}
