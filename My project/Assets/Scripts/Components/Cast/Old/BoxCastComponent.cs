using UnityEngine;
using MyProject.Components.Settings;
using MyProject.Utils;
using UnityEditor;

namespace MyProject.Components
{
    public class BoxCastComponent : MonoBehaviour
    {
        [SerializeField] bool UpdateCast;
        [SerializeField] BoxCastSettings[] _settings;
        Vector2 _currentPosition => transform.position;

        private void Update()
        {
            if (!UpdateCast)
                return;

            Cast();
        }
        void Cast()
        {
            if (_settings == null)
                return;

            foreach (BoxCastSettings cast in _settings)
            {
                foreach (LayerSettings layer in cast._layers)
                {
                    if (!layer._cast)
                        continue;

                    Collider2D hit = Physics2D.OverlapBox(_currentPosition + cast._CheckPoint, cast._CheckSize, 0f, layer._checkLayer);
                    LayerHit(layer, hit);
                }
            }
        }
        public void HandleCast(string castName, string layerName)
        {
            foreach (BoxCastSettings cast in _settings)
            {
                if (!(cast._name == castName))
                    continue;
                foreach (LayerSettings layer in cast._layers)
                {
                    if (!(layer._name == layerName))
                        continue;

                    Collider2D hit = Physics2D.OverlapBox(_currentPosition + cast._CheckPoint, cast._CheckSize, 0f, layer._checkLayer);
                    LayerHit(layer, hit);
                }
            }
        }
        void LayerHit(LayerSettings layer, Collider2D hit)
        {
            GameObject newObject = hit ? hit.gameObject : null;

            if (layer._touchAction)
            {
                if (newObject != null)
                {
                    if (layer._oneTime && layer._touchedObject != newObject)
                    {
                        layer._action?.Invoke(newObject);
                    }
                    else if (!layer._oneTime)
                    {
                        layer._action?.Invoke(newObject);
                    }
                }
            }
            else
            {
                layer._action?.Invoke(newObject);
            }

            layer._touchedObject = newObject;
        }
        public void SetCast(string NameCast, string NameLayer, bool _value)
        {
            foreach (BoxCastSettings cast in _settings)
            {
                if (cast._name == NameCast)
                {
                    foreach (LayerSettings layer in cast._layers)
                    {
                        if (layer._name == NameLayer)
                        {
                            layer.SetCast(_value);
                        }
                    }
                }
            }
        }
        void OnDrawGizmosSelected()
        {
            if (_settings != null)
            {
                foreach (BoxCastSettings cast in _settings)
                {
                    foreach (LayerSettings layer in cast._layers)
                    {
                        if (layer._showGizmos)
                        {
                            Handles.color = layer._touchedObject != null ? HandlesUtils.TransporentColorGreen : HandlesUtils.TransporentColorRed;
                            Handles.DrawWireCube(_currentPosition + cast._CheckPoint, cast._CheckSize);
                        }
                    }
                }
            }
        }
    }
}