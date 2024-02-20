using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBullets : MonoBehaviour
{
    [SerializeField] private WeaponObject data;

    public int amountOfBullet {  get; private set; }

    private float currentReloadTimer;

    [SerializeField] private StatsBullet statsBullet;

    void Start()
    {
        amountOfBullet = data.bullet;
        currentReloadTimer = data.reload;
        statsBullet.amountOfBulletText.text = data.bullet.ToString();
        statsBullet.reloadImage.fillAmount = 0;
    }
    private void Update()
    {
        Reload();
        if(amountOfBullet > 0)
        {
            statsBullet.reloadImage.fillAmount = 0;
        }       
        statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
    }

    public void UpdateBullets()
    {
        amountOfBullet--;
    }



    void Reload()
    {
        if(amountOfBullet <= 0)
        {
            currentReloadTimer -= Time.deltaTime;
            statsBullet.reloadImage.fillAmount = currentReloadTimer / data.reload;
            if (currentReloadTimer <= 0)
            {
                amountOfBullet = data.bullet;
                statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
                currentReloadTimer = data.reload;
                statsBullet.reloadImage.fillAmount = 0;
            }
        }
    }
}
