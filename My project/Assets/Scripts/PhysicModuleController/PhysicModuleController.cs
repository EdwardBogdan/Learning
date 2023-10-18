using UnityEngine;
using System.Collections.Generic;
using MyProject.Components.Triggers;
using UnityEditor;

namespace MyProject.Physic.Modules
{
    public class PhysicModuleController : MonoBehaviour
    {
        [SerializeField] private TouchComponent _wall;
        [SerializeField] private TouchComponent _ground;

        [HideInInspector] public string folderName = "Test Folder";

        readonly ControllDataPack _data = new();
        public ControllDataPack Data => _data;

        private List<IPhysicModule> _modules;

        private Rigidbody2D _rb;
        private Vector2 _direction;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _data.wallData = _wall;
            _data.groundData = _ground;
            _data.body = gameObject;
            _data.directionData = _direction;
            _data.velocityData = _rb.velocity;

            ActivateCurrentModuls();
        }
        private void FixedUpdate()
        {
            _data.directionData = _direction;
            _data.velocityData = _rb.velocity;
            
            foreach (IPhysicModule module in _modules)
            {
                _data.velocityData = module.Modification(_data);
            }
            _rb.velocity = _data.velocityData;
        }
        public void SetDirection(Vector2 vector)
        {
            _direction = vector;
            _direction.Normalize();
        }

        [ContextMenu("ActivateCurrentModuls")]
        public void ActivateCurrentModuls()
        {
            string ModuleFolderPath = "Assets/Resources/Physic Moduls/";
            string[] filesInFolder = System.IO.Directory.GetFiles($"{ModuleFolderPath}{folderName}");
            List<IPhysicModule> modules = new List<IPhysicModule>();

            foreach (string filePath in filesInFolder)
            {
                if (filePath.EndsWith(".asset"))
                {
                    Object obj = AssetDatabase.LoadAssetAtPath(filePath, typeof(PhysicModule));
                    if (obj is IPhysicModule)
                    {
                        var Created = Instantiate(obj) as IPhysicModule;
                        Created.Activating(_data);
                        modules.Add(Created);
                    }
                }
            }

            _modules = modules;
        }
    }
    public abstract class PhysicModule : ScriptableObject, IPhysicModule
    {
        public abstract string ModuleName { get; }
        public virtual void Activating(ControllDataPack data)
        {

        }
        public abstract Vector2 Modification(ControllDataPack data);
    }
    public interface IPhysicModule
    {
        public void Activating(ControllDataPack data);
        public Vector2 Modification(ControllDataPack data);
    }
    public class ControllDataPack
    {
        //public float horizontalSpeed;
        public Vector2 velocityData;
        public Vector2 directionData;
        public TouchComponent groundData;
        public TouchComponent wallData;
        public GameObject body;
    }
}
