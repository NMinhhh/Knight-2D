using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [Space]
    [Space]

    [Header("Move")]
    [SerializeField] private float speed;
    public Transform target {  get; private set; }
    private bool isMove;
    public bool isSkill;
    [Space]
    [Space]

    [Header("Hurt")]
    [SerializeField] private float hurtTime;
    [SerializeField] private float knockbackSpeed;
    [SerializeField] private float timerH;
    private float tim;
    private bool isKnockback;


    [Header("Attack")]
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private Vector2 sizeCheckPlayer;
    [SerializeField] private LayerMask whatIsPlayer;
    public LayerMask player { get; private set; }

    //Other Variable
    [SerializeField] private GameObject blood;
    [SerializeField] private Transform bloodPoint;
    private float facingRight;

    //Components
    public Animator anim {  get; private set; }
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = whatIsPlayer;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
        facingRight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFlip();
        Movement();
        tim += Time.deltaTime;
    }
    public Vector2 GetDir()
    {
        return (target.position - transform.position).normalized;
    }
    void Movement()
    {
        if (PlayerDetected() || isSkill)
        {
            isMove = false;
            rb.velocity = Vector2.zero;
        }
        else if(!PlayerDetected() && !isKnockback && !isSkill)
        {
            isMove = true;
            rb.velocity = GetDir() * speed;
        }
        anim.SetBool("move", isMove);
    }

    public bool PlayerDetected()
    {
        return Physics2D.OverlapBox(checkPlayer.position, sizeCheckPlayer, 0, whatIsPlayer);
    }

    void RecieveDamage(AttackDetail attackDetail)
    {
        currentHealth = Mathf.Clamp(currentHealth - attackDetail.damage, 0, maxHealth);
        if (currentHealth > 0)
        {
            StartCoroutine(Hurt());
        }
        if (currentHealth <= 0)
        {
            GameManager.Instance.PickupCoins(10);
            int ran = Random.Range(0, 100);
            int ran2 = Random.Range(0, 100);
            if (ran == ran2)
            {
                SpawnerManager.Instance.SpawnItem(SpawnerManager.Instance.GetItem(0), transform.position);
            }
            SpawnerManager.Instance.SpawnItem(SpawnerManager.Instance.GetItem(1), transform.position);
            Instantiate(blood, bloodPoint.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Damage(AttackDetail attackDetail)
    {
        if (attackDetail.continousDamage)
        {
            if (tim < timerH) return;
            RecieveDamage(attackDetail);
            tim = 0;
        }
        else
        {
            RecieveDamage(attackDetail);
        }
    }

    IEnumerator Hurt()
    {
        isKnockback = true;
        sprite.color = new Color(.95f,.6f , .6f, 1);
        rb.velocity = GetDir() * -knockbackSpeed;
        yield return new WaitForSeconds(hurtTime);
        sprite.color = new Color(1, 1, 1, 1);
        isKnockback = false;
    }

    void CheckIfFlip()
    {
        if (target.position.x < transform.position.x && facingRight == 1)
        {
            Flip();
        }
        else if (target.position.x > transform.position.x && facingRight == -1)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPlayer.position, sizeCheckPlayer);
    }
}
