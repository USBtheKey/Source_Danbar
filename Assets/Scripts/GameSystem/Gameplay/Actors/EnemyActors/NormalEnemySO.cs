#undef DO_NOT_INCLUDE

#if DO_NOT_INCLUDE

using UnityEngine;

[CreateAssetMenu(fileName ="NewNormalEnemy", menuName ="Scriptable Objects/Enemy/Normal Enemy", order = 4)]
public class NormalEnemySO : ScriptableObject
{
    [SerializeField] private GameObject projectile;

    public GameObject Projectile { get => projectile; }
}

#endif