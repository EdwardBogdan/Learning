using UnityEngine;

namespace MyProject.Components.Triggers
{
    public class EnterCollisionComponent : BaseTriggerComponent
    {
        public override string NameElement => "Ent.Collision";
        void OnCollisionEnter2D(Collision2D collision)
        {
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
