using UnityEngine;

namespace MyProject.Components.Triggers
{
    public class EnterTriggerComponent : BaseTriggerComponent
    {
        public override string NameElement => "Ent.Trig.";
        private void OnTriggerEnter2D(Collider2D other)
        {
            bool _layerTest = true, _tagTest = true;
            GameObject _object = other.gameObject;
            if (_checkByTag)
            {
                _tagTest = CheckTag(_object);
            }
            if (_checkByLayer)
            {
                _layerTest = CheckLayer(_object);
            }

            if (_tagTest && _layerTest)
            {
                _action?.Invoke(_object);
            }
        }
    }
}
