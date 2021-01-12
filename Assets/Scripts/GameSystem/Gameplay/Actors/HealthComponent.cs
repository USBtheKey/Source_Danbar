
using System;
using UnityEngine;

public class HealthComponent: MonoBehaviour
{
    private float maxHitpoints;
	[SerializeField] private int life = 0;
	[SerializeField] private float hitpoints = 0;
    public bool IsInvincible = false;

    private int originalLife;
    private float originalHealth;

    public float HitpointsRatio => Hitpoints / maxHitpoints;

    public int Life
    {
        get => life;
        set { life = value; OnLifeUpdate?.Invoke(life); } 
    }

    public float Hitpoints
    {
        get => hitpoints;
        set
        {
            hitpoints = value;
            OnHitpointsUpdate?.Invoke((int)hitpoints);
            OnHitpointsRatioUpdate?.Invoke(HitpointsRatio);
            if (!IsAlive) OnDeath?.Invoke();
            else if (Hitpoints <= 0 && Life >= 1)
            {
                OnLifeUpdate?.Invoke(--Life);
                Hitpoints = maxHitpoints;
            }
        }
    }

    private void Awake()
    {
        originalHealth = Hitpoints;
        originalLife = Life;
    }

    private void OnEnable()
    {
        maxHitpoints = Hitpoints;

        Life = originalLife;
        Hitpoints = originalHealth;

        OnHitpointsUpdate?.Invoke((int)hitpoints);
        OnLifeUpdate?.Invoke(Life);
    }

    private void OnDisable()
    {
        OnDeath = null;
    }

    public bool IsAlive => (Life > 0 || Hitpoints > 0);

    public void TakeLifeDamage(int damage)
    {
        Life -= damage;
    }

    public void TakeDamage(float damage)
    {
        if (IsInvincible) return;
        Hitpoints -= damage;
    }

    public void Suicide()
    {
        Life = 0;
        Hitpoints = 0;
    }

    public event Action<int> OnHitpointsUpdate;
    public event Action<float> OnHitpointsRatioUpdate;
    public event Action<int> OnLifeUpdate;
    public event Action OnDeath;
}