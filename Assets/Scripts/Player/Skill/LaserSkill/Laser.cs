using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Animation
    [SerializeField] private Texture[] textures;
    private int animationStep;
    private float timeChange;

    [SerializeField] private GameObject particle;

    //Info Skill
    private float damage;
    [SerializeField] private LayerMask whatIsEnemy;

    //draw line
    private LineRenderer lineRenderer;

    //Look pos enemy
    private GameObject enemyRamdom;


    AttackDetail attackDetail;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
        Animation();
    }

    void Animation()
    {
        timeChange += Time.deltaTime;
        if(timeChange > .1f)
        {
            animationStep++;
            if(animationStep == textures.Length) 
            { 
                animationStep = 0;
            }
            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);
            timeChange = 0;
        }
    }

    void ShootLaser()
    {
        //Get position enemy detected
        Collider2D[] hits = EnemysPosition.Instance.GetEnemysPosition();
        if(hits.Length > 0 && enemyRamdom == null)
        {
            enemyRamdom = hits[Random.Range(0, hits.Length)].gameObject;
        }

        if(enemyRamdom != null)
        {
            Draw2dRay(transform.position, enemyRamdom.transform.position);
            Instantiate(particle, enemyRamdom.transform.position, Quaternion.identity);
            attackDetail.damage = damage;
            attackDetail.attackDir = transform;
            enemyRamdom.transform.SendMessage("Damage", attackDetail);
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

   
}
