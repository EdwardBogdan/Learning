using HealthSystem;
using UnityEngine;

namespace SaveSystem
{
    public class CreatureSaveComponent : SimpleSaveComponent
    {
        [SerializeField] private HealthComponent health;
        [SerializeField] private GameObject _object;

        private CreatureSaveData creatureData;

        public void OnChangeHP(int value)
        {
            creatureData.hp = value;
        }
        private void OnChangeScene()
        {
            creatureData.pos = transform.position;
        }

        protected override void Start()
        {
            CurrentSessionData.OnChangeScene += OnChangeScene;

            var set = CurrentSessionData.CurrentSceneData.CreatureData;

            if (set.TryGetSaveData(_id, out creatureData))
            {
                CheckDead();

                health.SetHealth(creatureData.hp);
                _object.transform.position = creatureData.pos;
            }
            else
            {
                creatureData = set.CreateSaveData(_id);
            }
        }

        protected override void CheckDead()
        {
            if (creatureData.isDead)
            {
                OnDeadStatus?.Invoke();
            }
        }
        private void OnDestroy()
        {
            CurrentSessionData.OnChangeScene -= OnChangeScene;
        }
    }
}