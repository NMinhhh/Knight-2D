using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region State Machine

    protected StateMachine stateMachine;
    [SerializeField] private EntityData data;

    #endregion


    #region Component

    public Animator anim {  get; private set; }
    public Rigidbody2D rb {  get; private set; }

    private SpriteRenderer sprite;

    #endregion

    #region Transform

    [SerializeField] private Transform checkPlayerPos;
    [SerializeField] private Transform touchDamagePos;

    #endregion

    #region Other Variable

    //Hurt and dead
    private float currentHealth;
    private bool isKnockback;
    private float currentDamageTimeCon;
    protected bool isDead;

    //Touch Damage Player
    private float timeTouch;


    //Get cooldown attack
    [SerializeField] private bool haveAttackStates;
    private List<float> currentCooldownAttack;
    //attack selected
    public int stateAttackSelected { get; private set; }

    //state attack ready start
    public bool[] isReady { get; private set; }

    //Dash
    public bool isDash {  get; private set; }
    public Vector2 currentTarget {  get; private set; }



    public Transform target { get; private set; }
    public int facingRight {  get; private set; }

    public AttackDetail attackDetail;

    #endregion

    #region Unity Function Callback

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
        timeTouch = data.cooldownTouchDamage;
        isDash = true;
        if(haveAttackStates)
        {
            //Init
            currentCooldownAttack = new List<float>();
            //add cooldown data
            currentCooldownAttack.AddRange(data.cooldownAttack);
            //Init 
            isReady = new bool[data.cooldownAttack.Count];
            //Set State Attack 
            SelectedAttackState(0);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        currentDamageTimeCon -= Time.deltaTime;

        TouchDamagePlayer();

        if (haveAttackStates)
        {
            CheckCooldownAttack(stateAttackSelected);
        }

       
    }

    protected virtual void FixedUpdate()
    {
         stateMachine.currentState.PhysicUpdate();
    }

    #endregion

    #region Set Function
    public void SetMovement(float speed)
    {
        rb.velocity = GetDir() * speed;
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
    #endregion

    #region Chek Function
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

    #endregion

    #region Attck States

    public int GetRandomAttackState()
    {
        return Random.Range(0, currentCooldownAttack.Count);
    }

    //Reset cooldown attack
    public void ResetCooldownAttack(int idx)
    {
        currentCooldownAttack[idx] = data.cooldownAttack[idx];
        isReady[idx] = false;
    }

    //Selected Attack State
    public void SelectedAttackState(int idx)
    {
        stateAttackSelected = idx;
    }

    //Check State attack ready start
    void CheckCooldownAttack(int id)
    {
        currentCooldownAttack[id] -= Time.deltaTime;
        if (currentCooldownAttack[id] <= 0)
        {
            isReady[id] = true;
        }
    }

    #endregion

    #region Dash
    //Dashing is true
    public void IsDashing()
    {
        isDash = true;
    }
   
    public void CheckDistanceToStopDash(Vector3 a, Vector3 b, float distance)
    {
        if (Vector2.Distance(a, b) <= distance)
        {
            isDash = false;
            SetVelocityZero();
        }
    }
   
    public void GetCurrentTargetToDashing()
    {
        currentTarget = target.position;
    }

    public void Dash(float dashSpeed, Vector2 dir)
    {
        if (isDash)
        {
            rb.velocity = dir * dashSpeed;
            //Create image After 
            GameObject obj = new GameObject();
            SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
            sr.sprite = sprite.sprite;
            sr.flipX = true;
            sr.sortingLayerName = "Enemy";
            obj.transform.position = transform.position;
            obj.transform.localScale = transform.localScale;
            Destroy(obj, .1f);
        }

    }

    #endregion

    #region Other Fuction

    void TouchDamagePlayer()
    {
        timeTouch -= Time.deltaTime;
        attackDetail.attackDir = transform;
        attackDetail.damage = data.touchDamage;
        Collider2D[] hit = Physics2D.OverlapBoxAll(touchDamagePos.position, data.sizeTouch, 0, data.whatIsPlayer);
        Collider2D hitShield = Physics2D.OverlapBox(touchDamagePos.position, data.sizeTouch, 0, data.whatIsShield);

        if (hitShield)
        {
            hitShield.transform.parent.SendMessage("DamageShield");
            return;
        }

        foreach (Collider2D col in hit)
        {
            if (col && timeTouch <= 0)
            {
                col.transform.SendMessage("Damage", attackDetail);
                timeTouch = data.cooldownTouchDamage;
            }
        }
    }

    IEnumerator Hurt()
    {
        isKnockback = true;
        sprite.color = new Color(.95f, .6f, .6f, 1);
        rb.velocity = GetDir() * -data.knockbackSpeed;
        yield return new WaitForSeconds(data.hurtTime);
        sprite.color = new Color(1, 1, 1, 1);
        isKnockback = false;
    }

    public void DropItem()
    {
        //int ran = Random.Range(0, 100);
        //int ran2 = Random.Range(0, 100);
        //if (ran == ran2)
        //{
        //    SpawnerManager.Instance.SpawnItem(SpawnerManager.Instance.GetItem(0), transform.position);
        //}
        SpawnerManager.Instance.SpawnItem(SpawnerManager.Instance.GetItem(1), transform.position);
    }

    void RecieveDamage(AttackDetail attackDetail)
    {
        if(isDead) return;
        currentHealth = Mathf.Clamp(currentHealth - attackDetail.damage, 0, data.maxHealth);
        if (currentHealth > 0)
        {
            StartCoroutine(Hurt());
        }
        else
        {
            isDead = true;

        }
    }

    public virtual void Damage(AttackDetail attackDetail)
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPlayerPos.position, data.sizeCheck);
        Gizmos.DrawWireCube(touchDamagePos.position, data.sizeTouch);
    }

    #endregion
}
