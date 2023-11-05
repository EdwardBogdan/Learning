using System.Collections.Generic;
using UnityEngine;

namespace CastSystem2D.Components
{
    public class CastCombinerComponent : CastCore
    {
        List<CastComponent> casters = new();

        private void Start()
        {
            CastComponent[] mas = GetComponents<CastComponent>();
            foreach (var item in mas)
            {
                if (item._useByCombinator)
                {
                    casters.Add(item);
                }
            }
        }
        [ContextMenu("Combined Cast")]
        public void CombinatedCast()
        {
            if (_action == null) return;

            List<Collider2D> list = new();

            foreach (var caster in casters)
            {
                List<Collider2D> listOfCaster = caster.Collect();
                foreach (var item in listOfCaster)
                {
                    if(!list.Contains(item))
                    list.Add(item);
                }
            }

            InvokeAction(list);
        }
    }
}
