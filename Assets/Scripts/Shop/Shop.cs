using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Weapon")]

    [SerializeField] private GameObject ItemTemplate;
    [SerializeField] private Transform shopScrollView;
    [SerializeField] private DetailWeaponUI detailWeaponUI;

    WeaponObject weaponData;

    [Header("Avatar")]
    [SerializeField] private GameObject avatarItemTemplate;
    [SerializeField] private Transform avatarContent;
    [SerializeField] private SelectedAvatar selectedAvatar;

    AvatarData avatarData;

    [Header("Coin")]
    [SerializeField] private GameObject coinItemTemplate;
    [SerializeField] private Transform coinCotent;

    CoinData coinData;

    [Header("Diamond")]
    [SerializeField] private GameObject diamondItemTemplate;
    [SerializeField] private Transform diamondContent;

    CoinData diamondData;

    void Start() 
    {
        GetData();
        GenerateAvatarItemUI();
        GenerateWeaponItemUI();
        GenerateCoinItemUI();
        GenerateDiamondItemUI();
    }

    void GetData()
    {
        weaponData = GameData.Instance.GetWeaponData();
        avatarData = GameData.Instance.GetAvatarData();
        coinData = GameData.Instance.GetCoinData();
        diamondData = GameData.Instance.GetDiamondData();
    }

    #region Weapon
    public void GenerateWeaponItemUI()
    {
        for (int i = 0; i < weaponData.GetWeaponsLength(); i++)
        {
            int idx = i;
            Weapon weapon = weaponData.GetWeapon(idx);
            WeaponItemUI weaponItemUI = Instantiate(ItemTemplate, shopScrollView).GetComponent<WeaponItemUI>();
            weaponItemUI.SetWeaponName(weapon.name);
            weaponItemUI.SetWeaponImage(weapon.image, weapon.widthImage);
            weaponItemUI.SetWeaponDamage(weapon.damage.ToString());
            weaponItemUI.SetWeaponBullets(weapon.bullet.ToString());
            weaponItemUI.SetWeaponReload(weapon.reload.ToString());
            weaponItemUI.OnClickDetailButton(idx, OpenDeatailWeaponUI);
            if (!weapon.isPurchased)
            {
                weaponItemUI.OnWeaponUnlockButton(idx, BuyWeaponItem);
                weaponItemUI.SetWeaponPrice(weapon.price.ToString());
            }
            else
            {
                if (idx == GameManager.Instance.selectedWeaponIndex)
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

    void OpenDeatailWeaponUI(int idx)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        Weapon weapon = weaponData.GetWeapon(idx);
        detailWeaponUI.SetNameText(weapon.name);
        detailWeaponUI.SetImage(weapon.image);
        detailWeaponUI.SetIndexText($"Damage: {weapon.damage},Bullet: {weapon.bullet},Reload: {weapon.reload},Speed: {weapon.movementSpeed},Health: {weapon.maxHealth}");
        detailWeaponUI.SetInfoText(weapon.info);
        detailWeaponUI.gameObject.SetActive(true);
    }


    public void CloseDetailWeaponUI(bool isPlaySound)
    {
        if(isPlaySound)
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        detailWeaponUI.gameObject.SetActive(false);
    }

    void BuyWeaponItem(int idx)
    {
        Weapon weapon = weaponData.GetWeapon(idx);
        if (GameManager.Instance.HasEnoughCoins(weapon.price))
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Buy);
            GameManager.Instance.UseCoins(weapon.price);
            GameManager.Instance.AddWeaponPurchasedIndex(idx);
            weaponData.WeaponPurchased(idx);
            EquipWeapon(idx);
        }
        else
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        }

    }

    void EquipWeapon(int idx)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        //Get newWeaponUI
        WeaponItemUI newWeaponItemUI = GetWeaponItemUI(idx);
        //Get oldWeaponUI
        WeaponItemUI oldWeaponItemUI = GetWeaponItemUI(GameManager.Instance.GetWeaponSelectedID());
        //New Weapon Equiped
        newWeaponItemUI.OnWeaponEquipButton();
        //Old Weapon UnEquiped
        oldWeaponItemUI.OnWeaponUnEquipButton(GameManager.Instance.GetWeaponSelectedID(), EquipWeapon);
        //Change ID Weapon
        GameManager.Instance.ChangeWeaponID(idx);
        //Set Weapon Selected
        GameManager.Instance.SetWeaponSelected(weaponData.GetWeapon(idx));
        SelectedWeaponUI.Instance.SelectedWeapon();
    }

    public WeaponItemUI GetWeaponItemUI(int i)
    {
        return shopScrollView.GetChild(i).GetComponent<WeaponItemUI>();
    }
    #endregion

    #region Avatar

    public void GenerateAvatarItemUI()
    {
        int count = avatarContent.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(avatarContent.GetChild(i).gameObject);
        }

        for (int i = 0; i < avatarData.GetAvatarLenth(); i++)
        {
            int idx = i;
            Avatar avatar = avatarData.GetAvatar(idx);
            if (!avatar.isPurchased)
            {
                AvatarItemUI avatarItemUI = Instantiate(avatarItemTemplate, avatarContent).GetComponent<AvatarItemUI>();
                avatarItemUI.SetID(avatar.id);
                avatarItemUI.SetImage(avatar.image);
                avatarItemUI.SetName(avatar.name);
                avatarItemUI.SetPrice(avatar.price.ToString());
                avatarItemUI.OnAvatarUnlock(idx, BuyAvatarItem);
            }
        }

        for (int i = 0; i < avatarData.GetAvatarLenth(); i++)
        {
            int idx = i;
            Avatar avatar = avatarData.GetAvatar(idx);
            if(avatar.isPurchased)
            {
                AvatarItemUI avatarItemUI = Instantiate(avatarItemTemplate, avatarContent).GetComponent<AvatarItemUI>();
                avatarItemUI.SetImage(avatar.image);
                avatarItemUI.SetName(avatar.name);
                avatarItemUI.AvatarPurchased();
            }
        }
    }
    
    void BuyAvatarItem(int idx)
    {
        Avatar avatar = avatarData.GetAvatar(idx);
        if (GameManager.Instance.HasEnoughCoins(avatar.price))
        {
            GameManager.Instance.UseCoins(avatar.price);
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Buy);
            avatarData.AvatarPurchased(idx);
            GameManager.Instance.AddAvatarPurchased(idx);
            int count = avatarContent.childCount;
            for (int i = 0; i < count; i++)
            {
                AvatarItemUI avatarItemUI = avatarContent.GetChild(i).GetComponent<AvatarItemUI>();
                if (avatarItemUI.id == idx)
                {
                    avatarItemUI.AvatarPurchased();
                }
            }
            GenerateAvatarItemUI();
            selectedAvatar.GenerateAvatarUI();
        }
        else
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        }
    }


    #endregion

    #region Coin

    void GenerateCoinItemUI()
    {
        for (int i = 0; i < coinData.GetLength(); i++)
        {
            int idx = i;
            Coin coin = coinData.GetCoin(idx);
            CoinItemUI coinItemUI = Instantiate(coinItemTemplate, coinCotent).GetComponent<CoinItemUI>();
            coinItemUI.SetImage(coin.image);
            coinItemUI.SetValue(coin.value);
            coinItemUI.SetPrice(coin.coinPrice.ToString());
            coinItemUI.OnButtonBuy(idx, BuyCoinItem);
        }
    }

    void BuyCoinItem(int idx)
    {
        Coin coin = coinData.GetCoin(idx);
        if (GameManager.Instance.HasEnenoughDiamond(coin.coinPrice))
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Buy);
            GameManager.Instance.UseDiamond(coin.coinPrice);
            GameManager.Instance.AddCoin(coin.value);
        }
        else
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        }
    }

    #endregion

    #region Diamond

    void GenerateDiamondItemUI()
    {
        for (int i = 0; i < diamondData.GetLength(); i++)
        {
            int idx = i;
            Coin diamond = diamondData.GetCoin(idx);
            CoinItemUI diamondItemUI = Instantiate(diamondItemTemplate, diamondContent).GetComponent<CoinItemUI>();
            diamondItemUI.SetImage(diamond.image);
            diamondItemUI.SetValue(diamond.value);
            diamondItemUI.SetPrice("$ " + diamond.diamindPrice);
            diamondItemUI.OnButtonBuy(idx, BuyDiamondItem);
        }
    }

    void BuyDiamondItem(int idx)
    {
        Coin diamond = diamondData.GetCoin(idx);
        if (GameManager.Instance.HasEnenoughDiamond(diamond.coinPrice))
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Buy);
            GameManager.Instance.UseDiamond(diamond.coinPrice);
            GameManager.Instance.AddCoin(diamond.value);
        }
        else
        {
            SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        }
    }

    #endregion
}
