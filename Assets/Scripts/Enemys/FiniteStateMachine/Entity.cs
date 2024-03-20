using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Entity : MonoBehaviour
{
    protected StateMachine stateMachine;

    [SerializeField] private EntityData data;
    

    //Component
    public Animator anim {  get; private set; }
    public Rigidbody2D rb {  get; private set; }
    private SpriteRenderer sprite;
    //Other Variable
    [SerializeField] private Transform checkPlayerPos;

    private float currentHealth;
    private bool isKnockback;
    private float currentDamageTimeCon;

    public Transform target { get; private set; }
    public int facingRight {  get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        target = GameObject.Find("Player").transform;
        facingRight = 1;
        currentDamageTimeCon = data.damageTimeCon;
        currentHealth = data.maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
        currentDamageTimeCon -= Time.deltaTime;
    }

    protected virtual void FixedUpdate()
    {
         stateMachine.currentState.PhysicUpdate();
    }
    //Set function
    public void SetMovement(float speed)
    {
        rb.velocity = GetDir() * speed;
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }

    //Check fuction

    public bool CheckPlayer()
    {
        return Physics2D.OverlapBox(checkPlayerPos.position, data.sizeCheck, 0, data.whatIsPlayer);

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
    //Other Funtion
    IEnumerator Hurt()
    {
        isKnockback = true;
        sprite.color = new Color(.95f, .6f, .6f, 1);
        rb.velocity = GetDir() * -data.knockbackSpeed;
        yield return new WaitForSeconds(data.hurtTime);
        sprite.color = new Color(1, 1, 1, 1);
        isKnockback = false;
    }

    void RecieveDamage(AttackDetail attackDetail)
    {
        currentHealth = Mathf.Clamp(currentHealth - attackDetail.damage, 0, data.maxHealth);
        if (currentHealth > 0)
        {
            StartCoroutine(Hurt());
        }
        if (currentHealth <= 0)
        {
            CoinManager.Instance.PickupCoins(10);
            int ran = Random.Range(0, 100);
            int ran2 = Random.Range(0, 100);
            if (ran == ran2)
            {
                SpawnerManager.Instance.SpawnItem(SpawnerManager.Instance.GetItem(0), transform.position);
            }
            SpawnerManager.Instance.SpawnItem(SpawnerManager.Instance.GetItem(1), transform.position);
            //Instantiate(blood, bloodPoint.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Damage(AttackDetail attackDetail)
    {
        if (attackDetail.continousDamage)
        {
            if (currentDamageTimeCon > 0) return;
            RecieveDamage(attackDetail);
            currentDamageTimeCon = data.damageTimeCon;
        }
        else
        {
            RecieveDamage(attackDetail);
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight *= -1;
    }

    public Vector2 GetDir()
    {
        return (target.position - transform.position).normalized;
    }

    public void TriggerAnimation() => stateMachine.currentState.TriggerAnimation();

    public void FinishAnimtion() => stateMachine.currentState.FinishAnimation();

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPlayerPos.position, data.sizeCheck);
    }

}
