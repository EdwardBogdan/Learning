using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCouple : MonoBehaviour
{
    [SerializeField] string[] _tags;
    [SerializeField] float _pushForce = 10f;
    [SerializeField] float _timeForce = 10f;
    [SerializeField] PortalCouple _couple;
    [SerializeField] Transform _exit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in _tags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                other.transform.position = _couple._exit.position;
                StartCoroutine(GetForce(other));
                break;
            }
        }
    }
    private IEnumerator GetForce(Collider2D other)
    {
        Vector2 direction = _couple._exit.transform.position - _couple.transform.position;
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        float elapsedTime = 0f;
        while (true)
        {
            rb.AddForce(direction * _pushForce, ForceMode2D.Impulse);
            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime >= _timeForce)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnDrawGizmos()
    {
        if (_couple != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _couple.transform.position);
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position, new(0.3f, 0.3f, 0));
        }

    }
}
