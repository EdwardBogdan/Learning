using MyProject.Characters;
using UnityEngine;

namespace MyProject.Characters
{
    public class PlayerArming : MonoBehaviour
    {
        public void ArmPlayer(GameObject _object)
        {
            _object?.GetComponent<PlayerAnimationController>().SetArming(true);
        }
    }
}
