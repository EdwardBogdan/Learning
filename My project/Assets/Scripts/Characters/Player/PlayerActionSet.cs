using MyProject.Components.Cast;
using MyProject.Components;
using MyProject.Components.Interactable;
using MyProject.Data;
using UnityEngine;
using MyProject.Physic.PAController;

namespace MyProject.Characters
{
    public class PlayerActionSet : BaseActionSet
    {
        [SerializeField] int _lostCoins;
        [SerializeField] CastComponent _castInteract;
        [SerializeField] DropComponent _droper;
        private PlayerAnimatorController _animator;

        private void Awake()
        {
            _animator = GetComponent<PlayerAnimatorController>();
        }
        public void OnHealthChange(int health)
        {
            GameSession.CurrentSession._playerHP = health;
        }
        public void OnDisposeCoin()
        {
            var _numCoinToDispose = Mathf.Min(GameSession.CurrentSession._coins, _lostCoins);

            DropComponent _drop = _droper.GetComponent<DropComponent>();
            _drop.ChangeDropCount(0, _numCoinToDispose);

            GameSession.CurrentSession._coins -= _numCoinToDispose;
            _drop.Drop();
        }
        public void OnThrow()
        {
            GameSession.CurrentSession._swords -= 1;
            if (GameSession.CurrentSession._swords > 0) return;

            _animator.SetArming(false);
        }
        public void OnTriggerInteract()
        {
            _castInteract.Cast();
        }
        public void InteractAction(GameObject _object)
        {
            InteractableComponent interactable = _object?.GetComponent<InteractableComponent>();
            interactable?.Interact();
        }
    }
}

