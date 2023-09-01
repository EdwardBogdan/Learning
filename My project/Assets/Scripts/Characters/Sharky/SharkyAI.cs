using MyProject.Components;
using MyProject.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject.Characters
{
    public class SharkyAI : MonoBehaviour
    {
        [SerializeField] Patrol _patrol;

        [SerializeField] TouchComponent _targerIsVisible;
        [SerializeField] TouchComponent _attackbleArea;

        ParticalManagerComponent _particals;
        CharacterPhysicController _physicController;
        SharkyBehaviour _behaviour;

        GameObject _target;
        Coroutine _current;
        private void Awake()
        {
            _physicController = GetComponent<CharacterPhysicController>();
            _behaviour = GetComponent<SharkyBehaviour>();
            _particals = GetComponent<ParticalManagerComponent>();
        }
        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }
        public void StartState(IEnumerator _coroutine)
        {
            if (_current != null)
            {
                StopCoroutine(_current);
            }
            _current = StartCoroutine(_coroutine);
        }
        public void OnTargetIsVisible(GameObject _object)
        {
            _target = _object;

            StartState(AgroToTarget());
        }
        public void OnTargetIsNotVisible(GameObject _object)
        {
            StartState(_patrol.DoPatrol());
        }
        private IEnumerator AgroToTarget()
        {
            SetDirectionToTarget();
            _physicController.SetDirection(Vector2.zero);

            Cooldown alarm = _behaviour._alarmDelay;
            if (alarm.IsReady)
            {
                _particals.SummonPartical("Dialog Dead");
                alarm.Reset();
            }

            while (!alarm.IsReady)
            {
                yield return null;
            }
            StartState(GoToTarger());
        }
        private IEnumerator GoToTarger()
        {
            while (_targerIsVisible.IsTouched)
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
        }
        private IEnumerator LostTarget()
        {
            Cooldown delay = _behaviour._lostDelay;
            if (delay.IsReady)
            {
                _particals.SummonPartical("Dialog Dead");
                delay.Reset();
            }

            while (!delay.IsReady)
            {
                yield return null;
            }
        }
        private IEnumerator Attack()
        {
            while (_attackbleArea.IsTouched)
            {
                _behaviour.OnTriggerAttack();
                _physicController.SetDirection(Vector2.zero);
                yield return null;
            }
            StartState(GoToTarger());
        }
        private void SetDirectionToTarget()
        {
            Vector2 _direction = _target.transform.position - transform.position;
            _direction.y = 0;
            _physicController.SetDirection(_direction);
        }
    }
}

