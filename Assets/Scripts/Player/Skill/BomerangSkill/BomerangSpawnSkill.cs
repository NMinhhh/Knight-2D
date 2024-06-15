using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomerangSpawnSkill : MonoBehaviour
{
    [Header("Bomerang Pref")]
    [SerializeField] private GameObject spawnGo;
    private GameObject go;
    private Bomerang script;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    [Header("Info")]
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float timeLife;

    private int level;

    void Start()
    {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (timer <= 0 && level > 0)
        {
            SetSkill(level);
            timer = cooldown;
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
    }

    void SetSkill(int level)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Throw);
        float roationZ;
        Vector3 direction;
        int amountOfEnemy = 0;
        Collider2D[] enemy = GetPositionInCam.Instance.GetEnemysPosition();
        for (int i = 0; i < level; i++)
        {
            if (enemy.Length > 0 && amountOfEnemy < enemy.Length)
            {
                amountOfEnemy++;
                direction = (enemy[i].gameObject.transform.position - transform.position).normalized;
                roationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                roationZ = Random.Range(0, 360);
            }
            go = Instantiate(spawnGo, transform.position, Quaternion.Euler(0,0,roationZ));
            script = go.GetComponent<Bomerang>();
            script.CreateObj(damage, speed, timeLife);
        }
    }
}
