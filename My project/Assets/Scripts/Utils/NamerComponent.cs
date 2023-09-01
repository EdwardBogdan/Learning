using MyProject.Utils;
using UnityEngine;

public class NamerComponent : MonoBehaviour , INaming
{
    [SerializeField] string _name;
    public string NameElement => $"{_name} -";

    [ContextMenu("Rename")]
    public void OnValidate()
    {
        gameObject.name = AllNames();
    }
    private string AllNames()
    {
        Component[] components = GetComponents<MonoBehaviour>();
        string _name = "";

        foreach (var component in components)
        {
            if (component is INaming namingComponent)
            {
                _name += namingComponent.NameElement;
                _name += " ";
            }
        }
        return _name;
    }
}
