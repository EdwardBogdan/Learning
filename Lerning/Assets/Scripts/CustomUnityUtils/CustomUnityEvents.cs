using System;
using UnityEngine;
using UnityEngine.Events;

namespace CustomUnityUtils.UnityEvents
{
    [Serializable]
    public class UnityEvent_GameObject : UnityEvent<GameObject>
    {

    }

    [Serializable]
    public class UnityEvent_Int : UnityEvent<int>
    {

    }
    [Serializable]
    public class UnityEvent_String : UnityEvent<string>
    {

    }
}
