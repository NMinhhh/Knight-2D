using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    [SerializeField] private WeaponObject weaponData;

    [SerializeField] private GameObject ItemTemplate;
    GameObject go;
    [SerializeField] private Transform shopScrollView;
    [SerializeField] private Animator anim;

    Button btnBuy;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        for(int i = 0; i < CoinManager.Instance.GetWeaponsLength(); i++)
        {
            int idx = i;
            Weapon weapon = CoinManager.Instance.GetWeapon(idx);
            WeaponItemUI weaponItemUI = Instantiate(ItemTemplate, shopScrollView).GetComponent<WeaponItemUI>();
            weaponItemUI.SetWeaponName(weapon.name);
            weaponItemUI.SetWeaponImage(weapon.image);
            weaponItemUI.SetWeaponDamage(weapon.damage.ToString());
            weaponItemUI.SetWeaponBullets(weapon.bullet.ToString());
            weaponItemUI.SetWeaponReload(weapon.reload.ToString());
           
            if (!weapon.isPurchased)
            {
                weaponItemUI.OnWeaponUnlockButton(idx, BuyItem);
                weaponItemUI.SetWeaponPrice(weapon.price.ToString());
            }
            else
            {
                if(idx == CoinManager.Instance.selectedWeaponIndex)
                {
                    weaponItemUI.OnWeaponEquipButton();
                }
                else
                {
                    weaponItemUI.OnWeaponUnEquipButton(idx, EquipWeapon);
                }
            }
        }
    }

    void BuyItem(int idx)
    {
        Weapon weapon = CoinManager.Instance.GetWeapon(idx);
        WeaponItemUI weaponItemUI = GetWeaponItemUI(idx);
        if (CoinManager.Instance.HasEnoughCoins(weapon.price))
        {
            //GameManager.Instance.GetWeaponsUnLock(i);
            CoinManager.Instance.UseCoins(weapon.price);
            CoinManager.Instance.WeaponPurchased(idx);
            EquipWeapon(idx);
        }
        else
        {
            anim.SetTrigger("noCoin");
        }

    }

    void EquipWeapon(int idx)
    {
        WeaponItemUI newWeaponItemUI = GetWeaponItemUI(idx);
        WeaponItemUI oldWeaponItemUI = GetWeaponItemUI(CoinManager.Instance.selectedWeaponIndex);
        newWeaponItemUI.OnWeaponEquipButton();
        oldWeaponItemUI.OnWeaponUnEquipButton(CoinManager.Instance.selectedWeaponIndex, EquipWeapon);
        CoinManager.Instance.ChangeWeapon(idx);
    }

    public WeaponItemUI GetWeaponItemUI(int i)
    {
        return shopScrollView.GetChild(i).GetComponent<WeaponItemUI>();
    }

    public void SetData()
    {
        CoinManager.Instance.SetWeaponData(weaponData);
    }


}
