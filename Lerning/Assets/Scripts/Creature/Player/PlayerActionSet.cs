using CastSystem2D.Components;
using CreatureController;
using Interactable;
using SpawnEquipment;
using UnityEngine;

namespace PlayerController
{
    public class PlayerActionSet : CreatureActionSet
    {
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private SpawnManager _spawner;
        [SerializeField] private CastComponent _interactCast;


        private void Awake()
        {
            PlayerCore.SpawnManager = _spawner;
        }
        public void InteractCast()
        {
            _interactCast.Cast();
        }

        public void InteractAction(GameObject _object)
        {
            InteractableComponent interactable = _object?.GetComponent<InteractableComponent>();
            interactable?.Interact();
        }
    }
}
