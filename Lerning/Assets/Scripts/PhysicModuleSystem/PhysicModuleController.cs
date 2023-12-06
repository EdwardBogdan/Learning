using ColliderTouchSystem;

namespace PhysicModuleSystem
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class PhysicModuleController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private TouchProperty[] _touchProperties;

        [HideInInspector]
        [SerializeField] private PhysicModuleLogicCore _core;

        private Vector2 _direction = new(0,0);

        private delegate void OnModification();
        private OnModification OnMod;

        public Rigidbody2D RigidBody => _body;
        public Vector2 Direction => _direction;
        public Vector2 Velocity => _body.velocity;

#if UNITY_EDITOR
        public PhysicModuleLogicCore Core
        {
            get => _core;
            set => _core = value;
        }
#endif

        private ModuleRef[] _currentModules;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
        internal T GetModule<T>() where T : PhysicModule
        {
            foreach (ModuleRef mod in _currentModules)
            {
                if (mod.type is T)
                {
                    return (T)mod.module;
                }
            }
            return null;
        }
        internal TouchComponent GetTouchProperty(string id)
        {
            foreach (var touch in _touchProperties)
            {
                if (touch.Id == id)
                {
                    return touch.Touch;
                }
            }
            return null;
        }

        [ContextMenu("ReActivate Modules")]
        private void ActivateModules()
        {
            OnMod = null;
            List<ModuleRef> list = new List<ModuleRef>();

            foreach (var moduleData in _core.list)
            {
                if (!moduleData.Module.Active) continue;

                PhysicModule module = Instantiate(moduleData.Module);
                OnMod += module.Modification;
                module.Activating(this);
                list.Add(new ModuleRef(module));
            }

            _currentModules = list.ToArray();
        }

        private void Awake()
        {
            ActivateModules();
        }
        private void FixedUpdate()
        {
             OnMod?.Invoke();
        }
    }

    [Serializable]
    internal class TouchProperty
    {
        [SerializeField] private string _id;
        [SerializeField] private TouchComponent _touch;

        public string Id => _id;
        public TouchComponent Touch => _touch;
    }

    internal class ModuleRef
    {
        internal Type type;
        internal PhysicModule module;

        internal ModuleRef(PhysicModule _module)
        {
            type = _module.GetType();
            module = _module;
        }
    }

    public abstract class PhysicModule : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] protected bool _active;
        internal bool Active => _active;
#endif
        public abstract string ModuleName { get; }
        public virtual void Activating(PhysicModuleController controller) { }
        public abstract void Modification();
    }
}
