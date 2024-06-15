using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    private float damage;
    private float timeLife;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsPlayer;

    AttackDetail attackDetail;
    void Start()
    {
        attackDetail.attackDir = transform;
        Destroy(gameObject, timeLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }

        Attack();
    }

    public void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        if (player)
        {
            player.transform.SendMessage("Damage", attackDetail);
        }
    }

    public void CreateAcidSwawp(float damage, float timeLife)
    {
        this.damage = damage;
        this.timeLife = timeLife;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
