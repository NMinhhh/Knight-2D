using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public StateMachine stateMachine {  get; private set; }

    [SerializeField] private EntityData data;
    

    //Component
    public Animator anim {  get; private set; }
    public Rigidbody2D rb {  get; private set; }

    //Other Variable
    [SerializeField] private Transform checkPlayerPos;
    

    public Transform target { get; private set; }
    public int facingRight;


    void Start()
    {
        stateMachine = new StateMachine();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        facingRight = 1;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
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

        //Other Funtion
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

}
