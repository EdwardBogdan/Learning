using UnityEngine;

namespace CustomUnityUtils
{
    public class SORefer : MonoBehaviour
    {
        [SerializeField] private ScriptableObject _target;

#if UNITY_EDITOR
        public ScriptableObject Target => _target;

        public void ChangeTarget(ScriptableObject obj)
        {
            _target = obj;
        }
#endif
    }
}
