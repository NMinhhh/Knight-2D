using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Electric : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Texture[] textures;
    private int animationStep;
    private float timeChange;

    [Header("Particle")]
    [SerializeField] private GameObject particle;
    private float timer;

    //Info Skill
    private float damage;
    private float damageTime;
    private float damageTimeCur;
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
        Attack();
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

    void Attack()
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
            timer += Time.deltaTime;
            if(timer > .1f)
            {
                Instantiate(particle, enemyRamdom.transform.position, Quaternion.identity);
                timer = 0;
            }
            attackDetail.damage = damage;
            attackDetail.attackDir = transform;
            attackDetail.continousDamage = false;
            damageTimeCur += Time.deltaTime;
            if(damageTimeCur >= damageTime)
            {
                enemyRamdom.transform.SendMessage("Damage", attackDetail);
                damageTimeCur = 0;
            }
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

    public void SetElectric(float damage, float damageTime)
    {
        this.damage = damage;
        this.damageTime = damageTime;
    }

}
