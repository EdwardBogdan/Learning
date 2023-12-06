using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem
{
    public class LoadSceneComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private string _spawnID;
        [SerializeField] private bool _clearMemory;

        public void LoadScene()
        {
            if (_clearMemory) CurrentSessionData.ClearSessionData();

            CurrentSessionData.ChangeSpawnPoint(_spawnID);

            CurrentSessionData.LoadScene(_sceneName);
        }
    }
}