using System;
using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Utils
{
    [Serializable]
    public class UnityEvent_GameObject : UnityEvent<GameObject>
    {

    }

    [Serializable]
    public class UnityEvent_Int : UnityEvent<int>
    {

    }
}
