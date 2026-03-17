using UnityEngine;

public class Crystal_Controller : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private CircleCollider2D cd=>GetComponent<CircleCollider2D>();
    private float ExitTime; // 水晶存在时间

    private bool CanExplode;
    private bool CanMoveToEnemy;//自动跟踪到最近敌人；
    private Transform ClosestEnemyTransform;
    private float MoveSpeed;

    private void Update()
    {
        ExitTime -= Time.deltaTime;

        if(CanMoveToEnemy)
        {
            if (ClosestEnemyTransform == null)
                return;
            transform.position=Vector2.MoveTowards(transform.position, ClosestEnemyTransform.position, MoveSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position,ClosestEnemyTransform.position) < 0.5f)
            {
                CanMoveToEnemy=false; // 当水晶到达最近敌人位置时，停止移动
                CrystalFinished();
            }
        }
        if (ExitTime < 0)
        {
            CrystalFinished();
        }
    }

    public void CrystalFinished()
    {
        if (CanExplode)
        {
            anim.SetBool("Explode", true);
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), 1 * Time.deltaTime);
        }
        else
            SetDestroy(); // 如果不能爆炸，直接销毁水晶
    }

    public void SetupCrystal(float _ExitTime, bool _CanExplode,bool _CanMoveToEnemy,float _MoveSpeed, Transform _ClosestEnemyTransform)
    {
        ExitTime = _ExitTime;
        CanExplode = _CanExplode;
        CanMoveToEnemy = _CanMoveToEnemy;
        MoveSpeed = _MoveSpeed;

        ClosestEnemyTransform = _ClosestEnemyTransform;
    }
    public void SetDestroy() => Destroy(gameObject); // 动画结束后销毁水晶

    public void ExplodeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();
                PlayerManager.Instance.player.stats.DoMagicDamage(enemyStats);
            }

        }
    }
}
