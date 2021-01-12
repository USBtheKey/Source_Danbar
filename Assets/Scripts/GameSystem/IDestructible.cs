

/// <summary>
/// If implement, Gameobject can be destroyed by player's ultimate.
/// </summary>
public interface IDestructible
{
    /// <summary>
    /// Called By Player Ultimate.
    /// </summary>
    void Destroy();
}