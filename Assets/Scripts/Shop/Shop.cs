using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public string name;
        public Sprite image;
        public int damage;
        public int bullet;
        public float reload;
        public int price;
        public bool isPurchased;
    }

    [SerializeField] private List<ShopItem> shopItemsList;

    GameObject ItemTemplate;
    GameObject go;
    [SerializeField] private Transform shopScrollView;
    void Start()
    {
        ItemTemplate = shopScrollView.GetChild(0).gameObject;

        int length = shopItemsList.Count;

        for(int i = 0; i < length; i++)
        {
            go = Instantiate(ItemTemplate, shopScrollView);
            go.transform.GetChild(0).GetComponent<Text>().text = shopItemsList[i].name;
            go.transform.GetChild(1).GetComponent<Image>().sprite = shopItemsList[i].image;
            go.transform.GetChild(2).GetComponent<Text>().text = shopItemsList[i].damage.ToString();
            go.transform.GetChild(3).GetComponent<Text>().text = shopItemsList[i].bullet.ToString();
            go.transform.GetChild(4).GetComponent<Text>().text = shopItemsList[i].reload.ToString();
            go.transform.GetChild(5).GetComponent<Button>().interactable = !shopItemsList[i].isPurchased;
            go.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = shopItemsList[i].price.ToString();
        }

        Destroy(ItemTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
