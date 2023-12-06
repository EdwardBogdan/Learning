using System;
using ColliderTouchSystem;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace AIStateSystem
{
    public class AIStateMachine : MonoBehaviour
    {
        public string _currentStateName;

        public AILogicCore Core;

        [SerializeField] private LayerMask _obstructionVisionLayer;

        [AIStateChoose][SerializeField] private AIState _startState;

        [SerializeField] private AreaReference[] _areas;
        [SerializeField] private ComponentReference[] _components;

        private Coroutine currentStateLogic;
        public GameObject Target { get; private set; }

        public bool VisionRay => Target != null && SendVisionRay();
        public Vector3 DirectionToTarget => Target != null ? TargetPos - SelfPos : Vector3.zero;
        public Vector3 TargetPos => Target.transform.position;
        public Vector3 SelfPos => transform.position;

        public TouchComponent GetArea(string name)
        {
            foreach (AreaReference par in _areas)
            {
                if (par._name == name)
                {
                    return par.area;
                }
            }
            Debug.Log($"Area \"{name}\" isn`t found. The request was from {transform.name}");
            return null;
        }
        public T GetComponentReference<T>(string name) where T : Component
        {
            ComponentReference foundComponent = Array.Find(_components, par => par._name == name && par.component is T);

            return foundComponent == null ? null : (T)foundComponent.component;
        }

        public void SetTarget(GameObject obj)
        {
            Target = obj;
        }
        public void StartState(AIState state)
        {
            if (currentStateLogic != null)
            {
                StopCoroutine(currentStateLogic);
            }
            AIState newState = Instantiate(state);
            newState.AwakeState(this);
            _currentStateName = newState.StateLogicName;
            Debug.Log(_currentStateName);
            currentStateLogic = StartCoroutine(newState.StateLogic());
        }
        private bool SendVisionRay()
        {
            float raycastDistance = Vector2.Distance(SelfPos, TargetPos);

            Vector2 vector = DirectionToTarget;

            bool touch = Physics2D.Raycast(SelfPos, vector, raycastDistance, _obstructionVisionLayer);

            Color color = touch ? Color.red : Color.green;

            Debug.DrawRay(SelfPos, vector, color);

            return !touch;
        }
        private void Start()
        {
            StartState(_startState);
        }

#if UNITY_EDITOR
        public string[] GetComponentRefNames()
        {
            List<string> list = new();
            foreach (var item in _components)
            {
                list.Add(item._name);
            }
            return list.ToArray();
        }
        public string[] GetAreaNames()
        {
            List<string> list = new();
            foreach (var item in _areas)
            {
                list.Add(item._name);
            }
            return list.ToArray();
        }
#endif

        [Serializable]
        private class AreaReference
        {
            public string _name;
            public TouchComponent area;
        }
        [Serializable]
        private class ComponentReference
        {
            public string _name;
            public Component component;
        }
    }

    public abstract class AIState : ScriptableObject
    {
        protected AIStateMachine machine;

        [HideInInspector] public string stateName = "nameless";
        public virtual string StateLogicName => "Logic hasn`t name";
        public virtual void AwakeState(AIStateMachine machine)
        {
            this.machine = machine;
        }
        public abstract IEnumerator StateLogic();
    }
}
