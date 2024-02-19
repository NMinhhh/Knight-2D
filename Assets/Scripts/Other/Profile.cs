using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public static Profile Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private List<Weapon> listWeapons;
    [SerializeField] private GameObject itemTemplate;
    [SerializeField] private Transform scrollView;
    private GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        SetWeapons();
    }

    void SetWeapons()
    {
        for(int i = 0; i < Shop.Instance.shopItemsList.Count; i++)
        {
            int idx = i;
            if (Shop.Instance.shopItemsList[i].isPurchased) {
                AddWeapons((Shop.Instance.shopItemsList[i]));
            }

            
        }
    }

    public void AddWeapons(Weapon w)
    {
        if(listWeapons == null)
            listWeapons = new List<Weapon>();
        Weapon weapon = new Weapon()
        {
            name = w.name,
            image = w.image,
            damage = w.damage,
            bullet = w.bullet,
            reload = w.reload,
            price = w.price,
            isPurchased = w.isPurchased,
        };
        listWeapons.Add(weapon);
        go = Instantiate(itemTemplate, scrollView);
        go.transform.GetChild(0).GetComponent<Text>().text = weapon.name;
        go.transform.GetChild(1).GetComponent<Image>().sprite = weapon.image;
        go.transform.GetChild(2).GetComponent<Text>().text = weapon.damage.ToString();
        go.transform.GetChild(3).GetComponent<Text>().text = weapon.bullet.ToString();
        go.transform.GetChild(4).GetComponent<Text>().text = weapon.reload.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
