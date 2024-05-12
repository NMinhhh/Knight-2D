using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootingSkill : MonoBehaviour
{
    [Header("Info gun")]
    [SerializeField] private GameObject[] guns;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float timeLife;
    [SerializeField] private float amountOfBullet;
    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    [SerializeField] private int level;
    private float timer;
    private int amountOfGun;

    public void LevelUp(int level)
    {
        amountOfGun = level;
        guns[level - 1].SetActive(true);
        GunShooting script = guns[level - 1].GetComponent<GunShooting>();
        script.CreateGun(damage, speed, timeLife, amountOfBullet, bullet);
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfGun == 0)
            return;
        if (CheckReload())
        {
            timer += Time.deltaTime;
            if (timer > cooldown)
            {
                Reload();
                timer = 0;
            }
        }
       
    }

    void Reload()
    {
        for(int i = 0; i < amountOfGun;  i++)
        {
            GunShooting script = guns[i].GetComponent<GunShooting>();
            script.Reload();
        }
    }

    bool CheckReload()
    {
        GunShooting script = guns[0].GetComponent<GunShooting>();
        return script.isReload;
    }

}
