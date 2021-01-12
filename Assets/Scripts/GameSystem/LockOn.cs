using UnityEngine;
using System.Collections.Generic;

namespace GameSystem
{
    public class LockOn : MonoBehaviour, ISceneObject //These get attached on scene
    {
        private static readonly List<LockOn> instances = new List<LockOn>();

        private LockOn_VFX vfx;
        public Transform Target { private set; get; }
        //private AudioSource audio;
        private Player player;


        //private void Awake()
        //{
        //    audio = GetComponent<AudioSource>();
        //}

        private void OnEnable()
        {
            
            if (!LevelManager.Instance) return;
            LevelManager.Instance.OnGameOver += DisableObj;
            LevelManager.Instance.OnGameWon += DisableObj;

            player = LevelManager.Instance.GetPlayer();
            player.OnDestroyed += DisableObj;
            player.OnFireMissile += DisableObj;
            player.OnDestroyed += DisableObj;
            player.Health.OnDeath += DisableObj;
            instances.Add(this);

            Target = null;
            var lockOn = GameObjectPooler.Spawn("Player_Lock_On_VFX", transform.position);
            lockOn.SetActive(true);
            vfx = lockOn.GetComponent<LockOn_VFX>();
            vfx.OnTargetLocked += SetThisAsTarget;
            OnInvisible += vfx.DisableObj;
        }

        private void FixedUpdate()
        {
            if (!LevelManager.Instance) return;
            vfx.transform.position = transform.position;
        }

        private void OnDisable()
        {
            if (!LevelManager.Instance) return;

            OnInvisible?.Invoke();

            vfx.OnTargetLocked -= SetThisAsTarget;
            player.OnFireMissile -= DisableObj;
            player.OnDestroyed -= DisableObj;
            player.Health.OnDeath -= DisableObj;

            vfx = null;

            instances.Remove(this);
        }

        private void SetThisAsTarget()
        {
            Target = transform;
        }

        public static int InstancesCount
        {
            get
            {
                if (instances == null) return 0;
                return instances.Count;
            }
        }

        public static LockOn[] GetLockedInstances
        {
            get
            {
                var list = new List<LockOn>();

                foreach (var locked in FindLockedTarget(instances.ToArray()))
                {
                    list.Add(locked);
                }

                return list.ToArray();
            }
        }

        public static IEnumerable<LockOn> FindLockedTarget(LockOn[] targets)
        {
            foreach (var obj in targets)
            {
                if (obj.Target) yield return obj;
            }
        }

        public void DisableObj()
        {
            StopAllCoroutines();

            transform.parent = GameManager.Instance.transform;
            gameObject.SetActive(false);
        }

        public event System.Action OnInvisible;
    }
}

