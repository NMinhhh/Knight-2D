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

    public void ElectricAttack(Vector2 startPos, Vector2 endPos, GameObject go)
    {
        Draw2dRay(startPos, endPos);
        timer += Time.deltaTime;
        if(timer > .1f)
        {
            Instantiate(particle, go.transform.position, Quaternion.identity);
            timer = 0;
        }
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;
        damageTimeCur += Time.deltaTime;
        if(damageTimeCur >= damageTime)
        {
            if(go != null)
            {
                go.transform.SendMessage("Damage", attackDetail);
            }
            damageTimeCur = 0;
        }
    }

    public void Draw2dRay(Vector2 startPos, Vector2 endPos)
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
