using UnityEngine;
using UnityEngine.Events;

namespace MyProject.Components
{
    public class EnterTrigerComponent : BaseTriggerComponent
    {
        public override string NameElement => "Ent.Trigger";

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool _layerTest = true, _tagTest = true;
            if (_checkByTag)
            {
                _tagTest = CheckTag(other.gameObject);
            }
            if (_checkByLayer)
            {
                _layerTest = CheckLayer(other.gameObject);
            }

            if (_tagTest && _layerTest)
            {
                _action?.Invoke(null);
            }
        }
    }
}
