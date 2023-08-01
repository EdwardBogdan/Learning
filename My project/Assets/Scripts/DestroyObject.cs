
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] GameObject _object;

    public void DestroyTheObject()
    {
        Destroy(_object);
    }
}
