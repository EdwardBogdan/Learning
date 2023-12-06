using System.Collections;
using UnityEngine;
using ColliderTouchSystem;
using AnimatorController;
using PhysicModuleSystem;
using UnityEngine.Events;

namespace AIStateSystem.States
{
    public class AIState_AnimationAreaEvent : AIState
    {
        [SerializeField] private string _eventKey;
        [AIAreaChoose, SerializeField] private string _touchAreaName;
        [AIComponentRef, SerializeField] private string _animatorControllerName;
        [AIComponentRef, SerializeField] private string _physicControllerName;
        [Min(0.1f),SerializeField] private float _eventCD;

        [Header("Exits")]
        [AIStateChoose, SerializeField] private AIState _targetOutArea;

        private TouchComponent _area;

        private UnityEvent _event;
        private PhysicModuleController _physic;

        public override string StateLogicName => "Animation Event";
        public override IEnumerator StateLogic()
        {
            while (_area.IsTouched && machine.VisionRay)
            {
                _physic.SetDirection(Vector2.zero);
                _physic.RigidBody.velocity = new(0, 0);

                _event.Invoke();

                yield return new WaitForSeconds(_eventCD);
            }
             machine.StartState(_targetOutArea);
        }
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);
            CreatureAnimatorController animator = machine.GetComponentReference<CreatureAnimatorController>(_animatorControllerName);
            _event = animator.GetEvent(_eventKey);
            _physic = machine.GetComponentReference<PhysicModuleController>(_physicControllerName);
            _area = machine.GetArea(_touchAreaName);
        }
    }
}
