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
    private bool isKnockback;


    [Header("Attack")]
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private Vector2 sizeCheckPlayer;
    [SerializeField] private LayerMask whatIsPlayer;
    public LayerMask player { get; private set; }

    //Other Variable
    [SerializeField] private GameObject floatingText;
    [SerializeField] private GameObject blood;
    [SerializeField] private Transform bloodPoint;
    private float facingRight;
    private int damageDir;

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
        else if(!PlayerDetected() && !IsAttacking() && !isKnockback && !isSkill)
        {
            isMove = true;
            rb.velocity = GetDir() * speed;
        }
        anim.SetBool("move", isMove);
    }

    bool IsAttacking()
    {
        return anim.GetBool("attack");
    }

    public bool PlayerDetected()
    {
        return Physics2D.OverlapBox(checkPlayer.position, sizeCheckPlayer, 0, whatIsPlayer);
    }

    void Damage(AttackDetail attackDetail)
    {
        currentHealth = Mathf.Clamp(currentHealth - attackDetail.damage, 0, maxHealth);
        if (attackDetail.attackDir.position.x > transform.position.x)
        {
            damageDir = -1;
        }
        else
        {
            damageDir = 1;
        }
        FloatingTextManager.Instance.CreateFloatingText(floatingText, transform, attackDetail.damage.ToString(), damageDir);
        if(currentHealth > 0)
        {
            StartCoroutine(Hurt());
        }
        if(currentHealth <= 0)
        {
            GameManager.Instance.PickupCoins(10);
            Instantiate(blood, bloodPoint.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator Hurt()
    {
        isKnockback = true;
        sprite.color = new Color(.95f,.6f , .6f, 1);
        rb.velocity = Vector2.zero;
        if (!IsAttacking())
        {
            rb.velocity = GetDir() * -knockbackSpeed/2;
        }
        yield return new WaitForSeconds(hurtTime);
        sprite.color = new Color(1, 1, 1, 1);
        isKnockback = false;
    }

    void CheckIfFlip()
    {
        if (!IsAttacking())
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
