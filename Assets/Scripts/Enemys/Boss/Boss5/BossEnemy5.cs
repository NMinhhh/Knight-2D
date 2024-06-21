using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy5 : BossEnemy
{
    [Header("Intrinsic")]
    [SerializeField] private GameObject[] bulletPrefs;
    private NormalBullet bulletScript;
    private GameObject bulletGo;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletTimeLife;
    [SerializeField] private float intrinsicCooldown;
    private float currentIntrinsicCooldown;
    private float angle;
    private bool isIntrinsic;
    [Space]

    [Header("Cave Summon Enemy Skill")]
    //Cave pref
    [SerializeField] private GameObject cavePref;
    [SerializeField] private int amountOfCave;
    //Enenmy in cave
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private int amountOfEnemy;
    [SerializeField] private float summonCooldown;
    //Signal
    [SerializeField] private GameObject signalObj;
    [SerializeField] private float signalTimer;

    private CaveSummonEnenmy summonEnenmyScript;
    private GameObject caveGo;
    private bool isCaveSummonEnemySkill;

    private List<GameObject> enemies = new List<GameObject>();

    [Header("Dash skill")]
    [SerializeField] private float dashSpeed;
    private Vector3 currentTarget;
    protected bool isDash;
    [SerializeField] private float distanceToStop;
    [Space]

    private static int orderLayout;


    protected override void RecieveDamage(AttackDetail attackDetail)
    {
        base.RecieveDamage(attackDetail);
        if (isIntrinsic)
        {
            isIntrinsic = false; 
            currentIntrinsicCooldown = intrinsicCooldown;
            SpawnBullet();
        }
    }

    protected override void Start()
    {
        base.Start();
        SelectedSkill(0);
    }   

    protected override void Update()
    {
        base.Update();
        if (isCheckSkill)
            CheckCooldownSkill(selectedSkill);
        if (isReadySkills[0])
        {
            isMove = false;
            isCaveSummonEnemySkill = true;
            ResetCooldownSkill(selectedSkill);
        }else if (isReadySkills[1])
        {
            isLock = true;
            isMove = false;
            isDash = true;
            SetCurrentTarget();
            ResetCooldownSkill(selectedSkill);
        }

        if (isCaveSummonEnemySkill)
        {
            isCaveSummonEnemySkill = false;
            StartCoroutine(SpawnCave());
        }else if (isDash)
        {
            Dash(dashSpeed);
        }

        if (!isIntrinsic)
        {
            currentIntrinsicCooldown -= Time.deltaTime;
            if (currentIntrinsicCooldown <= 0)
            {
                isIntrinsic = true;
            }
        }
        
    }

    //==================================Intrintic============================================

    void SpawnBullet()
    {
        if(Random.Range(0,3) == 1)
        {
            angle = Mathf.Atan2(GetDir().y, GetDir().x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Random.Range(0, 360);
        }
        bulletGo = Instantiate(bulletPrefs[Random.Range(0,bulletPrefs.Length)], transform.position, Quaternion.Euler(0,0,angle));
        bulletScript = bulletGo.GetComponent<NormalBullet>();
        bulletScript.CreateBullet(bulletDamage, bulletSpeed, bulletTimeLife);
    }

    //=======================================Cave Summon Enemy Skill===============================================

    Vector3[] GetDestination()
    {
        Vector3[] destination = new Vector3[amountOfCave];
        for (int i = 0; i < amountOfCave; i++)
        {
            destination[i] = SpawnerManager.Instance.GetRandomSpawnPosition();
        }
        return destination;
    }

    IEnumerator SpawnCave()
    {
        Vector3[] destination = GetDestination();
        for (int i = 0; i < destination.Length; i++)
        {
            GameObject go = Instantiate(signalObj, destination[i], Quaternion.identity);
            Destroy(go, signalTimer);
        }
        yield return new WaitForSeconds(signalTimer);
        for (int i = 0; i < amountOfCave; i++)
        {
            caveGo = Instantiate(cavePref, destination[i], Quaternion.identity);
            summonEnenmyScript = caveGo.GetComponent<CaveSummonEnenmy>();
            summonEnenmyScript.CreateCave(enemys, amountOfEnemy, summonCooldown);
        }
        isMove = true;
        ChangeSkillRandom();
    }

     void EnemyAllDeath()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Cave"));

        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();

    }

    //=================================Dash Skill====================

    void SetCurrentTarget()
    {
        currentTarget = target.position;
    }
    
    public void Dash(float dashSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, dashSpeed * Time.deltaTime);
        DashEffect();
        CheckDistanceToStopDash(transform.position, currentTarget, distanceToStop);
    }

    public void CheckDistanceToStopDash(Vector3 a, Vector3 b, float distance)
    {
        if (Vector2.Distance(a, b) <= distance)
        {
            isLock = false;
            isDash = false;
            isMove = true;
            SetVelocityZero();
            ChangeSkillRandom();
        }
    }

    void DashEffect()
    {
        if (target.GetComponent<Player>().isDie)
            return;
        GameObject obj = new GameObject();
        SpriteRenderer spriteEffect = obj.AddComponent<SpriteRenderer>();
        spriteEffect.sprite = spriteRenderer.sprite;
        spriteEffect.color = new Color(1, 1, 1, .8f);
        spriteEffect.sortingLayerName = "Enemy";
        spriteEffect.sortingOrder = orderLayout;
        orderLayout++;
        obj.transform.position = transform.position;
        obj.transform.localScale = transform.localScale;
        obj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        Destroy(obj, .05f);
    }

    protected override void Dead()
    {
        base.Dead();
        EnemyAllDeath();
    }
}
