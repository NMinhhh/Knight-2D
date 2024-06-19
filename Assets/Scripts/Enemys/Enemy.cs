using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [Header("Facing the player and flip")]
    [SerializeField] private bool isFacingPlayer;
    [SerializeField] private bool isFlip;
    protected bool isLock;
    [Space]

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

    [Header("Health and hurt")]
    [SerializeField] protected bool isHealthByStage;
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
    [Space]

    [Header("Floating Text")]
    [SerializeField] protected GameObject floatingText;
    [SerializeField] protected Color floatingTextColor;
    [Space]
    [Space]

    [Header("Dead")]
    [SerializeField] private GameObject particleBlood;
    [SerializeField] protected int amountOfEnergy;
    [SerializeField] protected float radiusPointDropEnergy;
    [SerializeField] protected int coinDead;
    [SerializeField] protected int diamondDead;
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
        if (isHealthByStage)
            maxHealth = MapManager.Instance.Calculate(basicHealth, healthLevelUp, healthLevelUpPercent, MapManager.Instance.stage);
        else
            maxHealth = basicHealth;
        currentHealth = maxHealth;
        isImortal = false;
    }

    protected virtual void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }

        if (isFacingPlayer && !isLock)
        {
            transform.right = GetDir();
        }
        if (isFlip)
        {
            CheckIfFlip();
        }
        if (!CheckPlayer() && isMove)
        {
            SetMovement(speed);
        }
        else if(CheckPlayer() || !isMove)
        {
            SetVelocityZero();
        }


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

    protected virtual void DropItem()
    {
        for (int i = 0; i < amountOfEnergy; i++)
        {
            dropItemPoint = Random.insideUnitCircle * radiusPointDropEnergy;
            SpawnerManager.Instance.SpawnEnergy(transform.position + dropItemPoint);
        }
    }


    protected virtual void Dead()
    {
        Instantiate(particleBlood, transform.position, Quaternion.identity);
        DropItem();
        Destroy(gameObject);
        MapManager.Instance.PickUpCoin(coinDead);
        MapManager.Instance.PickUpDiamond(diamondDead);
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

    protected virtual void RecieveDamage(AttackDetail attackDetail)
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

    IEnumerator Hurt()
    {
        spriteRenderer.color = new Color(.95f, .6f, .6f, 1);
        yield return new WaitForSeconds(hurtEffectTimer);
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

   
    void TouchDamagePlayer()
    {
        touchTimer -= Time.deltaTime;
        attackDetail.attackDir = transform;
        attackDetail.damage = touchDamage;
        Collider2D hit = Physics2D.OverlapCircle(touchDamagePos.position, touchRadius, whatIsPlayer);
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
        Gizmos.DrawWireSphere(transform.position, radiusPointDropEnergy);
    }
}
