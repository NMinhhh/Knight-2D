using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy4 : BossEnemy
{
    [Header("Intrinsic")]
    [SerializeField] private GameObject objPref;
    [SerializeField] private float damageSkill;
    [SerializeField] private float objTimeLife;
    [SerializeField] private int amountOfObj;
    [SerializeField] private GameObject signObj;  
    [SerializeField] private float timerSign;
    [SerializeField] private float cooldownIntrintic;
    private float timerIntrintic;
    private DamageAnimation damageAnimScript;
    private GameObject go;
    [Space]
    [Space]

    [Header("Shooting Bullet Skill")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private int amountOfBullet;
    private int currentAmountOfBullet;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeLife;
    [SerializeField] private float shootingCooldown;
    private float shootingTime;
    private float angle;
    private NormalBullet script;
    private GameObject Go;
    private bool isShootingBulletSkill;
    [Space]
    [Space]

    [Header("Dash Skill")]
    [SerializeField] private float dashSpeed;
    private Vector3 currentTarget;
    protected bool isDash;
    [SerializeField] protected float distanceToStop;
    [Space]
    [Space]

    private Transform cam;

    protected override void Start()
    {
        base.Start();
        cam = GameObject.Find("Main Camera").transform;
        SelectedSkill(0);
    }

    protected override void Update()
    {
        base.Update();
        if (isCheckSkill)
            CheckCooldownSkill(selectedSkill);
        if (isReadySkills[0])
        {
            Reload();
            isMove = false;
            isShootingBulletSkill = true;
            ResetCooldownSkill(selectedSkill);
        }else if (isReadySkills[1])
        {
            isMove = false;
            SetCurrentTarget();
            isDash = true;
            ResetCooldownSkill(selectedSkill);
        }

        if (isShootingBulletSkill)
        {
            Shooting();
        }
        else if (isDash)
        {
            Dash(dashSpeed);
        }

        //Intrintic
        timerIntrintic += Time.deltaTime;
        if(timerIntrintic >= cooldownIntrintic)
        {
            timerIntrintic = 0;
            StartCoroutine(SpawnObj());
        }
    }

    //==================================Intrintic============================================

    Vector3[] GetDestination()
    {
        Vector3[] destination = new Vector3[amountOfObj];
        int rd = Random.Range(0, amountOfObj);
        for (int i = 0; i < amountOfObj; i++)
        {

            if (i == rd)
            {
                destination[i] = target.position;
            }
            else
            {
                destination[i] = new Vector2(Random.Range(cam.position.x - 18, cam.position.y + 18),
                            Random.Range(cam.position.y - 7, cam.position.y + 7));
            }
        }
        return destination;
    }

    IEnumerator SpawnObj()
    {
        Vector3[] destination = GetDestination();
        for (int i = 0; i < destination.Length; i++)
        {
            GameObject go = Instantiate(signObj, destination[i], Quaternion.identity);
            Destroy(go, timerSign + objTimeLife);
        }
        yield return new WaitForSeconds(timerSign);
        for (int i = 0; i < amountOfObj; i++)
        {
            go = Instantiate(objPref, destination[i], Quaternion.identity);
            damageAnimScript = go.GetComponent<DamageAnimation>();
            damageAnimScript.CreateObj(damageDir, objTimeLife);
        }
    }

    //============================Shooting Skill======================================

    void Reload()
    {
        currentAmountOfBullet = amountOfBullet;
        angle = Random.Range(0, 360);
    }

    void Shooting()
    {
        if (currentAmountOfBullet > 0)
        {
            shootingTime -= Time.deltaTime;
            if (shootingTime <= 0)
            {
                shootingTime = shootingCooldown;
                SpawnBulletObj();
                currentAmountOfBullet--;
            }

        }
        else
        {
            isShootingBulletSkill = false;
            ChangeSkillRandom();
            isMove = true;
        }
    }

    void SpawnBulletObj()
    {
        angle += Random.Range(10, 12);
        Go = GameObject.Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
        script = Go.GetComponent<NormalBullet>();
        script.CreateBullet(bulletDamage, bulletSpeed, timeLife);
    }

    //======================Dash Skill=============================
    void SetCurrentTarget()
    {
        currentTarget = target.position;
    }

    void DashEffect()
    {
        GameObject obj = new GameObject();
        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = spriteRenderer.sprite;
        sr.color = new Color(1, 1, 1, 1);
        sr.flipX = true;
        sr.sortingLayerName = "Enemy";
        obj.transform.position = transform.position;
        obj.transform.localScale = transform.localScale;
        Destroy(obj, .1f);
    }

    public void CheckDistanceToStopDash(Vector3 a, Vector3 b, float distance)
    {
        if (Vector2.Distance(a, b) <= distance)
        {
            isDash = false;
            SetVelocityZero();
            isMove = true;
            ChangeSkillRandom();
        }
    }

    public void Dash(float dashSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, dashSpeed * Time.deltaTime);
        DashEffect();
        CheckDistanceToStopDash(transform.position, currentTarget, distanceToStop);
    }
}
