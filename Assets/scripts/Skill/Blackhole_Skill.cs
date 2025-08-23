using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill : Skill
{
    [SerializeField] private GameObject Blackhole_Prefab;
    [SerializeField] private float MaxSize;
    [SerializeField] private float GrowSpeed;
    [SerializeField] private float ShrinkSpeed;
    [SerializeField] private int AmountOfAttacks;
    [SerializeField] private float AttackCooldown;
    [SerializeField] private float BlackholeDuration;

    private Blackhole_Controller CurrentBlackhole_Contro;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void UseSkill()
    {
        base.UseSkill();

        GameObject Blackhole = Instantiate(Blackhole_Prefab, player.transform.position,Quaternion.identity);
        
        CurrentBlackhole_Contro=Blackhole.GetComponent<Blackhole_Controller>();
        CurrentBlackhole_Contro.SetupBlackhole(MaxSize, GrowSpeed, ShrinkSpeed, AmountOfAttacks, AttackCooldown,BlackholeDuration);

    }
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }
    public bool BlackholeFinished()
    {
        if (!CurrentBlackhole_Contro)
            return false;
        if(CurrentBlackhole_Contro.isExitBlackhole)
        {
            CurrentBlackhole_Contro = null;
            return true;
        }
        return false;
    }

}
