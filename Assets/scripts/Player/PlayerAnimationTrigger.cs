using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player=>GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.AttackRadius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats enemyStats=hit.GetComponent<Enemy>().GetComponent<EnemyStats>();
                player.stats.DoDamege(enemyStats);
            }
        }
    }

    private void launchSword()
    {
        SkillManager.Instance.sword.CreateSword();
    }
}
