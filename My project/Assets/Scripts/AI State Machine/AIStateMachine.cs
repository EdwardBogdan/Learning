using MyProject.Components.Spawn;
using MyProject.Components.Triggers;
using MyProject.Physic.Modules;
using System;
using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace
{
    public class AIStateMachine : MonoBehaviour
    {
        public string _currentStateName;

        [HideInInspector] public string folderName = "Test Logic";

        [SerializeField] private AIState _startState;

        [SerializeField] private LayerMask _layerToVisionRay;

        [SerializeField] private AreaReference[] _areas;

        private Coroutine currentStateLogic;

        public PhysicModuleController PhysicController { get; private set; }
        public GameObject Target { get; private set; }

        public SpawnManager Spawner { get; private set; }

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
        public void SetDirection(Vector2 targetPos)
        {
            PhysicController.SetDirection(targetPos);
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
            currentStateLogic = StartCoroutine(newState.StateLogic());
        }
        private bool SendVisionRay()
        {
            float raycastDistance = Vector2.Distance(SelfPos, TargetPos);

            Vector2 vector = DirectionToTarget;

            bool touch = Physics2D.Raycast(SelfPos, vector, raycastDistance, _layerToVisionRay);

            Color color = touch ? Color.red : Color.green;

            Debug.DrawRay(SelfPos, vector, color);

            return !touch;
        }
        private void Awake()
        {
            PhysicController = GetComponent<PhysicModuleController>();
            Spawner = GetComponent<SpawnManager>();
        }
        private void Start()
        {
            StartState(_startState);
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

    [Serializable]
    class AreaReference
    {
        public string _name;
        public TouchComponent area;
    }
}
