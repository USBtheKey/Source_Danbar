using UnityEngine;

[CreateAssetMenu(fileName ="NewCooldownIndicator",menuName ="Scriptable Objects/Cooldown Indicator", order = 3)]
public class CooldownIndicatorSO: ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private float cooldownTime;

    public Sprite Sprite { get => sprite;  }
    public float CooldownTime { get => cooldownTime;  }
}