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

    [SerializeField] private GameObject ItemTemplate;
    GameObject go;
    [SerializeField] private Transform shopScrollView;
    [SerializeField] private Animator anim;
    Button btnBuy;

    private void Awake()
    {
        for (int i = 0; i < data.Count; i++)
        {
            shopItemsList[i].name = data[i].name;
            shopItemsList[i].image = data[i].image;
            shopItemsList[i].damage = data[i].damage;
            shopItemsList[i].bullet = data[i].bullet;
            shopItemsList[i].reload = data[i].reload;
            shopItemsList[i].price = data[i].price;
            shopItemsList[i].isPurchased = data[i].isPurchased;
        }
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        
        int length = shopItemsList.Count;

        for(int i = 0; i < length; i++)
        {
            int idx = i;
            go = Instantiate(ItemTemplate, shopScrollView);
            go.transform.GetChild(0).GetComponent<Text>().text = shopItemsList[i].name;
            go.transform.GetChild(1).GetComponent<Image>().sprite = shopItemsList[i].image;
            go.transform.GetChild(2).GetComponent<Text>().text = shopItemsList[i].damage.ToString();
            go.transform.GetChild(3).GetComponent<Text>().text = shopItemsList[i].bullet.ToString();
            go.transform.GetChild(4).GetComponent<Text>().text = shopItemsList[i].reload.ToString();
            btnBuy = go.transform.GetChild(5).GetComponent<Button>();
            btnBuy.transform.GetChild(0).GetComponent<Text>().text = shopItemsList[i].isPurchased == false ? shopItemsList[i].price.ToString() : "UNLOCK";
            btnBuy.interactable =  !shopItemsList[i].isPurchased;
            btnBuy.onClick.AddListener(() => BuyItem(idx));
        }
    }

    void BuyItem(int i)
    {
        if (GameManager.Instance.HasEnoughCoins(shopItemsList[i].price))
        {
            GameManager.Instance.UseCoins(shopItemsList[i].price);
            shopItemsList[i].isPurchased = true;
            btnBuy = shopScrollView.GetChild(i).GetChild(5).GetComponent<Button>();
            btnBuy.interactable = false;
            btnBuy.transform.GetChild(0).GetComponent<Text>().text = "UNLOCK";
            Profile.Instance.UnlockGun(i);
            data[i].isPurchased = true;
        }
        else
        {
            anim.SetTrigger("noCoin");
        }

    }


}
