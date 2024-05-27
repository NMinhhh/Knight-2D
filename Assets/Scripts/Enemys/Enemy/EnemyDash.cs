using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : Enemy
{
    [Header("Check player to dash")]
    [SerializeField] private Transform checkPlayerDetected;
    [SerializeField] private Vector2 checkPlayerDetectedSize;
    [Space]

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    private Vector3 currentTarget;
    protected bool isDash;
    [SerializeField] private float distanceToStop;
    [Space]

    [Header("Dash cooldown")]
    [SerializeField] private float dashCooldown;
    private float dashTimer;

    private static int orderLayout;

    protected override void Start()
    {
        base.Start();
        dashTimer = dashCooldown;
    }

    protected override void Update()
    {
        base.Update();
        dashTimer += Time.deltaTime;
        if (dashTimer >= dashCooldown && !isDash && CheckPlayerDetected())
        {
            isLock = true;
            isDash = true;
            isMove = false;
            SetCurrentTarget();
        }
        if (isDash)
        {
            Dash(dashSpeed);
        }
    }

   
    void SetCurrentTarget()
    {
        currentTarget = target.position;
    }

    void DashEffect()
    {
        GameObject obj = new GameObject();
        SpriteRenderer spriteEffect = obj.AddComponent<SpriteRenderer>();
        spriteEffect.sprite = spriteRenderer.sprite;
        spriteEffect.color = new Color(1, 1, 1, .8f);
        spriteEffect.sortingLayerName = "Enemy";
        spriteEffect.sortingOrder = orderLayout;
        orderLayout++;
        obj.transform.position = transform.position;
        obj.transform.localScale = transform.localScale;
        obj.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        Destroy(obj, .05f);
    }

    public void CheckDistanceToStopDash(Vector3 a, Vector3 b, float distance)
    {
        if (Vector2.Distance(a, b) <= distance)
        {
            isDash = false;
            isLock = false;
            SetVelocityZero();
            isMove = true;
            dashTimer = 0;
        }
    }

    public void Dash(float dashSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, dashSpeed * Time.deltaTime);
        DashEffect();
        CheckDistanceToStopDash(transform.position, currentTarget, distanceToStop);
    }

    bool CheckPlayerDetected()
    {
        return Physics2D.OverlapBox(checkPlayerDetected.position, checkPlayerDetectedSize, 0, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(checkPlayerDetected.position, checkPlayerDetectedSize);
    }
}
