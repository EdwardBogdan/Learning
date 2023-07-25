using UnityEngine;

namespace MyProject.Components
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _speed;

        private void LateUpdate()
        {
            Vector3 targetPosition = _target.position;
            Vector3 destination = new(targetPosition.x, targetPosition.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _speed);
        }
    }
}
