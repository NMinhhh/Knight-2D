using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBullets : MonoBehaviour
{
    Weapon weapon;

    public int amountOfBullet {  get; private set; }

    private int amountOfBulletMax;

    private float currentReloadTimer;

    [SerializeField] private StatsBullet statsBullet;

    void Start()
    {
        ResetBullet();
        currentReloadTimer = weapon.reload;
        statsBullet.reloadImage.sprite = weapon.image;
        statsBullet.amountOfBulletText.text = weapon.bullet.ToString();
        statsBullet.reloadImage.fillAmount = 0;
    }
    private void Update()
    {
        Reload();     
        statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
    }

    public void IncreseaBullets(int amount)
    {
        amountOfBullet = weapon.bullet + amount;
        amountOfBulletMax = amountOfBullet;
    }

    public void ResetBullet()
    {
        if(weapon == null)
            weapon = GameManager.Instance.GetWeaponSelected();
        amountOfBullet = weapon.bullet;
        amountOfBulletMax = amountOfBullet;
    }

    public void UpdateBullets()
    {
        amountOfBullet--;
    }


    void Reload()
    {
        if (amountOfBullet <= 0)
        {
            currentReloadTimer -= Time.deltaTime;
            statsBullet.reloadImage.fillAmount = currentReloadTimer / weapon.reload;
            if (currentReloadTimer <= 0)
            {
                amountOfBullet = amountOfBulletMax;
                statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
                currentReloadTimer = weapon.reload;
                statsBullet.reloadImage.fillAmount = 0;
            }
        }
    }
}
