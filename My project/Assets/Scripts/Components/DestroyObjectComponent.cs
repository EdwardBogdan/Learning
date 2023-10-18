using UnityEngine;

namespace MyProject.Components.Shutdown
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] Object[] _objects;

        [ContextMenu("Destroy Objects")]
        public void DestroyObjects()
        {
            foreach (Object _object in _objects)
            {
                Destroy(_object);
            }
        }
    }
}