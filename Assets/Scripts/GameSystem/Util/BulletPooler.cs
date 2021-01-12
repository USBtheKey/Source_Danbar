
namespace GameSystem
{
    public sealed class BulletPooler : GameObjectPooler
    {
        public static BulletPooler Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CreateInstance();
            InstantiateGameObjects(pools);
            pools = null;
        }

        protected override void OnDestroy()
        {
            Instance = null;
        }

        public static BulletPooler GetInstance()
        {
            return Instance;
        }

        protected override void CreateInstance()
        {
            if (!Instance) Instance = this;
            else Destroy(this.gameObject);
        }
    }
}