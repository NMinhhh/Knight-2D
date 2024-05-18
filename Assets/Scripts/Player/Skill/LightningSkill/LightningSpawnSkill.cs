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
    [SerializeField] private float basicDamage;
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    private int level;

    private Transform cam;

    void Start()
    {
        timer = cooldown;
        cam = GameObject.Find("Main Camera").transform;
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
        damage = GameManager.Instance.Calculate(basicDamage, damageLevelUp, damageLevelUpPercent, this.level);
    }

    void SetSkill(int amount)
    {
        Vector3 pos;
        Collider2D[] enemy = EnemysPosition.Instance.GetEnemysPosition();
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
                pos = new Vector3(Random.Range(cam.position.x - 15, cam.position.x + 15), Random.Range(cam.position.y - 8, cam.position.y + 8), 0);
            }
            go = Instantiate(lightning, new Vector3(pos.x, pos.y + 20, 0), Quaternion.identity);
            script = go.GetComponent<Lightning>();
            script.Set(pos, damage);
        }
    }
}
