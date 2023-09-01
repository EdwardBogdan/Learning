using MyProject.Components;
using UnityEngine;

public class ExitTriggerPassComponent : BaseTriggerComponent
{
    public override string NameElement => "Exit.Trig.Pass";
    private void OnTriggerExit2D(Collider2D other)
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
