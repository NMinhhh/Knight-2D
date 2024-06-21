using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSkill : MonoBehaviour
{
    [Header("LavaPref")]
    [SerializeField] private GameObject lavaObj;
    private GameObject go;
    private Lava script;

    [Header("Info")]
    [SerializeField] private float damageLevelUp;
    [Range(0, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;
    [SerializeField] private float timeLife;
    [SerializeField] private float damageCooldown;

    [Header("Size lava")]
    [SerializeField] private Vector3[] localScaleLavaObjs;
    [SerializeField] private float[] sizeTakeDamage;

    [Header("Cooldown")]
    [SerializeField] private float cooldownSpawn;
    private float timer;
    private int level;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > cooldownSpawn && level > 0)
        {
            SpawnLava();
            timer = 0;
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
    }

    void SpawnLava()
    {
        go = Instantiate(lavaObj, transform.position, Quaternion.identity);
        go.transform.localScale = localScaleLavaObjs[level - 1];
        script = go.GetComponent<Lava>();
        script.CreateLava(damage, damageCooldown, timeLife, sizeTakeDamage[level - 1]);
    }
}
