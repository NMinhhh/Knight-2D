using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [Header("Bullet prefs")]
    [SerializeField] private GameObject[] bulletPrefs;
    private GameObject go;
    private NormalBullet script;

    [Header("Delay explosion")]
    [SerializeField] private float delayExplosion;
    private float delayTimer;

    [Header("Speed and destination")]
    [SerializeField] private float bombSpeed;
    private Vector3 destination;

    [Header("Bullet info")]
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletTimeLife;

    [Header("Amount of bullet")]
    [SerializeField] private int amountOfBullet;

    private bool isExplode;

    private float angle;

    private Animator anim;

    private Transform player;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, bombSpeed * Time.deltaTime);
        bombSpeed += bombSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, destination) <= 0.1f && !isExplode)
        {
            isExplode = true;
            anim.SetBool("flash", isExplode);
        }

        if (isExplode)
        {
            delayTimer += Time.deltaTime;
            if(delayTimer > delayExplosion)
            {
                anim.SetBool("explode", isExplode);
            }
        }
    }

    void SpawnBullet()
    {
        int rd = Random.Range(0, amountOfBullet);
        for (int i = 0; i < amountOfBullet; i++)
        {
            if(i == rd)
            {
                Vector2 dir = (player.position - transform.position).normalized;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            }
            else
            {
                angle = Random.Range(0, 360);
            }
            go = Instantiate(bulletPrefs[Random.Range(0, bulletPrefs.Length)], transform.position, Quaternion.Euler(0, 0, angle));
            script = go.GetComponent<NormalBullet>();
            script.CreateBullet(bulletDamage, bulletSpeed, bulletTimeLife);
            
        }
    }

    public void FinishAnimation()
    {
        Destroy(gameObject);
    }

    public void CreateBomb(Vector3 destination ,float bombSpeed, float delayExplosion, float bulletDamage, int amountOfBullet)
    {
        this.destination = destination;
        this.bombSpeed = bombSpeed;
        this.bulletDamage = bulletDamage;
        this.amountOfBullet = amountOfBullet;
        this.delayExplosion = delayExplosion;
    }

}
