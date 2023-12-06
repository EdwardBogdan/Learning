using System;
using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem
{
    [RequireComponent(typeof(SpawnPointComponent))]
    public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnChecked;
        [SerializeField] private UnityEvent OnUnchecked;
        private SpawnPointComponent point;

        private static Action OnChanged;

        public void SetCheckPoint()
        {
            OnChanged?.Invoke();
            OnChecked?.Invoke();
            CurrentSessionData.SetCheckPoint(new(point.ID));
        }

        private void Awake()
        {
            point = GetComponent<SpawnPointComponent>();
            OnChanged += OnPointChanged;

            if (CurrentSessionData.SpawnPosID == point.ID)
            {
                OnChecked?.Invoke();
            }
        }

        private void OnPointChanged()
        {
            OnUnchecked?.Invoke();
        }
        private void OnDestroy()
        {
            OnChanged -= OnPointChanged;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void CheckPointLoader()
        {
            CurrentSessionData.OnLoad += OnLoad;
        }
        private static void OnLoad()
        {
            var data = CurrentSessionData.checkPoint;
            if (data == null) return;

            CurrentSessionData.ChangeSpawnPoint(data.SpawnPosID);
        }
    }
}
