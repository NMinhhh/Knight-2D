using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance;
    [Header("List Weapons")]
    public List<Weapon> shopItemsList;

    [SerializeField] private GameObject ItemTemplate;
    GameObject go;
    [SerializeField] private Transform shopScrollView;
    Button btnBuy;
    [Space]

    [Header("Coin")]
    [SerializeField] private Text coinText;
    private void Awake()
    {
         if(Instance == null)
            Instance = this;
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
            SetCoinUI();
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
            SetCoinUI();
            Profile.Instance.UnlockGun(i);
        }
        else
        {
            Debug.Log("You don't have enough coins!!");
        }

    }

    void SetCoinUI()
    {
        coinText.text = GameManager.Instance.coin.ToString();
    }
}
