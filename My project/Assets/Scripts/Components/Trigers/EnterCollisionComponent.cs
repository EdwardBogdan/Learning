using UnityEngine;

namespace MyProject.Components
{
    public class EnterCollisionComponent : BaseTriggerComponent
    {
        [SerializeField] bool _enabled;
        public override string NameElement => "Ent.Collision";
        public void SetEnabled(bool _value)
        {
            _enabled = _value;
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_enabled) return;

            bool _layerTest = true, _tagTest = true;

            GameObject _object = collision.gameObject;
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
