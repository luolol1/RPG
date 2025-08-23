using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class Sword_Skill : Skill
{
    [SerializeField] private GameObject SwordPrefab;

    public SwordType swordType = SwordType.Regular;

    [Header("Spin info")]
    [SerializeField] private float SpinGravity_Scale;
    [SerializeField] private int MaxDistance;
    [SerializeField] private float SpinDuration;
    [SerializeField] private float hitCooldown;


    [Header("Pierce info")]
    [SerializeField] private int NumberOfPierce;
    [SerializeField] private float PierceGravity_Scale;


    [Header("Bounce info")]
    [SerializeField] private int NumberOfBounces;
    [SerializeField] private float BounGravity_Scale;
    [SerializeField] private float BounceSpeed;

    [Header("Sword info")]
    [SerializeField] private float gravity_Scale;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float FreezeTime;


    private Vector2 launchDirection;

    [Header("Aim dots")]
    [SerializeField] private int number_of_dots;
    [SerializeField] private float TimeBetweenDots;
    [SerializeField] private GameObject DotsPrefab;
    [SerializeField] private Transform DotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        CreateDots();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            launchDirection = new Vector2(AimDirection().x * launchForce.x, AimDirection().y * launchForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = SetDotPosition(i * TimeBetweenDots);
            }
        }
    }
    public void CreateSword()
    {
        GameObject sword = Instantiate(SwordPrefab, player.transform.position, transform.rotation);

        switch (swordType)
        {
            case SwordType.Bounce:
                gravity_Scale = BounGravity_Scale;
                sword.GetComponent<Sword_Skill_Manager>().SetupBounce(true, NumberOfBounces,BounceSpeed);
                break;
            case SwordType.Pierce:
                gravity_Scale = PierceGravity_Scale;
                sword.GetComponent<Sword_Skill_Manager>().SetupPierce(NumberOfPierce);
                break;
            case SwordType.Spin:
                gravity_Scale = SpinGravity_Scale;
                sword.GetComponent<Sword_Skill_Manager>().SetupSpin(true, MaxDistance, SpinDuration,hitCooldown);
                break;
        }
        sword.GetComponent<Sword_Skill_Manager>().SetupSword(launchDirection, gravity_Scale,returnSpeed,FreezeTime);
        player.AssignSword(sword);
        SetUpActive(false);
    }

    #region Aim
    private Vector2 AimDirection()
    {
        Vector2 PlayerPosition = player.transform.position;
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (MousePosition - PlayerPosition).normalized;//πÈ“ªªØ
        return direction;
    }

    public void SetUpActive(bool _active)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_active);
        }
    }

    public void CreateDots()
    {
        dots = new GameObject[number_of_dots];
        for (int i = 0; i < number_of_dots; i++)
        {
            dots[i] = Instantiate(DotsPrefab, player.transform.position, Quaternion.identity, DotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 SetDotPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().x * launchForce.x * t,
            AimDirection().y * launchForce.y * t + 0.5f * (Physics2D.gravity.y * gravity_Scale) * t * t);
        return position;
    }
    #endregion
}
