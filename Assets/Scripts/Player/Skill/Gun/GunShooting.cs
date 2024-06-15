using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GunShooting : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private Transform shootingPoint;
    private GameObject bullet;
    private float damage;
    private float speed;
    private float timeLife;
    private int amountOfBullet;
    private int amountOfBulletCur;

    public bool isReload { get; private set; }

    [Header("Cooldown")]
    [SerializeField] private float cooldownShooting;
    private float timer;

    private Vector3 direction;

    public float angle { get; private set; }

    private GameObject enemyRam;
    [SerializeField] private float timeChageTarget;
    private float currentTimeChage;

    [SerializeField] private bool canPlaySound;

    private Animator anim;

    void Start()
    {
        amountOfBulletCur = amountOfBullet;
        anim = transform.Find("MuzzleFlash").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGunRotationEnemy();
    }

    void HandleGunRotationEnemy()
    {
        Collider2D[] enemys = GetPositionInCam.Instance.GetEnemysPosition();
        currentTimeChage += Time.deltaTime;
        if(enemyRam == null || currentTimeChage >= timeChageTarget)
        {
            currentTimeChage = 0;
            if (enemys.Length <= 0)
                return;
            enemyRam = enemys[Random.Range(0, enemys.Length)].gameObject;
        }
        else if(enemyRam != null)
        {
            HandleGunRotation(enemyRam.transform.position);
            Shooting();

        }
    }

    void HandleGunRotation(Vector2 taget)
    {
        direction = (taget - (Vector2)transform.position).normalized;
        transform.right = direction;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 localScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }

        transform.localScale = localScale;
    }

    void Shooting()
    {
        timer += Time.deltaTime;
        if(timer > cooldownShooting && amountOfBulletCur > 0) 
        {
            if(canPlaySound)
                SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.GunMachine);
            anim.SetTrigger("shoot");
            SpawnBullet(shootingPoint, shootingPoint.rotation);
            amountOfBulletCur--;
            timer = 0;
            if (amountOfBulletCur <= 0)
            {
                isReload = true;
            }
        }
    }

    void SpawnBullet(Transform spawnPos, Quaternion ro)
    {
        GameObject projectile = Instantiate(bullet, spawnPos.position, ro);
        NormalBullet script = projectile.GetComponent<NormalBullet>();
        script.CreateBullet(damage, speed, timeLife);
    }

    public void CreateGun(float damage, float speed, float timeLife, int amountOfBullet, GameObject bullet)
    {
        this.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
        this.amountOfBullet = amountOfBullet;
        this.bullet = bullet;
    }

    public void Reload()
    {
        isReload = false;
        amountOfBulletCur = amountOfBullet;
    }
}
