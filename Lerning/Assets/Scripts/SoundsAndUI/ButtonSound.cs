using UnityEngine;
using UnityEngine.EventSystems;
using Core;

namespace SoundsAndUI.UIElements
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _audioClip;

        public void OnPointerClick(PointerEventData eventData)
        {
            SystemCore.SFXSource.PlayOneShot(_audioClip);
        }
    }
}
