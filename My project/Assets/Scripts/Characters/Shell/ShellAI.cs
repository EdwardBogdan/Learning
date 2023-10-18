using MyProject.Components.Triggers;
using System.Collections;
using UnityEngine;
using MyProject.Physic.PAController;

namespace MyProject.Characters.Shell
{
    public class ShellAI : BaseAI
    {
        [SerializeField] protected TouchComponent _throwbleArea;
        ShellAnimatorController Animator => _animator as ShellAnimatorController;
        protected override IEnumerator GoToTarget()
        {
            _currentStateName = "Go to target";
            while (_targetInChaseArea.IsTouched && VisionRay())
            {
                if (_attackbleArea.IsTouched)
                {
                    StartState(Attack());
                }
                else if (_throwbleArea.IsTouched)
                {
                //    StartState(Throw());
                }
                SetDirectionToTarget();
                yield return null;
            }

            StartState(LostTarget());
        }
    }
}
