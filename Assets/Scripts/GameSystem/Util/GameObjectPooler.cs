

using System.Collections.Generic;
using UnityEngine;


namespace GameSystem
{

    public abstract class GameObjectPooler : MonoBehaviour
    {

        [System.Serializable]
        protected struct Pool
        {
            public GameObject prefab;

            [Tooltip("Leave at 0 to for default 20 instances.")]
            public int size;
        }

        [SerializeField] protected Pool[] pools;
        private const int defaultSize = 100;
        private static Transform parent;
        private static Vector3 position;
        private static Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

        public static Vector3 Position
        {
            get
            {
                return position;
            }
        }

        protected virtual void Awake()
        {
           if(position == null) position = transform.position;
           if(!parent) parent = transform;
        }

        public static GameObject Spawn(string name, Vector3 position)
        {
            GameObject objectToSpawn = poolDictionary[name].Dequeue();

            if(objectToSpawn.activeSelf)
            {
                poolDictionary[name].Enqueue(objectToSpawn); //Put the previous obj back into queue
                objectToSpawn = Instantiate(objectToSpawn, parent);
            }

            objectToSpawn.transform.position = position;
            poolDictionary[name].Enqueue(objectToSpawn);
            return objectToSpawn;
        }

        public static GameObject Spawn(string name, Vector3 position, Quaternion rotation)
        {

            GameObject objectToSpawn = poolDictionary[name].Dequeue();

            if(objectToSpawn.activeSelf)
            {
                poolDictionary[name].Enqueue(objectToSpawn); //Put the previous obj back into queue
                objectToSpawn = Instantiate(objectToSpawn, parent);
            }

            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            poolDictionary[name].Enqueue(objectToSpawn);
            return objectToSpawn;
        }

        protected void InstantiateGameObjects(Pool[] pool)
        {
            foreach (Pool p in pool)
            {
                //Checks if dupe keys
                List<string> keyList = new List<string>(poolDictionary.Keys);
                foreach (string s in keyList) if (s == p.prefab.name) continue;

                Queue<GameObject> objectPool = new Queue<GameObject>();

                var spawnSize = p.size == 0 ? defaultSize : p.size;

                for (int i = 0; i < spawnSize; i++)
                {
                    GameObject obj = Instantiate(p.prefab, parent);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(p.prefab.name, objectPool);
            }
        }

        protected abstract void CreateInstance();

        protected abstract void OnDestroy();
    }
}