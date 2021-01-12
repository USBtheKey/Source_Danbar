namespace GameSystem
{
    public interface ISceneObject
    {
        /// <summary>
        /// Despawn object when start loading another scene.
        /// </summary>
        /// <remarks>Needs to subscribe in void Start() to GameManager.OnStartLoadingNextScene.</remarks>
        void DisableObj();
    }
}