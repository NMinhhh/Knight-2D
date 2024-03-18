using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance;
    [Header("List Weapons")]
    public List<Weapon> shopItemsList;
    public List<WeaponObject> data;
    [SerializeField] private WeaponData weaponData;
    private Weapon weapon;

    public List<int> weaponsUnLock {  get; private set; }

    [SerializeField] private GameObject ItemTemplate;
    private WeaponItemUI weaponItemUI;
    GameObject go;
    [SerializeField] private Transform shopScrollView;
    [SerializeField] private Animator anim;

    Button btnBuy;

    private void Awake()
    {
        //for (int i = 0; i < data.Count; i++)
        //{
        //    shopItemsList[i].name = data[i].name;
        //    shopItemsList[i].image = data[i].image;
        //    shopItemsList[i].damage = data[i].damage;
        //    shopItemsList[i].bullet = data[i].bullet;
        //    shopItemsList[i].reload = data[i].reload;
        //    shopItemsList[i].price = data[i].price;
        //    shopItemsList[i].isPurchased = data[i].isPurchased;
        //}
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        
        int length = shopItemsList.Count;
        for(int i = 0; i < weaponData.GetLength(); i++)
        {
            int idx = i;
            weapon = weaponData.GetWeapon(idx);
            weaponItemUI = Instantiate(ItemTemplate, shopScrollView).GetComponent<WeaponItemUI>();
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
                weaponItemUI.SetWeaponUnlockButton();
            }
      

        }
    }


    void BuyItem(int i)
    {
        weapon = GetWeapon(i);
        if (CoinManager.Instance.HasEnoughCoins(weapon.price))
        {
            weapon.isPurchased = true;
            btnBuy = shopScrollView.GetChild(i).GetChild(5).GetComponent<Button>();
            btnBuy.interactable = false;
            btnBuy.transform.GetChild(0).GetComponent<Text>().text = "UNLOCK";
            //GameManager.Instance.GetWeaponsUnLock(i);
            CoinManager.Instance.UseCoins(weapon.price);
            //data[i].isPurchased = true;
        }
        else
        {
            anim.SetTrigger("noCoin");
        }

    }

    public Weapon GetWeapon(int i)
    {
        return weaponData.GetWeapon(i);
    }

}
