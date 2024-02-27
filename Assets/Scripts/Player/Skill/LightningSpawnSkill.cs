using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawnSkill : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    private GameObject go;
    private Lightning script;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    private float timer;
    private int amountLightning;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && amountLightning > 0)
        {
            timer = cooldown;
            SetSkill(amountLightning);
        }
    }

    public void SetAmount(int amount)
    {
        amountLightning = amount;
    }

    void SetSkill(int amount)
    {
        Vector3 pos;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < amount; i++)
        {
            if (enemy.Length > 0)
            {
                pos = enemy[Random.Range(0, enemy.Length)].transform.position;
            }
            else
            {
                pos = new Vector3(Random.Range(transform.position.x + 10, transform.position.x - 10), Random.Range(transform.position.y + 8, transform.position.y - 8), 0);
            }
            go = Instantiate(lightning, pos, Quaternion.identity);
            script = go.GetComponent<Lightning>();
            script.Set(damage);
        }
    }
}
