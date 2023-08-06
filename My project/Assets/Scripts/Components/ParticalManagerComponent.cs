using MyProject.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Components
{
    public class ParticalManagerComponent : MonoBehaviour
    {
        [SerializeField] ParticalObject[] _particals;
        public void SummonPartical(string name)
        {
            foreach (ParticalObject par in _particals)
            {
                if (par._name == name)
                {
                    GameObject partical = par.spawner.Spawn();
                    ScriptSmartAnimator scriptSmartAnimator = partical?.GetComponent<ScriptSmartAnimator>();
                    if (scriptSmartAnimator != null)
                    {
                        scriptSmartAnimator.SetClip(name);
                        scriptSmartAnimator.Play(true);
                    }
                    break;
                }
            }
        }
        [Serializable]
        class ParticalObject
        {
            public string _name;
            public SpawnComponent spawner;
        }
    }
}
