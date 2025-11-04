using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player => GetComponent<Player>();
    protected override void Start()
    {
        base.Start();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        player.DamageEffect();
    }
    override protected void Die()
    {
        base.Die();
        player.Die();
    }
}
