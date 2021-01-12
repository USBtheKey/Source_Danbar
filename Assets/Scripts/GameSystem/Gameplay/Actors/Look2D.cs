
using GameSystem.Actors;
using UnityEngine;

namespace GameSystem
{
    public class Look2D : MonoBehaviour
    {
        private Transform player;
        private EnemyLookAt lookAt = EnemyLookAt.Destination;

        private void OnEnable()
        {
            if (LevelManager.Instance == null) return;

            if (lookAt == EnemyLookAt.Player)
            {
                player = LevelManager.Instance.GetPlayer().transform;
            }

            LookTowards();
        }

        private void Update()
        {
            TrackPlayer();
        }

        public EnemyLookAt LookAt
        {
            set
            {
                this.lookAt = value;
            }
        }

        /// <summary>
        /// Looks at a direction in world space.
        /// </summary>
        public void At(Vector3 direction) => transform.up = direction.normalized;

        /// <summary>
        /// Tacks an point in world space.
        /// </summary>
        /// <param name="point">Look at coordinates.</param>
        public void Track(Vector3 point) => this.transform.up = point - transform.position;

        private void TrackPlayer()
        {
            if (lookAt == EnemyLookAt.Player) Track(player.position);
        }

        private void LookTowards()
        {
            switch (lookAt)
            {
                case EnemyLookAt.None:
                    break;

                case EnemyLookAt.Down:
                    At(Vector2.down);
                    break;

                case EnemyLookAt.Up:
                    At(Vector2.up);
                    break;

                case EnemyLookAt.Right:
                    At(Vector2.right);
                    break;

                case EnemyLookAt.Left:
                    At(Vector2.left);
                    break;
            }

        }

        public void LookTowards(Vector3 position)
        {
            if (lookAt == EnemyLookAt.Destination) At(position - transform.position);
        }
    }
}