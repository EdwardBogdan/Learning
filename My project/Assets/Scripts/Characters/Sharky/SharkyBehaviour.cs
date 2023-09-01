using MyProject.Utils;
using UnityEngine;

namespace MyProject.Characters
{
    public class SharkyBehaviour : CharacterBehaviour
    {
        [SerializeField] internal Cooldown _alarmDelay;
        [SerializeField] internal Cooldown _lostDelay;

        public override void OnTriggerDeath()
        {
            base.OnTriggerDeath();
            Destroy(GetComponent<SharkyAI>());
            Destroy(GetComponent<CharacterPhysicController>());
        }
    }
}
