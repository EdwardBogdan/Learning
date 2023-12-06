using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem
{
    public class SimpleSaveComponent : IDComponent
    {
        [SerializeField] protected UnityEvent OnDeadStatus;

        protected SimpleSaveData data;
        public void SetDestroyed(bool value)
        {
            data.isDead = value;
        }

        protected virtual void Start()
        {
            var dataSet = CurrentSessionData.CurrentSceneData.SimpleData;

            if (dataSet.TryGetSaveData(_id, out data))
            {
                CheckDead();
            }
            else
            {
                data = dataSet.CreateSaveData(_id);
            }
        }
        protected virtual void CheckDead()
        {
            if (data.isDead)
            {
                OnDeadStatus?.Invoke();
            }
        }
    }
}
