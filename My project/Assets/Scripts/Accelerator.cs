using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    [SerializeField] string[] _tags;
    [SerializeField] float _power;
    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in _tags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                var body = other.transform.GetComponent<Rigidbody2D>();
                body.AddForce(Vector2.up * _power, ForceMode2D.Impulse);
                break;
            }
        }
    }
}
