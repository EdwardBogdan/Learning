using UnityEngine;

public class ScriptDirectionMoving : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _x;
    [SerializeField] private float _y;

    private float scaleX;
    private float scaleY;

    private void Start()
    {
        scaleX = (transform.localScale.x > 0 ? 1 : -1) * _x;
        scaleY = (transform.localScale.y > 0 ? 1 : -1) * _y;
    }
    private void FixedUpdate()
    {
        Vector2 vector = _transform.position;
        _transform.position = new(vector.x += scaleX, vector.y += scaleY);
    }
}
