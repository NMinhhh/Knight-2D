using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBullets : MonoBehaviour
{
    //[SerializeField] private WeaponObject weaponsData;
    Weapon weapon;

    public int amountOfBullet {  get; private set; }
    private int currentAmountOfBullet;

    private float currentReloadTimer;

    [SerializeField] private StatsBullet statsBullet;

    void Start()
    {
        weapon = CoinManager.Instance.GetWeaponSelected();
        statsBullet.weaponIcon.sprite = weapon.image;
        ResetBullet();
        currentReloadTimer = weapon.reload;
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
        currentAmountOfBullet = amountOfBullet;
    }

    public void ResetBullet()
    {
        amountOfBullet = weapon.bullet;
        currentAmountOfBullet = amountOfBullet;
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
                amountOfBullet = currentAmountOfBullet;
                statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
                currentReloadTimer = weapon.reload;
                statsBullet.reloadImage.fillAmount = 0;
            }
        }
    }
}
