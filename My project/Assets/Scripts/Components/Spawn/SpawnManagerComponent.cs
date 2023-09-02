using MyProject.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Components
{
    public class SpawnManagerComponent : MonoBehaviour
    {
        [SerializeField] SpawnObject[] _objects;
        public void SummonPartical(string name)
        {
            foreach (SpawnObject par in _objects)
            {
                if (par._name == name)
                {
                    GameObject partical = par.spawner.SpawnObject();
                    ScriptSmartAnimator scriptSmartAnimator = partical?.GetComponent<ScriptSmartAnimator>();
                    if (scriptSmartAnimator != null)
                    {
                        scriptSmartAnimator.Play(true);
                    }
                    break;
                }
            }
        }
        [Serializable]
        class SpawnObject
        {
            public string _name;
            public SpawnComponent spawner;
        }
    }
}
