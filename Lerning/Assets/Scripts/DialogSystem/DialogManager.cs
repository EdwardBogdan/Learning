using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private DialogData[] _dialogs;
        public void StartDialog(string name)
        {
            foreach (var item in _dialogs)
            {
                if (item.DialogName == name)
                {
                    DialogUIManager.StartDialog(item);
                    return;
                }
            }

            Debug.Log($"Dialog {name} is not exist, call from: {gameObject.name}");
        }
    }
    [Serializable]
    internal class DialogData
    {
        [SerializeField] private string _dialogName;
        [SerializeField] private PhraseData[] _dialog;

        internal string DialogName => _dialogName;
        internal PhraseData[] Dialog => _dialog;
    }

    [Serializable]
    internal class PhraseData
    {
        [SerializeField] private Sprite _face;
        [SerializeField] private bool _side;
        [SerializeField] private string _phraseKey;
        [SerializeField] private UnityEvent _event;

        internal string PhraseKey => _phraseKey;
        internal bool Side => _side;
        internal Sprite Face => _face;
        internal UnityEvent Event => _event;
    }
}
