using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoundsAndUI.Windows
{
    public class MainMenuWindow : AnimatedWindow
    {
        [SerializeField] private GameObject _optionMenuPrefab;
        [SerializeField] private GameObject _hud;

        public void OnNewGame()
        {
            _closeAction = () => 
            { 
                //CustomSceneManager.LoadScene("Test");
                Instantiate(_hud, CanvasTransform);
            };
            TriggerClose();
        }
        public void OnContinue()
        {
            //_closeAction = () => { SceneManager.LoadScene("Level1"); };
            //TriggerClose();
        }
        public void OnChooseLevel()
        {
            //_closeAction = () => { SceneManager.LoadScene("Level1"); };
            //TriggerClose();
        }
        public void OnOptions()
        {
            var canvas = FindObjectOfType<Canvas>();

            _closeAction = () =>
            {
                Instantiate(_optionMenuPrefab, CanvasTransform);
            };

            TriggerClose();
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
            TriggerClose();
        }
    }
}
