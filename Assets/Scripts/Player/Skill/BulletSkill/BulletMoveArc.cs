using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveArc : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [Header("Animation")]
    [SerializeField] private Texture[] textures;
    private int animationStep;
    private float timeChange;

    [Header("Particle Explode")]
    [SerializeField] private GameObject effect;

    [Header("Fly line")]
    private Vector3[] points = new Vector3[3];
    [SerializeField] private float radius;

    private float damage;
    private float speed;
    private GameObject go;

    private Vector3 m1, m2;
    private float count = 0;

    [SerializeField] private LayerMask whatIsEnemy;

    private TrailRenderer trailRenderer;

    AttackDetail attackDetail;


    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }


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

        if (go != null)
        {
            points[2] = go.transform.position;
        }
        if(count < 1)
        {
            count += speed * Time.deltaTime;
            m1 = Vector3.Lerp(points[0], points[1], count);
            m2 = Vector3.Lerp(points[1], points[2], count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
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
            trailRenderer.material.SetTexture("_MainTex", textures[animationStep]);
            timeChange = 0;

        }
    }

    void Attack()
    {
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;
        if(Vector3.Distance(transform.position,points[2]) <= .1f)
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.BulletExplodeSkill);
            Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, whatIsEnemy);
            if (hit)
            {
                hit.transform.SendMessage("Damage", attackDetail);
            }
            Instantiate(effect, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(gameObject);
        }
    }

    public void CreateBlletObj(GameObject go, Vector3 point0, Vector3 point1, Vector3 point2,float damage, float speed)
    {
        this.go = go;
        this.points[0] = point0;
        this.points[1] = point1;
        if(this.go == null)
        {
            this.points[2] = point2;
        }
        else
        {
            points[2] = go.transform.position;
        }
        this.damage = damage;
        this.speed = speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
