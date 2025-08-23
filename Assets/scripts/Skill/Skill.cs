using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float Cooldown;
    protected float CooldownTime;
    public Player player { get; private set; }
    protected virtual void Start()
    {
        player = PlayerManager.Instance.player;
    }
    protected virtual void Update()
    {
        CooldownTime -= Time.deltaTime;
    }


    public virtual bool CanUseSkill()
    {
        if(CooldownTime <0)
        {
            UseSkill();
            CooldownTime = Cooldown;
            return true;
        }
        else
            Debug.Log("Skill on Cooldown");
        return false;
    }


    protected virtual void UseSkill()
    {
        
    }

    public virtual Transform FindClosestEnemy(Transform _transform)
    {
        Transform ClosestEnemyTransform = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 25);
        float closestDistacne = Mathf.Infinity;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(_transform.position, hit.transform.position);
                if (distance < closestDistacne)
                {
                    closestDistacne = distance;
                    ClosestEnemyTransform = hit.transform;
                }
            }
        }
        return ClosestEnemyTransform;
    }
}
