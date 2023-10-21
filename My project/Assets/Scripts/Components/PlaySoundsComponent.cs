using System;
using UnityEngine;

namespace MyProject.Components
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _clips;

        public void Play(string id)
        {
            foreach (var data in _clips)
            {
                if (data.Id != id) continue;

                _source.PlayOneShot(data.Clip);
                break;
            }
        }

        [Serializable]
        public class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }
}
