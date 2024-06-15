using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawnSkill : MonoBehaviour
{
    [Header("Lightning Pref")]
    [SerializeField] private GameObject lightning;
    private GameObject go;
    private Lightning script;

    [Header("Damage")]
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    private int level;

    void Start()
    {
        timer = cooldown;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && level > 0)
        {
            timer = cooldown;
            SetSkill(level);
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
    }

    void SetSkill(int amount)
    {
        Vector3 pos;
        Collider2D[] enemy = GetPositionInCam.Instance.GetEnemysPosition();
        int amountOfEnemy = 0;
        for (int i = 0; i < amount; i++)
        {
            if (enemy.Length > 0 && amountOfEnemy < enemy.Length)
            {
                pos = enemy[i].transform.position;
                amountOfEnemy++;
            }
            else
            {
                pos = GetPositionInCam.Instance.GetPositionInArea();
            }
            go = Instantiate(lightning, new Vector3(pos.x, pos.y + 30, 0), Quaternion.identity);
            script = go.GetComponent<Lightning>();
            script.Set(pos, damage);
            if (i == 0)
                script.CanPlaySound();
        }
    }
}
