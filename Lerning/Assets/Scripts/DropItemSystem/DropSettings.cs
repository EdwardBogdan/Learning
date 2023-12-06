using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DropItemSystem
{
    public class DropSettings : MonoBehaviour
    {
        [SerializeField] private float _dropInterval;
        [SerializeField] private float _height;
        [Range(-1f, 0), SerializeField] private float _leftEdge;
        [Range(0, 1f), SerializeField] private float _rightEdge;

        [SerializeField] private Drop[] _dropList;

        public void StartDrop()
        {
            GameObject obj = Instantiate(gameObject);

            obj.transform.position = transform.parent.transform.position;

            DropSettings droper = obj.GetComponent<DropSettings>();

            droper.StartCoroutine(droper.DropProcess());
        }
        private IEnumerator DropProcess()
        {
            List<Rigidbody2D> list = new();

            foreach (var item in _dropList)
            {
                int count = item._count;

                if (item._chanceToEach)
                {
                    for (int x = 0; x < count; x++)
                    {
                        int randomValue = (int)(UnityEngine.Random.value * 100);

                        if (randomValue <= item._chance)
                        {
                            list.Add(item._drop);
                        }
                    }
                }
                else
                {
                    int randomValue = (int)(UnityEngine.Random.value * 100);

                    if (randomValue <= item._chance)
                    {
                        for (int x = 0; x < count; x++)
                        {
                            list.Add(item._drop);
                        }
                    }
                }
            }

            Vector3 pos = transform.position;

            foreach (var drop in list)
            {
                var item = Instantiate(drop.gameObject);

                item.transform.position = pos;

                float xDirection = UnityEngine.Random.Range(_leftEdge, _rightEdge);
                Vector2 direction = new(xDirection, _height);

                item.GetComponent<Rigidbody2D>().velocity = direction * 10;

                yield return new WaitForSeconds(_dropInterval);
            }

            Destroy(gameObject);
        }

        [Serializable]
        private class Drop
        {
            public Rigidbody2D _drop;
            public int _count;
            [Range(0, 100)] public int _chance;
            public bool _chanceToEach;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Vector3 point1 = transform.position;
            Vector3 foo = new(_leftEdge, _height, 0f);
            Vector3 point2 = point1 + foo;
            foo = new(_rightEdge, _height, 0f);
            Vector3 point3 = point1 + foo;

            Gizmos.DrawLine(point1, point2);
            Gizmos.DrawLine(point2, point3);
            Gizmos.DrawLine(point3, point1);
        }
    }
}
