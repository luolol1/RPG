using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private GameObject CrystalPrefab;
    private GameObject crystal;

    [SerializeField] private float ExitTime;
    [Header("Explore info")]
    [SerializeField] private bool CanExplode;

    [Header("Move to Player")]
    [SerializeField] private bool CanMoveToEnemy;
    [SerializeField] private float MoveSpeed;

    [Header("Multiple crystal")]
    [SerializeField] private bool CanCreateMultipleCrystal;//是否可以创建多个水晶
    [SerializeField] private int CrystalCount;
    [SerializeField] private float MultipleCrystalCooldown;
    [SerializeField] private float CrystalWindow;
    [SerializeField] private List<GameObject> CrystalLeft=new List<GameObject>();

    protected override void Start()
    {
        base.Start();

    }
    protected override void UseSkill()
    {
        base.UseSkill();
        if (CanCreateMultipleCrystal)
        {
            MultipleCrystalLogic();
            return;
        }
        if (crystal == null)
        {
            crystal = Instantiate(CrystalPrefab, player.transform.position, Quaternion.identity);
            Crystal_Controller CurrentCrystalScript = crystal.GetComponent<Crystal_Controller>();
            CurrentCrystalScript.SetupCrystal(ExitTime, CanExplode, CanMoveToEnemy, MoveSpeed, FindClosestEnemy(crystal.transform));
        }
        else
        {
            //如果水晶已经存在，则将玩家传送到水晶位置。二者交换位置
            Vector2 playerPosition = player.transform.position;
            player.transform.position = crystal.transform.position;
            crystal.transform.position = playerPosition;

            crystal.GetComponent<Crystal_Controller>().CrystalFinished();
        }
    }
    private void MultipleCrystalLogic()
    {
        if (CrystalLeft.Count > 0)
        {
            Cooldown = 0;//如果不设置为0，则会导致无法创建多个水晶
            if(CrystalLeft.Count==CrystalCount)
            {
                Invoke("ResetCrystal", CrystalWindow);
            }
            GameObject chosenCrystal = CrystalLeft[CrystalLeft.Count - 1];
            CrystalLeft.Remove(chosenCrystal);
            GameObject newCrystal = Instantiate(chosenCrystal, player.transform.position, Quaternion.identity);
            newCrystal.GetComponent<Crystal_Controller>().
                SetupCrystal(ExitTime, CanExplode, CanMoveToEnemy, MoveSpeed, FindClosestEnemy(newCrystal.transform));

            if (CrystalLeft.Count <= 0)
            {
                Cooldown = MultipleCrystalCooldown;
                SupplementCrystal();
            }
        }

    }
    private void SupplementCrystal()
    {
 
        while (CrystalLeft.Count < CrystalCount)
        {
            CrystalLeft.Add(CrystalPrefab);
        }
    }
    private void ResetCrystal()
    {
        if (Cooldown> 0)
            return;//证明是用完水晶的cd，不需要重置。
        CooldownTime = MultipleCrystalCooldown;
        SupplementCrystal();
    }
}
