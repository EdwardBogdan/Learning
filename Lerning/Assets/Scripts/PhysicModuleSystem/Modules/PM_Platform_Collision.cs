using CustomUnityUtils.TimeUtils;
using PhysicModuleSystem;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Physic.Modules
{
    public class PM_Platform_Collision : PhysicModule
    {
        [SerializeField] float _ignoreTime;
        [SerializeField] float _rayDistance;

        private readonly List<Collider2D> colliders = new();
        private readonly List<Cooldown> cooldowns = new();

        private Collider2D body;

        private PhysicModuleController _controller;

        public override string ModuleName => "Platform colision";

        public override void Activating(PhysicModuleController controller)
        {
            _controller = controller;
            body = _controller.RigidBody.GetComponent<Collider2D>();
        }

        public override void Modification()
        {
            if (_controller.Direction.y < 0)
            {
                Vector2 raycastOrigin = _controller.RigidBody.transform.position;

                // Выпускаем луч вниз
                RaycastHit2D[] hits = Physics2D.RaycastAll(raycastOrigin, Vector2.down, _rayDistance);

                // Обрабатываем результаты луча
                foreach (RaycastHit2D hit in hits)
                {
                    Collider2D collider = hit.collider;
                    if (collider.CompareTag("Platform") && !colliders.Contains(collider))
                    {
                        colliders.Add(collider);
                        Physics2D.IgnoreCollision(collider, body);

                        Cooldown cd = new(_ignoreTime);
                        cooldowns.Add(cd);
                        cd.Reset();
                    }
                }
            }

            for (int x = 0; x < colliders.Count; x++)
            {
                RemoveCollision(ref x, body);
            }
        }

        private void RemoveCollision(ref int index, Collider2D body)
        {
            Cooldown cd = cooldowns[index];

            if (!cd.IsReady) return;

            Collider2D collider = colliders[index];

            if(collider != null) Physics2D.IgnoreCollision(collider, body, false);

            colliders.RemoveAt(index);
            cooldowns.RemoveAt(index);

            index--;
        }
    }
}