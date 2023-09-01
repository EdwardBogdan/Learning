using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Components
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}

