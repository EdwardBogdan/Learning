using System;
using UnityEngine;

namespace SoundsAndUI.Components
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _clips;

        public void PlaySound(string id)
        {
            foreach (var data in _clips)
            {
                if (data.Id != id) continue;

                _source.PlayOneShot(data.Clip);
                break;
            }
        }

        [Serializable]
        private class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            internal string Id => _id;
            internal AudioClip Clip => _clip;
        }
    }
}
