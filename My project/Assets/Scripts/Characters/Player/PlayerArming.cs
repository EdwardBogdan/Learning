using MyProject.Data;
using MyProject.Physic.PAController;
using UnityEngine;

namespace MyProject.Characters.Player
{
    public class PlayerArming : MonoBehaviour
    {
        public void Arming()
        {
            var session = GameSession.CurrentSession;
            session.Player.GetComponent<PlayerAnimatorController>().SetArming(session.PersonData.IsArmed());
        }
    }
}
