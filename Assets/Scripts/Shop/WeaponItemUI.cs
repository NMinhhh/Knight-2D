using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemUI : MonoBehaviour
{
    [SerializeField] private Text nameWeapon;
    [SerializeField] private Image image;
    [SerializeField] private Text damageText;
    [SerializeField] private Text bullets;
    [SerializeField] private Text reloadText;
    [SerializeField] private Button button;
    [SerializeField] private Text price;

    public void SetWeaponName(string name)
    {
        nameWeapon.text = name;
    }

    public void SetWeaponImage(Sprite image)
    {
        this.image.sprite = image;
    }

    public void SetWeaponDamage(string damageText)
    {
        this.damageText.text = damageText;
    }

    public void SetWeaponBullets(string bulletsText)
    {
        this.bullets.text = bulletsText;
    }

    public void SetWeaponReload(string reloadText)
    {
        this.reloadText.text = reloadText;
    }

    public void SetWeaponPrice(string priceText)
    {
        this.price.text = priceText;
    }

    public void SetWeaponUnlockButton()
    {
        button.interactable = false;
        price.text = "UNLOCK"; 
    }

    public void OnWeaponUnlockButton(int idx, Action<int> action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action(idx));
    }
}

