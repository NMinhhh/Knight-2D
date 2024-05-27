using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootingSkill : MonoBehaviour
{
    [Header("Gun Obj")]
    [SerializeField] private GameObject[] guns;
    [SerializeField] private GameObject bullet;

    [Header("Info")]
    [SerializeField] private float basicDamage;
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float timeLife;
    [SerializeField] private int amountOfBullet;

    [Range(1,10)]
    [SerializeField] private int amountOfBulletLevelUp;
    private int currentAmountOfBullet;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    private int level;

    void Update()
    {
        if (level == 0)
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

    public void LevelUp(int level)
    {
        this.level = level;
        damage = GameManager.Instance.Calculate(basicDamage, damageLevelUp, damageLevelUpPercent, this.level);
        guns[level - 1].SetActive(true);
        UpdateGun(this.level);
    }

    void UpdateGun(int level)
    {
        for (int i = 0; i < level; i++)
        {
            GunShooting script = guns[i].GetComponent<GunShooting>();
            script.CreateGun(damage, speed, timeLife, amountOfBullet, bullet);
        }
        Reload();
    }

    // Update is called once per frame
 

    void Reload()
    {
        for(int i = 0; i < level;  i++)
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
