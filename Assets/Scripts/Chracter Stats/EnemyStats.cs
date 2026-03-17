using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy => GetComponent<Enemy>();

    [Header("Level details")]
    [SerializeField] private int level=1;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier;
    protected override void Start()
    {
        ApplyLevelModifiers();
        base.Start();
    }

    private void ApplyLevelModifiers()
    {
        Modify(damage);
        Modify(armor);
        Modify(MagicResistence);

        Modify(maxHealth);

        Modify(FireDamage);
        Modify(IceDamage);
        Modify(LightningDamage);
    }

    public void Modify(Stats _stats)
    {
        for(int i=1;i<level;i++)
        {
            float modifier = _stats.GetValue() * percentageModifier;
            _stats.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
