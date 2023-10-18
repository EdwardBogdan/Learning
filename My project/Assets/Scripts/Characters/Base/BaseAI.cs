using MyProject.Components.Triggers;
using System.Collections;
using MyProject.Utils;
using UnityEngine;
using MyProject.Components.Spawn;
using MyProject.Physic.Modules;
using MyProject.Physic.PAController;

namespace MyProject.Characters
{
    public abstract class BaseAI : MonoBehaviour
    {
        public string _currentStateName;

        //[SerializeField] protected Patrol _patrol;

        [SerializeField] protected Cooldown _lostTargetDelay;
        [SerializeField] protected Cooldown _alarmDialogDelay;

        [SerializeField] protected LayerMask _layerToVisionRay;

        [SerializeField] protected TouchComponent _targetInVisible;
        [SerializeField] protected TouchComponent _targetInChaseArea;
        [SerializeField] protected TouchComponent _attackbleArea;

        protected BaseAnimatorController _animator;
        protected SpawnManager _particals;
        protected PhysicModuleController _physicController;

        protected GameObject _target;
        protected Coroutine _currentState;
        private Vector2 TargetPos => _target.transform.position;
        private Vector2 ThisPos => transform.position;
        protected virtual void Awake()
        {
            _animator = GetComponent<BaseAnimatorController>();
            _physicController = GetComponent<PhysicModuleController>();
            _particals = GetComponent<SpawnManager>();
        }
        protected void Start()
        {
            _currentStateName = "patrol";
            //StartState(_patrol.PatrolLogic());
        }
        protected virtual IEnumerator AgroToTarget()//
        {
            _currentStateName = "Agro";

            if (VisionRay())
            {
                SetDirectionToTarget();
                _physicController.SetDirection(Vector2.zero);

                if (_alarmDialogDelay.IsReady)
                {
                    _particals.SpawnTarget("Dialog Dead");
                    _alarmDialogDelay.Reset();
                }
                while (!_alarmDialogDelay.IsReady)
                {
                    yield return null;
                }
                StartState(GoToTarget());
            }
            else
            {
                StartState(LostTarget());
            }
        }
        protected virtual IEnumerator Attack()//
        {
            _currentStateName = "Attack";
            while (_attackbleArea.IsTouched)
            {
                _animator.ACTriggerAttack();
                _physicController.SetDirection(Vector2.zero);
                yield return null;
            }
            StartState(GoToTarget());
        }
        protected virtual IEnumerator LostTarget()//
        {
            _currentStateName = "Lost";
            if (_target != null)
            {
                _physicController.SetDirection(Vector2.zero);

                if (_lostTargetDelay.IsReady) _particals.SpawnTarget("Dialog Request");

                _lostTargetDelay.Reset();

                while (!_lostTargetDelay.IsReady)
                {
                    if (_targetInChaseArea.IsTouched && VisionRay())
                    {
                        StartState(GoToTarget());
                    }
                    yield return null;
                }
            }

            _target = null;
            _currentStateName = "patrol";
           // StartState(_patrol.PatrolLogic());
        }
        protected virtual IEnumerator GoToTarget()//
        {
            _currentStateName = "Go to target";
            while (_targetInChaseArea && VisionRay())
            {
                if (_attackbleArea.IsTouched)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }

            StartState(LostTarget());
        }

        #region Tools
        public virtual void StartState(IEnumerator _coroutine)//
        {
            if (_currentState != null)
            {
                StopCoroutine(_currentState);
            }
            _currentState = StartCoroutine(_coroutine);
        }
        public virtual void OnTargetIsVisible(GameObject _object)
        {
            if (_target != null) return;

            _target = _object;

            StartState(AgroToTarget());
        }
        public virtual void OnTargetIsNotInArea(GameObject _object)
        {
            StartState(LostTarget());
        }
        protected void SetDirectionToTarget()
        {
            Vector2 _direction = _target.transform.position - transform.position;
            _physicController.SetDirection(_direction);
        }
        protected bool VisionRay()
        {
            float raycastDistance = Vector2.Distance(ThisPos, TargetPos);

            Vector2 vector = TargetPos - ThisPos;

            bool touch = Physics2D.Raycast(ThisPos, vector, raycastDistance, _layerToVisionRay);

            Color color = touch ? Color.red : Color.green;

            Debug.DrawRay(ThisPos, vector, color);

            return !touch;
        }
        #endregion
    }
}
