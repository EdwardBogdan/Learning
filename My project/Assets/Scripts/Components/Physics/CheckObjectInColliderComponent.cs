using MyProject.Components;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjectInColliderComponent : MonoBehaviour
{
    [SerializeField] TouchComponent _touch;

    private List<GameObject> _inCollider = new();

    public void AddSurface(GameObject _object)
    {
        _inCollider.Add(_object);
        _touch.Touch(_object);
    }
    public void RemoveSurface(GameObject _object)
    {
        _inCollider.Remove(_object);

        if (_inCollider.Count <= 0) _touch.Touch(null);
    }
}
