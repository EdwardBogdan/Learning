using MyProject.Player;
using UnityEngine;

namespace MyProject.Player
{
    public class PlayerArming : MonoBehaviour
    {
        public void ArmPlayer(GameObject _object)
        {
            _object?.GetComponent<PlayerAnimationController>().SetArming(true);
        }
    }
}
