using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform checkPoint;
    [SerializeField] private float radius;

    //Info Skill
    private float damage;
    [SerializeField] private LayerMask whatIsEnemy;

    //draw line
    private LineRenderer lineRenderer;

    //Look pos enemy
    private GameObject nearestObj;


    AttackDetail attackDetail;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       ShootLaser();
    }

    void ShootLaser()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        if(hits.Length > 0 && nearestObj == null)
        {
            nearestObj = hits[Random.Range(0, hits.Length)].gameObject;
        }
        //allObj = GameObject.FindGameObjectsWithTag("Enemy");
        //for (int i = 0; i < allObj.Length; i++)
        //{
        //    distance = Vector3.Distance(transform.position, allObj[i].transform.position);
        //    if (distance < nearestDis && nearestObj == null)
        //    {
        //        nearestObj = allObj[i];
        //        nearestDis = distance;
        //    }
        //}
        if(nearestObj != null)
        {
            Draw2dRay(transform.position, nearestObj.transform.position);
            attackDetail.damage = damage;
            attackDetail.attackDir = transform;
            nearestObj.transform.SendMessage("Damage", attackDetail);
        }
        else
        {
            Draw2dRay(transform.position, transform.position);
        }
    }

    void Draw2dRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    public void SetLaser(float damage)
    {
        this.damage = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
