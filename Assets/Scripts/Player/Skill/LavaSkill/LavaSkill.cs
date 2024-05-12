using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSkill : MonoBehaviour
{
    [SerializeField] private GameObject lavaObj;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    [SerializeField] private float cooldown;
    [SerializeField] private Vector3[] localScaleLavaObjs;
    [SerializeField] private float[] sizeTakeDamage;


    [SerializeField] private float cooldownSpawn;
    private float timer;

    private GameObject go;
    private Lava script;

    private int level;

    void Start()
    {
    }

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
    }

    void SpawnLava()
    {
        go = Instantiate(lavaObj, transform.position, Quaternion.identity);
        go.transform.localScale = localScaleLavaObjs[level - 1];
        script = go.GetComponent<Lava>();
        script.CreateLava(damage, cooldown, timeLife, sizeTakeDamage[level - 1]);
    }
}
