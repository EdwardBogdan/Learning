using MyProject.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyProject.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particleSystem;
        [SerializeField] HandleCircleAllCastComponent _castAttackSimple;
        [SerializeField] HandleCircleAllCastComponent _castInteract;


        DamageMakerComponent _damageComponent;
        PlayerAnimationController _playerAnimator;


        void Awake()
        {
            _damageComponent = GetComponent<DamageMakerComponent>();
            _playerAnimator = GetComponent<PlayerAnimationController>();
        }

        #region Attacks
        public void AttackSimpleTriger()
        {
            _playerAnimator.TriggerAttackSimple();
        }
        public void AttackSimpleAnimationCast()
        {
            _castAttackSimple.Cast();
        }
        #endregion

        #region Death
        public void Death()
        {
            _playerAnimator.TriggerDeathHit();
            //string currentSceneName = SceneManager.GetActiveScene().name;
            //SceneManager.LoadScene(currentSceneName);
            //Debug.Log("����, �������, �� ����, ������");
        }
        #endregion
        public void TakeDamage()
        {
            _playerAnimator.TriggerHit();
            Debug.Log($"HP: {GetComponent<HealthComponent>().Health}");

            DisposeCoin();
        }
        public void DisposeCoin()
        {
            var _numCoinToDispose = Mathf.Min(CoinTriger._count, 5);
            CoinTriger._count -= _numCoinToDispose;
            var burst = _particleSystem.emission.GetBurst(0);
            burst.count = _numCoinToDispose;
            _particleSystem.emission.SetBurst(0, burst);
            //_particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }
        
        public void InteractTriger()
        {
            _castInteract.Cast();
        }
        public void InteractAction(GameObject _object)
        {
            _object?.GetComponent<InteractableComponent>()?.Interact();
        }
        
        
        
    }
}
