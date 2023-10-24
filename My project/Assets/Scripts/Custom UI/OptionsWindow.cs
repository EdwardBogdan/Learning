
namespace MyProject.CustomUI.Windows
{
    public class OptionsWindow : AnimatedWindow
    {
        public MainMenuWindow _menu;
        public void OnOk()
        {
            _menu.SetInteract(true);

            Close();
        }
    }
}
