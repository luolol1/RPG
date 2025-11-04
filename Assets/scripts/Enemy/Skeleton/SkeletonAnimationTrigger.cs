using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Enemy_Skeleton enemy =>GetComponentInParent<Enemy_Skeleton>();
    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.AttackCheck.position, enemy.AttackRadius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats playerStats=hit.GetComponent<Player>().GetComponent<PlayerStats>();
                enemy.stats.DoDamege(playerStats);
            }
        }
    }
    private void OpenCounterAttackWindow() =>enemy.OpenCounterAttackWindow();
    private void CloseCounterAttackWindow()=>enemy.CloseCounterAttackWindow();
}
