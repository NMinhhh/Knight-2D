using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;
    [Space]

    [Header("Sprite to change")]
    [SerializeField] private Sprite[] skins;
    [Space]

    [Header("Bounce Force")]
    [SerializeField] private float powerForce;
    private float damage;
    private float speed;
    private float timeLife;
    [Space]

    [Header("Radius Check")]
    [SerializeField] private float radius;
    [SerializeField] private float radiusCheck;
    [Space]

    [Header("Layer")]
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsDeathZone;

    GameObject go;

    private bool isDamageFirst;

    private bool isBouncce;

    private List<GameObject> listEnemyCol;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    AttackDetail attackDetail;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = skins[Random.Range(0, skins.Length)];
        rb.velocity = transform.right * speed;
        attackDetail.attackDir = transform;
        listEnemyCol = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }

        if (!isDamageFirst)
        {
            Attack();
            timeLife -= Time.deltaTime;
            if(timeLife <= 0 || CheckDeathZone())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (isBouncce)
            {
                Bounce();
            }
            else
            {
                GetEnemyDetected();
            }
        }
    }

    bool CheckDeathZone()
    {
        return Physics2D.OverlapCircle(transform.position, radiusCheck, whatIsDeathZone);
    }

    void Bounce()
    {
        if(go != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, go.transform.position, powerForce * Time.deltaTime);
            if (Vector3.Distance(transform.position, go.transform.position) <= 0.1f)
            {
                go.transform.SendMessage("Damage", attackDetail);
                listEnemyCol.Add(go);
                isBouncce = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void GetEnemyDetected()
    {
        Collider2D[] enemyCols = Physics2D.OverlapCircleAll(transform.position, radiusCheck, whatIsEnemy);
        foreach(Collider2D col in enemyCols)
        {
            if (!listEnemyCol.Contains(col.gameObject))
            {
                go = col.gameObject;
                transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                isBouncce = true;
                return;
            }
        }
        Destroy(gameObject);
    }

    void Attack()
    {
        Collider2D enemyCol = Physics2D.OverlapCircle(transform.position, radius, whatIsEnemy);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        attackDetail.continousDamage = false;
        if (enemyCol && !listEnemyCol.Contains(enemyCol.gameObject))
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.DiceHit);

            if (!isDamageFirst)
            {
                isDamageFirst = true;
                rb.velocity = Vector2.zero;
            }
            enemyCol.transform.SendMessage("Damage", attackDetail);
            listEnemyCol.Add(enemyCol.gameObject);
        }
    }

    public void CreateDice(float damage, float speed, float timeLife)
    {
        this.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, radiusCheck);
    }
}
