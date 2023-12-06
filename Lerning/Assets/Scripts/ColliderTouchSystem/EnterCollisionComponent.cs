using UnityEngine;

namespace ColliderTouchSystem
{
    public class EnterCollisionComponent : BaseTriggerComponent
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            bool _layerTest = true, _tagTest = true, _ignoreTest = true;

            GameObject _object = collision.gameObject;
            if (_checkByTag)
            {
                _tagTest = CheckTag(_object);
            }
            if (_ignoreByTag)
            {
                _ignoreTest = TagIgnore(_object);
            }
            if (_checkByLayer)
            {
                _layerTest = CheckLayer(_object);
            }

            if (_tagTest && _layerTest && _ignoreTest)
            {
                _action?.Invoke(_object);
            }
        }
    }
}
