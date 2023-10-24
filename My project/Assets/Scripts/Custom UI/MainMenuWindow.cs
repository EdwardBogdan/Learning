using System;
using UnityEngine;

namespace MyProject.CustomUI.Windows
{
    public class MainMenuWindow : AnimatedWindow
    {
        [SerializeField] private GameObject _optionPanel;
        [SerializeField] private CanvasGroup _group;

        private Action _closeAction;
        

        public void OnShowSettings()
        {
            var canvas = FindObjectOfType<Canvas>();
            var options = Instantiate(_optionPanel, canvas.transform);

            options.GetComponent<OptionsWindow>()._menu = this;

            SetInteract(false);
        }

        public void OnStartGame()
        {
            //_closeAction = () => { SceneManager.LoadScene("Level1"); };
            Close();
        }

        public void OnExit()
        {
            _closeAction = () =>
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        }
        public void SetInteract(bool value)
        {
            _group.interactable = value;
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();

            _closeAction?.Invoke();
        }
    }
}
