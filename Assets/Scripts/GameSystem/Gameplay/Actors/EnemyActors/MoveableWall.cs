
using UnityEngine;

namespace GameSystem.Actors
{
    public sealed class MoveableWall : BaseEnemyActor
    {
        [SerializeField] private float speed = 100f;
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }
    }
}
