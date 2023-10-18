using UnityEngine;
using MyProject.Physic.PAController;

namespace MyProject.Characters
{
    public class PlayerArming : MonoBehaviour
    {
        public void ArmPlayer(GameObject _object)
        {
            _object?.GetComponent<PlayerAnimatorController>().SetArming(true);
            Data.GameSession.CurrentSession._swords += 1;
        }
    }
}
