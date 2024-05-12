using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask whatIsEnemy;
    private float damage;
    private GameObject go;

    [SerializeField] private float cooldown;
    private float timer;

    private Animator anim;

    AttackDetail attackDetail;


    public bool isLaser {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaser) return;
        timer += Time.deltaTime;
        if(go != null)
        {
            transform.right = GetDir();
        }
        if(timer >= cooldown)
        {
            timer = 0;
            anim.SetBool("flash", true);
        }
        
    }

    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position, size, transform.localEulerAngles.z, whatIsEnemy);
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;
        foreach (Collider2D col in hits)
        {
            if (col)
            {
                col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }

    private Vector2 GetDir()
    {
        return (go.transform.position - transform.position).normalized;
    }

    void FinishAnimation()
    {
        timer = 0;
        OffLaser();
        anim.SetBool("flash", false);
    }

    public void CreateLaser(float damage)
    {
        this.damage = damage;
    }

    public void OnLaser(GameObject go)
    {
        this.go = go;
        isLaser = true;
        anim.SetBool("ready", isLaser);
    }

    public void OnLaser()
    {
        isLaser = true;
        anim.SetBool("ready", isLaser);
    }

    private void OffLaser()
    {
        isLaser = false;
        anim.SetBool("ready", isLaser);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackPoint.position, size);
    }
}
