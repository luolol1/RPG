using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Manager : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool CanRotate = true;
    private  bool isReturning = false;
    private float returnSpeed ;
    private float FreezeTime;

    [Header("Spin info")]
    private bool isSpining;
    private int MaxDistance;
    private float SpinDuration;
    private float SpinTime;
    private bool wasStopped =false;

    private float hitTime;
    private float hitCooldown;



    [Header("Pierce info")]
    private int NumberOfPierce;


    [Header("Bounce info")]
    private bool isBouncing ;
    private int NumbersOfbounces ; // 弹跳次数
    private float BounceSpeed;
    private List<Transform> enemies;
    private int TargetIndex=0;

    private void Awake()
    {
        anim= GetComponentInChildren<Animator>();
        rb= GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (CanRotate)
            transform.right = rb.velocity;
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchSword();
        }
        BounceLogic();
        SpinLogic();
    }

    private void SpinLogic()
    {
        if (isSpining)
        {
            if (Vector2.Distance(player.transform.position, transform.position) >= MaxDistance && !wasStopped)
            {
                wasStopped = true;
                SpinTime = SpinDuration;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            if (wasStopped)
            {
                SpinTime -= Time.deltaTime;
                if (SpinTime < 0)
                {
                    isSpining = false;
                    isReturning = true;
                }
                hitTime -= Time.deltaTime;
                if (hitTime < 0)
                {
                    hitTime = hitCooldown;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                        {
                            EnemyStats enemyStats = hit.GetComponent<EnemyStats>();
                            player.stats.DoDamege(enemyStats);
                        }
                    }
                }
            }
        }
    }

    private void BounceLogic()
    {
        if (isBouncing && enemies.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemies[TargetIndex].position, BounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemies[TargetIndex].position) < 0.1f)
            {
                SwordDamage(enemies[TargetIndex].GetComponent<Enemy>());
                TargetIndex++;
                NumbersOfbounces--;
            }
            if (NumbersOfbounces <= 0)
            {
                isBouncing = false;
                isReturning = true;
            }

            if (TargetIndex >= enemies.Count)
            {
                TargetIndex = 0;
            }
        }
    }

    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning =true;
    }
    public void SetupSword(Vector2 _velocity,float Gravity_Scale,float _returnSpeed,float _FreezeTime)
    {
        player = PlayerManager.Instance.player;
        rb.velocity = _velocity;
        rb.gravityScale = Gravity_Scale;
        returnSpeed = _returnSpeed;
        FreezeTime = _FreezeTime;

        if (NumberOfPierce <=0)
            anim.SetBool("Flip", true);
    }

    public void SetupBounce(bool _isBouncing,int _NumberOfBounces,float _BounceSpeed)
    {
        isBouncing = _isBouncing;
        NumbersOfbounces = _NumberOfBounces;
        BounceSpeed = _BounceSpeed;
        enemies = new List<Transform>();
    }
    
    public void SetupPierce(int _NumberOfPierce)
    {
        NumberOfPierce = _NumberOfPierce;
    }

    public void SetupSpin(bool _isSpining,int _MaxDistance,float _SpinDuaration,float _hitCooldown)
    {
        isSpining = _isSpining;
        MaxDistance = _MaxDistance;
        SpinDuration = _SpinDuaration;
        hitCooldown = _hitCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;

        if (collision.GetComponent<Enemy>() != null)
        {
            if(isBouncing)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15f);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemies.Add(hit.transform);
                    }
                }
                enemies.Sort((a, b) =>
                Vector2.Distance(transform.position, a.position)
                .CompareTo(Vector2.Distance(transform.position, b.position)));//按照距离从小到大排序
            }
        }
        if(NumberOfPierce > 0 && collision.GetComponent<Enemy>() !=null)
        {
            NumberOfPierce--;
            SwordDamage(collision.GetComponent<Enemy>());
            return;
        }
        if (isSpining)
        {
            wasStopped = true;
            SpinTime = SpinDuration;
            rb.constraints=RigidbodyConstraints2D.FreezeAll;
            return;
        }
        StuckInto(collision);

    }

    private void SwordDamage(Enemy enemy)
    {
        player.stats.DoDamege(enemy.GetComponent<EnemyStats>());
        enemy.StartCoroutine("FreezeTimefor", FreezeTime);
    }

    private void StuckInto(Collider2D collision)
    {
        CanRotate = false;
        rb.isKinematic = true;
        cd.enabled = false;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemies.Count > 0)
            return;
        transform.parent = collision.transform;
        anim.SetBool("Flip", false);
    }
}
