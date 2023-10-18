using MyProject.Components.Triggers;
using MyProject.Utils;
using System.Collections;
using UnityEngine;

namespace MyProject.AIStateMachineSpace.States
{
    public class AIState_SlimeMoving : AIState
    {
        [SerializeField] private bool moveLeft = false;
        [SerializeField] private Cooldown _waitTimeToChange;

        private TouchComponent wall;

        private Vector3 direction;

        public override string StateLogicName => "Slime Moving";
        public override IEnumerator StateLogic()
        {
            while(true)
            {
                if (wall.IsTouched && _waitTimeToChange.IsReady)
                {
                    direction *= -1;
                    _waitTimeToChange.Reset();
                }

                machine.SetDirection(direction);

                yield return new WaitForEndOfFrame();
            }
        }
        public override void AwakeState(AIStateMachine machine)
        {
            base.AwakeState(machine);

            wall = machine.PhysicController.Data.wallData;

            int x = moveLeft ? -1 : 1;

            direction = new(x, 0, 0);

            _waitTimeToChange.Reset();
        }
    }
}