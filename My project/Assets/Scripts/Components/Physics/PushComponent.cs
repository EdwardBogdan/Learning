using UnityEngine;

namespace MyProject.Components
{
    public class PushComponent : MonoBehaviour
    {
        [SerializeField] bool _pushX;
        [SerializeField] float _pushForceX;
        [SerializeField] bool _pushY;
        [SerializeField] float _pushForceY;
        public void AddForce(GameObject _object)
        {
            Vector2 posThis = transform.position;
            Vector2 posObj = _object.transform.position;

            float forceX = _pushX ? (posObj.x - posThis.x) * _pushForceX : 0;
            float forceY = _pushY ? (posObj.y - posThis.y) * _pushForceY : 0;

            Vector2 direction = new(forceX, forceY);


            Rigidbody2D rb = _object.GetComponent<Rigidbody2D>();
            rb.velocity = new(0, 0);
            rb.AddForce(direction, ForceMode2D.Impulse);
        }   
    }
}

