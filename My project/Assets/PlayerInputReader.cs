using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] PlayerControler player;

        float vert = 0, horiz = 0;
        public void OnMovementVertical(InputAction.CallbackContext context)
        {
            vert = context.ReadValue<float>();
            player.SetDirection(horiz, vert);
        }
        public void OnMovementHorizontal(InputAction.CallbackContext context)
        {
            horiz = context.ReadValue<float>();
            player.SetDirection(horiz, vert); 
        }
        public void OnSayHello(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                Debug.Log("Hello World");
            }
        }
    }

}
