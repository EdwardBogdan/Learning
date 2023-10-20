using MyProject.Components.Cast;
using MyProject.Components;
using MyProject.Components.Interactable;
using MyProject.Data;
using UnityEngine;
using MyProject.Physic.PAController;
using MyProject.Attributes;
using MyProject.Components.Health;

namespace MyProject.Characters
{
    public class PlayerActionSet : BaseActionSet
    {
        [InventoryId][SerializeField] string _swordId;
        [InventoryId][SerializeField] string _coinId;
        [InventoryId][SerializeField] string _healPotionId;
        [SerializeField] int _healByPotion;
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
            GameSession.CurrentSession.PersonData.HP = health;
        }
        public void OnDisposeCoin()
        {
            var _numCoinToDispose = Mathf.Min(GameSession.CurrentSession.Inventory.GetCount(_coinId), _lostCoins);

            DropComponent _drop = _droper.GetComponent<DropComponent>();
            _drop.ChangeDropCount(0, _numCoinToDispose);

            GameSession.CurrentSession.Inventory.Remove(_coinId, _numCoinToDispose);
            _drop.Drop();
        }
        public void OnThrow()
        {
            GameSession.CurrentSession.Inventory.Remove(_swordId, 1);
            if (GameSession.CurrentSession.Inventory.GetCount(_swordId) > 0) return;

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
        public void OnUseHeal()
        {
            InventoryData inventory = GameSession.CurrentSession.Inventory;

            int count = inventory.GetCount(_healPotionId);
            if (count > 0)
            {
                inventory.Remove(_healPotionId, 1);
                var healtComp = GetComponent<HealthComponent>();
                healtComp.ApplyHeal(_healByPotion);
            }
            else
            {
                Debug.Log("You have not enough potions");
            }
        }
    }
}

