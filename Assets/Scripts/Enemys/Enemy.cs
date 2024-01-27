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
    private Transform target;
    private bool isMove;
    [Space]
    [Space]

    [Header("Hurt")]
    [SerializeField] private float hurtTime;
    [SerializeField] private float knockbackSpeed;


    [Header("Attack")]
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private Vector2 sizeCheckPlayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radiusAttack;
    [SerializeField] private float damage;
    [SerializeField] private float cooldownAttack;
    private float timeAttack;
    [SerializeField] private LayerMask whatIsPlayer;

    //Other Variable
    private float facingRight;
    private AttackDetail attackDetail;
    private bool isFinishAnimation;
    private bool isAttack;
    //Components
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
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
        Attack();
        CheckIfFlip();
        Movement();
    }
    Vector2 GetDir()
    {
        return (target.position - transform.position).normalized;
    }
    void Movement()
    {
        if (PlayerDetected())
        {
            isMove = false;
            rb.velocity = Vector2.zero;
        }
        else if(!PlayerDetected() && !isAttack)
        {
            isMove = true;
            rb.velocity = GetDir() * speed;
        }
        anim.SetBool("move", isMove);
    }

    void Attack()
    {
        timeAttack += Time.deltaTime;
        if (isFinishAnimation)
        {
            anim.SetBool("attack", false);
            isFinishAnimation = false;
            isAttack = false;
        }
        else if (PlayerDetected() && !isFinishAnimation && timeAttack >= cooldownAttack)
        {
            timeAttack = 0;
            isAttack = true;
            anim.SetBool("attack", true);
            rb.velocity = Vector2.zero;
        }
        
    }

    void TriggerAnimation()
    {
        attackDetail.damage = this.damage;
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, radiusAttack, whatIsPlayer);
        foreach (Collider2D col in hit)
        {
            if (col)
            {
                Debug.Log("Damage");
                //col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }

    void FinishAnimation()
    {
        isFinishAnimation = true;
    }

    bool PlayerDetected()
    {
        return Physics2D.OverlapBox(checkPlayer.position, sizeCheckPlayer, 0, whatIsPlayer);
    }

    void Damage(AttackDetail attackDetail)
    {
        currentHealth = Mathf.Clamp(currentHealth - attackDetail.damage, 0, maxHealth);

        if(currentHealth > 0)
        {
            StartCoroutine(Hurt());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Hurt()
    {
        sprite.color = new Color(.95f,.6f , .6f, 1);
        rb.velocity = Vector2.one;
        rb.velocity = GetDir() * -knockbackSpeed;
        yield return new WaitForSeconds(hurtTime);
        sprite.color = new Color(1, 1, 1, 1);
    }

    void CheckIfFlip()
    {
        if (!isAttack)
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
        Gizmos.DrawWireSphere(attackPoint.position, radiusAttack);
    }
}
