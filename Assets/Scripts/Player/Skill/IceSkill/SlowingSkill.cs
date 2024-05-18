using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingSkill : MonoBehaviour
{
    [Header("Percent decresea speed")]
    [Range(0,100)]
    [SerializeField] private float[] slowingSpeedPercent;
    private float currentSlowingSpeedPercent;
    [Header("Radius check enemy")]
    [SerializeField] private float radius;


    [SerializeField] private LayerMask whatIsEnemy;

    private bool isSkill;

    private Animator anim;

    private int level;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isSkill) return;
        Slowing();
    }

    public void LevelUp(int level)
    {
        this.level = level;
        if (!isSkill)
        {
            isSkill = true;
            anim.SetBool("isSkill", isSkill);
        }
        UpdateCooldown();

    }

    void UpdateCooldown()
    {
        currentSlowingSpeedPercent = slowingSpeedPercent[level - 1];
    }

    public void Slowing()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        foreach (Collider2D hit in hits)
        {
            if (hit)
            {
                hit.transform.SendMessage("IsSlowingEffect", currentSlowingSpeedPercent);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Update is called once per frame

}
