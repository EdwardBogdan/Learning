using MyProject.Utils;
using UnityEngine;

namespace MyProject.Components
{
    public class ItemComponent : MonoBehaviour, INaming
    {
        public string NameElement => "Item";
    }
}

