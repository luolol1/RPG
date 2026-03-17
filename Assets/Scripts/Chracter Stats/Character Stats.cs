using System.Xml.Serialization;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major Stats")]
    public Stats strength; //力量，影响物理伤害、暴击伤害
    public Stats agility; //敏捷，影响闪避、暴击率
    public Stats intelligence; //智力，影响法术伤害
    public Stats vitality; //体力，影响生命值

    [Header("Defensive Stats")]
    public Stats maxHealth;
    public Stats armor; //护甲，减少物理伤害
    public Stats MagicResistence; //魔抗，减少法术伤害
    public Stats evasion; //闪避，减少被命中概率

    [Header("Offensive Stats")]
    public Stats damage;
    public Stats CriticalChance;//暴击率
    public Stats CriticalPower; //暴击增伤150%

    [Header("Magic Stats")]
    public Stats FireDamage;
    public Stats IceDamage;
    public Stats LightningDamage;

    public bool isIgnited; // 火伤，持续造成伤害
    public bool isChilled; // 冰冻，削弱护甲20%
    public bool isShocked; // 如果敌人被电击，命中率降低20%

    [SerializeField] private GameObject ShockStrikePrefab;

    private float AilmentDuration = 3f;
    private float IgnitedTimer;
    private float ChilledTimer;
    private float ShockedTimer;


    private float IgnitedDamageTimer;
    private float IgnitedDamageWindow = 0.3f;//每0.3秒造成一次伤害
    private int IgnitedDamage;
    private int ShockStrikeDamage;


    public int currentHealth;

    private bool IsDead=false;

    public System.Action OnHealthChange;
    private EntityFX FX;
    protected virtual void Start()
    {
        CriticalPower.SetValue(150);
        currentHealth = GetTotalHealth();
        FX = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        IgnitedTimer -= Time.deltaTime;
        ChilledTimer -= Time.deltaTime;
        ShockedTimer -= Time.deltaTime;

        IgnitedDamageTimer -= Time.deltaTime;
        if (IgnitedTimer < 0)
            isIgnited = false;

        if (ChilledTimer < 0)
            isChilled = false;
        if (ShockedTimer < 0)
            isShocked = false;

        if (isIgnited && IgnitedDamageTimer < 0)
        {
            DecreaseHealth(IgnitedDamage);
            if (currentHealth < 0 && !IsDead)
                Die();
            IgnitedDamageTimer = IgnitedDamageWindow;
        }
    }
    public virtual void DoDamege(CharacterStats _target)
    {
        if (CanAvoidDamage(_target))
            return;


        int totalDamage = damage.GetValue() + strength.GetValue();
        if (CanCriticalHit())
        {
            totalDamage = GetCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_target, totalDamage);
        //_target.TakeDamage(totalDamage);
        DoMagicDamage(_target);
    }

    public virtual void DoMagicDamage(CharacterStats _target)
    {
        int _fireDamage = FireDamage.GetValue();
        int _iceDamage = IceDamage.GetValue();
        int _lightningDamage = LightningDamage.GetValue();

        int totalMagicDamage = _fireDamage + _iceDamage + _lightningDamage + intelligence.GetValue();

        totalMagicDamage = CheckTargetResistance(_target, totalMagicDamage);
        _target.TakeDamage(totalMagicDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightningDamage) <= 0)//如果都为0，后面的循环也会死循环
            return;
        //取最大伤害的buff，施加负面效果
        bool CanbeIgnited = _fireDamage > _iceDamage && _fireDamage > _lightningDamage;
        bool CanbeChilled = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
        bool CanbeShocked = _lightningDamage > _fireDamage && _lightningDamage > _iceDamage;

        //如果三种伤害相等，则随机一个
        while (!CanbeIgnited && !CanbeChilled && !CanbeShocked)
        {
            if (Random.value < 0.33f && _fireDamage > 0)
            {
                CanbeIgnited = true;
                _target.SetIgnitedDamage(Mathf.RoundToInt(_fireDamage * 0.2f));
                _target.ApplyAilment(CanbeIgnited, CanbeChilled, CanbeShocked);
                Debug.Log("Ignite");
                return;
            }
            if (Random.value < 0.5f && _iceDamage > 0)
            {
                CanbeChilled = true;
                _target.ApplyAilment(CanbeIgnited, CanbeChilled, CanbeShocked);
                Debug.Log("Chill");
                return;
            }
            if (Random.value < 0.5f && _lightningDamage > 0)
            {
                CanbeShocked = true;
                _target.ApplyAilment(CanbeIgnited, CanbeChilled, CanbeShocked);
                Debug.Log("Shock");
                return;
            }
        }
        _target.SetIgnitedDamage(Mathf.RoundToInt(_fireDamage * 0.2f));
        _target.SetShockStrikeDamage(Mathf.RoundToInt(_lightningDamage * 0.1f));
        _target.ApplyAilment(CanbeIgnited, CanbeChilled, CanbeShocked);
    }


    public void SetIgnitedDamage(int _damage) => IgnitedDamage = _damage; 
    public void SetShockStrikeDamage(int _damage) => ShockStrikeDamage = _damage;
    public virtual void ApplyAilment(bool _isIgnited, bool _isChilled, bool _isShocked)
    {
        bool CanApplyIgnited = !isIgnited && !isChilled && !isShocked;
        bool CanApplyChilled = !isIgnited && !isChilled && !isShocked;
        bool CanApplyShocked = !isIgnited && !isChilled;
        if (_isIgnited  && CanApplyIgnited)
        {
            isIgnited = _isIgnited;
            IgnitedTimer = AilmentDuration;
            FX.IgnitedFXfor(AilmentDuration);
        }
        if (_isChilled && CanApplyChilled)
        {
            isChilled = _isChilled;
            ChilledTimer = 2;
            FX.ChilledFXfor(AilmentDuration);
            float slowPercent = 0.2f;//冰冻减速百分比
            GetComponent<Entity>().SlowEntityBy(slowPercent, AilmentDuration);
        }
        if (_isShocked && CanApplyShocked)
        {
            if (!isShocked)
            {
                ApplyShock(_isShocked);
            }
            else
            {
                if (GetComponent<Player>() != null)
                    return;
                HitTargetWithShockStrike();//召唤闪电打最近的敌人

            }
        }
    }

    public void ApplyShock(bool _isShocked)
    {
        if(isShocked)
            return;
        isShocked = _isShocked;
        ShockedTimer = 2;
        FX.ShockedFXfor(AilmentDuration);
    }

    private void HitTargetWithShockStrike()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float closestDistacne = Mathf.Infinity;
        Transform ClosestEnemyTransform = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistacne)
                {
                    closestDistacne = distance;
                    ClosestEnemyTransform = hit.transform;
                }
            }
        }
        if (ClosestEnemyTransform != null)
        {
            GameObject Thunder = Instantiate(ShockStrikePrefab, transform.position, Quaternion.identity);
            Thunder.GetComponent<ShockStrike_Controller>().SetUp(ClosestEnemyTransform.GetComponent<CharacterStats>(), ShockStrikeDamage);
        }
    }

    public virtual void TakeDamage(int _damage)//重载了TakeDamage方法，有动画
    {
        //Debug.Log(_damage);
        DecreaseHealth(_damage);
        GetComponent<Entity>().DamageImpact();

        FX.StartCoroutine("FlashFX");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    public virtual void DecreaseHealth(int _damage)//直接减少生命值，没有动画
    {
        currentHealth -= _damage;
        OnHealthChange ?.Invoke();
    }
    private int CheckTargetArmor(CharacterStats _target, int totalDamage)
    {
        if (_target.isChilled)
            totalDamage -= Mathf.RoundToInt(_target.armor.GetValue() * 0.8f);
        else
            totalDamage -= _target.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }
    private int CheckTargetResistance(CharacterStats _target, int totalMagicDamage)
    {
        totalMagicDamage -= _target.MagicResistence.GetValue();
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }
    protected bool CanAvoidDamage(CharacterStats _target)
    {
        int totalEvasion = _target.agility.GetValue() + _target.evasion.GetValue();
        if (isShocked)
            totalEvasion += 20;
        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }
    protected bool CanCriticalHit()
    {
        int totalChance = agility.GetValue() + CriticalChance.GetValue();
        if (Random.Range(0, 100) < totalChance)
        {
            return true;
        }
        return false;
    }
    protected int GetCriticalDamage(int _Damage)
    {
        float totalCriticalPower = (CriticalPower.GetValue() + strength.GetValue()) * 0.01f;
        float CriticalDamage = _Damage * totalCriticalPower;
        return Mathf.RoundToInt(CriticalDamage);
    }

    public int GetTotalHealth()
    {
        return maxHealth.GetValue() + vitality.GetValue()*10;
    }
    protected virtual void Die()
    {
        Debug.Log(transform.name + " died.");

    }
}
