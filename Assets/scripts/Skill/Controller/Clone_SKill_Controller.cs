using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_SKill_Controler : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private Transform AttackCheck;
    [SerializeField] private float AttackRadius;
    private Transform ClosestEnemyTransform;
    [SerializeField] private float CloneFadeSpeed;
    private float CloneTime;
    public void SetTransform(Transform _newtransform,float CloneDuration,bool _CanAttack,Vector3 offset)
    {
        if(_CanAttack)
        {
            anim.SetInteger("ComboNumber", Random.Range(1,4));
        }
        transform.position = _newtransform.position + offset;
        CloneTime = CloneDuration;


        FaceClosestEnemy();
    }
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        CloneTime -= Time.deltaTime;
        if (CloneTime < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * CloneFadeSpeed));
            if(sr.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void AnimationTrigger()
    {
        CloneTime = -0.1f;
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(AttackCheck.position, AttackRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();
                PlayerManager.Instance.player.stats.DoDamege(enemyStats);
            }
        }
    }
    private void FaceClosestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float closestDistacne = Mathf.Infinity;
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if(distance <closestDistacne)
                {
                    closestDistacne = distance;
                    ClosestEnemyTransform = hit.transform;
                }
            }
        }
        if(ClosestEnemyTransform!=null)
        {
            if(transform.position.x > ClosestEnemyTransform.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
