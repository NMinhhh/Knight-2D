using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private Image[] tabImages;
    [SerializeField] private Vector2[] tabIconSizeActive;
    [SerializeField] private Vector2[] tabIconSizeInActive;
    [SerializeField] private float posY;
    [SerializeField] private Sprite inActiveTab, activeTab;
    [SerializeField] private Vector2 sizeInActive, sizeActive;

    private int currentId = 0;
    [SerializeField] private bool isIconEffect; 
    public void SwitchTab(int id)
    {
        for(int i= 0; i< tabs.Length; i++)
        {
            tabImages[i].rectTransform.sizeDelta = sizeInActive;
            tabImages[i].sprite = inActiveTab;
            Button button = tabImages[i].GetComponent<Button>();
            button.interactable = true;
            tabs[i].SetActive(false);
            if (i == id)
            {
                tabs[id].SetActive(true);
                tabImages[i].rectTransform.sizeDelta = sizeActive;
                tabImages[i].sprite = activeTab;
                if(isIconEffect)
                    Effect(id);
                button.interactable = false;
            }

        }
    }

    void Effect(int id)
    {
        RectTransform currentRect =  tabImages[currentId].transform.GetChild(0).GetComponent<RectTransform>();
        currentRect.sizeDelta = tabIconSizeInActive[currentId];
        currentRect.anchoredPosition = Vector2.zero;

        GameObject currentTextObj = tabImages[currentId].transform.GetChild(1).gameObject;
        currentTextObj.SetActive(false);

        currentId = id;

        RectTransform newRect = tabImages[currentId].transform.GetChild(0).GetComponent<RectTransform>();
        newRect.sizeDelta = tabIconSizeActive[currentId];
        newRect.anchoredPosition = new Vector2(0, posY);

        GameObject newTextObj = tabImages[currentId].transform.GetChild(1).gameObject;
        newTextObj.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        SwitchTab(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
