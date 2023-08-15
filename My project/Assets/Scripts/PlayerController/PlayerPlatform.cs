using System.Collections;
using UnityEngine;

namespace MyProject.Player
{
    public class PlayerPlatform : MonoBehaviour
    {
        [SerializeField] float _ignoreTime = 0.25f;
        Collider2D _currentCollider;
        PlayerPhysicController _player;

        void Awake()
        {
            _player = GetComponent<PlayerPhysicController>();
        }
        void FixedUpdate()
        {
            if (_player.GetDirection().y < 0)
            {
                if (_currentCollider != null)
                {
                    StartCoroutine(DisableCollision());
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                _currentCollider = collision.collider;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                _currentCollider = null;
            }
        }
        private IEnumerator DisableCollision()
        {
            Collider2D playerCollider = _player.GetComponent<Collider2D>();
            Collider2D _oldCollider = _currentCollider;
            Physics2D.IgnoreCollision(playerCollider, _oldCollider);
            yield return new WaitForSeconds(_ignoreTime);
            Physics2D.IgnoreCollision(playerCollider, _oldCollider, false);
        }
    }

}
