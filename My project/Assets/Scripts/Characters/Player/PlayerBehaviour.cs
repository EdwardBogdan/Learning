using MyProject.Components;
using MyProject.Data;
using UnityEngine;

namespace MyProject.Characters
{
    public class PlayerBehaviour : CharacterBehaviour
    {
        [SerializeField] int _lostCoins;
        [SerializeField] CircleAllCastComponent _castInteract;
        public void OnHealthChange(int health)
        {
            GameSession.CurrentSession._playerHP = health;
        }
        public void OnDisposeCoin()
        {
            var _numCoinToDispose = Mathf.Min(GameSession.CurrentSession._coins, _lostCoins);

            DropComponent _drop = GetComponent<DropComponent>();
            _drop.ChangeDropCount(0, _numCoinToDispose);

            GameSession.CurrentSession._coins -= _numCoinToDispose;
            _drop.Drop();
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

