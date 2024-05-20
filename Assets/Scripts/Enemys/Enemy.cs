using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    protected bool isMove = true;
    [Space]
    [Space]

    [Header("Check player")]
    [SerializeField] private Transform checkPlayerPos;
    [SerializeField] private float checkRadius;
    [Space]
    [Space]

    [Header("Damage On Touch")]
    [SerializeField] private Transform touchDamagePos;
    [SerializeField] private float touchRadius;
    [SerializeField] protected float touchDamage;
    [SerializeField] protected float touchCooldown;
    protected float touchTimer;
    [Space]
    [Space]

    [Header("Hurt")]
    [SerializeField] protected float basicHealth;
    [SerializeField] protected float healthLevelUp;
    [SerializeField] protected float healthLevelUpPercent;
    [SerializeField] protected float damageTimerCon;
    [SerializeField] protected float hurtEffectTimer;
    protected float maxHealth;
    protected float currentHealth;
    protected float currentDamageTimeCon;
    protected bool isDead;
    protected bool isImortal = true;
    [Header("Floating Text")]
    [SerializeField] protected GameObject floatingText;
    [SerializeField] protected Color floatingTextColor;
    [Space]
    [Space]

    [Header("Dead")]
    [SerializeField] private GameObject particleBlood;
    [SerializeField] protected int amountOfEx;
    [SerializeField] protected float radiusPointEx;
    protected Vector3 dropItemPoint;
    [Space]
    [Space]

    [Header("Slowing")]
    [SerializeField] private float slowingTimer;
    [SerializeField] private Color slowingColor;
    private bool isSlowingEffect;
    private float slowingTimerCur;
    private float decreseaSpeedPercent;
    [Space]
    [Space]

    [Header("Layer Mask")]
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected LayerMask whatIsShield;



    protected int damageDir;
    protected int facingRight;
    protected AttackDetail attackDetail;

    protected Transform target;

    protected Animator anim;

    protected Rigidbody2D rb;

    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player").transform;
        facingRight = 1;
        SetHealth();
    }

    public void SetHealth()
    {
        maxHealth = GameManager.Instance.Calculate(basicHealth, healthLevelUp, healthLevelUpPercent, GameManager.Instance.stage);
        currentHealth = maxHealth;
        isImortal = false;
    }

    protected virtual void Update()
    {
        if (!CheckPlayer() && isMove)
        {
            SetMovement(speed);
        }
        else if(CheckPlayer() || !isMove)
        {
            SetVelocityZero();
        }

        CheckIfFlip();

        TouchDamagePlayer();
        currentDamageTimeCon -= Time.deltaTime;

        if (isSlowingEffect)
            SlowingEffect();
    }

    void IsSlowingEffect(int percent)
    {
        isSlowingEffect = true;
        slowingTimerCur = slowingTimer;
        this.decreseaSpeedPercent = percent;
    }

    void SlowingEffect()
    {
        slowingTimerCur -= Time.deltaTime;
        spriteRenderer.color = slowingColor;
        if (slowingTimerCur <= 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            isSlowingEffect = false;
        }

    }

    protected virtual void Dead()
    {
        Instantiate(particleBlood, transform.position, Quaternion.identity);
        //for (int i = 0; i < amountOfEx; i++)
        //{
        //    dropItemPoint = Random.insideUnitCircle * radiusPointEx;
        //    DropItem(transform.position + dropItemPoint);
        //}
        Destroy(gameObject);
        GameManager.Instance.AddKill();
        SpawnerManager.Instance.SpawnItem(SpawnerManager.Instance.GetItem(0), transform.position);
    }

    protected virtual void DropItem(Vector3 point)
    {
        int ran = Random.Range(0, 50);
        int ran2 = Random.Range(0, 50);
        
        int ran3 = Random.Range(0, 100);
        if (ran3 == 20)
            SpawnerManager.Instance.SpawnItem(SpawnerManager.Instance.GetItem(1), point);

    }

    IEnumerator Hurt()
    {
        spriteRenderer.color = new Color(.95f, .6f, .6f, 1);
        yield return new WaitForSeconds(hurtEffectTimer);
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void RecieveDamage(AttackDetail attackDetail)
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
        FloatingTextManager.Instance.CreateFloatingText(floatingText, transform.position, attackDetail.damage.ToString(), floatingTextColor, damageDir);
        if (currentHealth > 0)
        {
            StartCoroutine(Hurt());
        }
        else
        {
            isDead = true;
        }

        if (isDead)
        {
            Dead();
        }
    }

    public virtual void Damage(AttackDetail attackDetail)
    {
        if (isDead || isImortal) return;
        if (attackDetail.continousDamage)
        {
            if (currentDamageTimeCon > 0) return;
            RecieveDamage(attackDetail);
            currentDamageTimeCon = damageTimerCon;
        }
        else
        {
            RecieveDamage(attackDetail);
        }
    }


    void TouchDamagePlayer()
    {
        touchTimer -= Time.deltaTime;
        attackDetail.attackDir = transform;
        attackDetail.damage = touchDamage;
        Collider2D hit = Physics2D.OverlapCircle(touchDamagePos.position, touchRadius, whatIsPlayer);
        Collider2D hitShield = Physics2D.OverlapCircle(touchDamagePos.position, touchRadius, whatIsShield);

        if (hitShield)
        {
            hitShield.transform.parent.SendMessage("DamageShield");
            return;
        }

        if (hit && touchTimer <= 0)
        {
            hit.transform.SendMessage("Damage", attackDetail);
            touchTimer = touchCooldown;
        }
    }
    public void SetMovement(float speed)
    {
        if (isSlowingEffect)
            rb.velocity = GetDir() * (speed - speed * decreseaSpeedPercent/100);
        else
            rb.velocity = GetDir() * speed;
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }

    public bool CheckPlayer()
    {
        return Physics2D.OverlapCircle(checkPlayerPos.position, checkRadius, whatIsPlayer);

    }
    public void CheckIfFlip()
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

    public Vector2 GetDir()
    {
        return (target.position - transform.position).normalized;
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight *= -1;
    }


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPlayerPos.position, checkRadius);
        Gizmos.DrawWireSphere(touchDamagePos.position, touchRadius);
        Gizmos.DrawWireSphere(transform.position, radiusPointEx);
    }
}
