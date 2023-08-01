using UnityEngine;

namespace MyProject.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] UnityEvent_GameObject _action;
        [SerializeField] string[] _tags;

        void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (string tag in _tags)
            {
                if (collision.gameObject.CompareTag(tag))
                {
                    _action?.Invoke(collision.gameObject);
                    break;
                }
            }
        }
    }
}
