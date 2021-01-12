
namespace GameSystem
{
    public sealed class PlayerShipsPooler: GameObjectPooler
    {
        public static PlayerShipsPooler Instance { get; private set; }

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

        protected override void CreateInstance()
        {
            if (!Instance) Instance = this;
            else Destroy(this.gameObject);
        }
    }
}