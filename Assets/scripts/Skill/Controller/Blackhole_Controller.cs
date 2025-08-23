using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Controller : MonoBehaviour
{
    [SerializeField] private GameObject Blackhole_hotkey;
    [SerializeField] private List<KeyCode> KeyCodeList;
    private float MaxSize;
    private float GrowSpeed;
    private float ShrinkSpeed;

    private bool CanGrow = true;//黑洞变大
    private bool CanShrink;//黑洞缩小
    private bool CanCreateHotkey = true;

    public bool isExitBlackhole;
    private bool canAttack;//分身攻击
    private int AmountOfAttacks;
    private float AttackCooldown;
    private float AttackTimer;
    private float BlackholeDuration;
    private bool isUsed;

    [SerializeField] private List<Transform> EnemiesList = new List<Transform>();
    private List<GameObject> CreatedKey = new List<GameObject>();//存储创建的热键，为了后续销毁
    public void SetupBlackhole(float _MaxSize,float _GrowSpeed,float _ShrinkSpeed,int _AmountOfAttacks,float _AttackCooldown,float _BlackholeDuration)
    {
        MaxSize = _MaxSize;
        GrowSpeed = _GrowSpeed; 
        ShrinkSpeed = _ShrinkSpeed;
        AmountOfAttacks = _AmountOfAttacks;
        AttackCooldown = _AttackCooldown;
        BlackholeDuration= _BlackholeDuration;
    }
    private void Update()
    {
        AttackTimer -= Time.deltaTime;
        
        BlackholeDuration -= Time.deltaTime;

        //黑洞持续窗口时间
        if(BlackholeDuration<0)
        {
            BlackholeDuration = Mathf.Infinity;
            if (EnemiesList.Count > 0)
            {
                canAttack = true;
                CanCreateHotkey = false;
                PlayerManager.Instance.player.MakeTransparent(true);
            }
            else
            {
                FinishBlackholeAbility();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && EnemiesList.Count > 0 && !isUsed)
        {
            canAttack = true;
            CanCreateHotkey = false;
            isUsed = true;
            PlayerManager.Instance.player.MakeTransparent(true);
        }

        AttackLogic();

        if (CanGrow && !CanShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(MaxSize, MaxSize), GrowSpeed * Time.deltaTime);
        }
        if (CanShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), ShrinkSpeed * Time.deltaTime);

            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void AttackLogic()
    {
        if (canAttack && AttackTimer <= 0 && AmountOfAttacks >0)
        {
            AttackTimer = AttackCooldown;

            float xOffset = 0;
            if (Random.Range(0, 100) < 50)
            {
                xOffset = 2;
            }
            else
            {
                xOffset = -2;
            }

            SkillManager.Instance.clone.CreateClone(EnemiesList[Random.Range(0, EnemiesList.Count)], new Vector3(xOffset, 0));
            AmountOfAttacks--;

            if (AmountOfAttacks <= 0)
            {
                Invoke("FinishBlackholeAbility",1f);
            }
        }
    }

    private void FinishBlackholeAbility()
    {
        DestroyHotKey();
        canAttack = false;
        CanShrink = true;
        isExitBlackhole = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().FreezeTime(false);
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (KeyCodeList.Count <= 0)
            return;

        if (!CanCreateHotkey)
            return;
        GameObject newHotkey = Instantiate(Blackhole_hotkey, collision.transform.position + new Vector3(0, 1), Quaternion.identity);
        CreatedKey.Add(newHotkey);

        KeyCode newKeyCode = KeyCodeList[Random.Range(0, KeyCodeList.Count)];
        KeyCodeList.Remove(newKeyCode);
        newHotkey.GetComponent<Blackhole_hotkey_Controller>().Set_Hotkey(newKeyCode, collision.transform, this);
    }

    private void DestroyHotKey()
    {
        if (CreatedKey.Count <= 0)
            return;
        for (int i = 0; i < CreatedKey.Count; i++)
        {
            Destroy(CreatedKey[i]);
        }
    }
    public void AddEnemyList(Transform _enemy)
    {
        if (!EnemiesList.Contains(_enemy))
            EnemiesList.Add(_enemy);
    }
}
