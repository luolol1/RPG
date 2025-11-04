using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockStrike_Controller : MonoBehaviour
{
    [SerializeField] private CharacterStats target;
    private int damage;
    [SerializeField] private float speed;
    private Animator anim;

    private bool Trigger;

    private void Start()
    {
        anim=GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Trigger)
            return;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.right = (target.transform.position - transform.position);
        if (Vector2.Distance(transform.position,target.transform.position) < 0.1f)
        {
            anim.transform.localPosition = new Vector3(0, 0.5f);
            anim.transform.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3);

            Trigger = true;
            anim.SetTrigger("Hit");
                 
            Invoke("DamageAndSelfDestroy", 0.2f);
        }
    }
    private void DamageAndSelfDestroy()
    {
        target.ApplyShock(true); //ÉÁµç´¥·¢shock×´Ì¬
        target.TakeDamage(damage);
        Destroy(gameObject,.4f);

    }
    public void SetUp(CharacterStats _target,int _damage)
    {
        target = _target;
        damage = _damage;
    }
}
