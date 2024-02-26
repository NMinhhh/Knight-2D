using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSkill : MonoBehaviour
{
    public static SelectionSkill Instance {  get; private set; }

    

    [SerializeField] private List<Skill> listSkill;
    [SerializeField] private GameObject[] skill;

    [SerializeField] private GameObject itemTemplate;
    [SerializeField] private Transform scrollView;

    private List<int> idSkill;

    private List<GameObject> skilObj;

    private GameObject go;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetId();
    }

    

    void GetId()
    {
        idSkill = new List<int>();
        int count = listSkill.Count;
        for(int i = 0; i < count; i++)
        {
            idSkill.Add(i);
        }
    }

    public void AppearMenuSkills()
    {
        skilObj = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            int idx = idSkill[Random.Range(0, idSkill.Count)];
            go = Instantiate(itemTemplate, scrollView);
            go.transform.GetChild(0).GetComponent<Text>().text = listSkill[idx].name.ToUpper();
            go.transform.GetChild(1).GetComponent<Text>().text = "LV: " + listSkill[idx].level;
            go.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = listSkill[idx].image;
            go.transform.GetChild(3).GetComponent<Text>().text = listSkill[idx].content;
            go.transform.GetChild(4).GetComponent<Text>().text = "+ 1 " + listSkill[idx].name;
            go.transform.GetComponent<Button>().onClick.AddListener(() =>  Selection(idx));
            skilObj.Add(go);
        }
    }

    void Selection(int i)
    {
        switch (i)
        {
            case 0:
                WeaponRotationSkill weaponRotationSkill = skill[i].GetComponent<WeaponRotationSkill>();
                weaponRotationSkill.SetSkill(listSkill[i].level);
                listSkill[i].level++;
                break; 
            case 1:
                WaterBlastSpawnSkill waterBlastSpawnSkill = skill[i].GetComponent<WaterBlastSpawnSkill>();
                waterBlastSpawnSkill.AddDirSkill(listSkill[i].level);
                listSkill[i].level++;
                break;
            case 2:
                RocketSpawnSkill rocketSpawnSkill = skill[i].GetComponent<RocketSpawnSkill>();
                rocketSpawnSkill.AddDirSkill(listSkill[i].level);
                listSkill[i].level++;
                break;

        }
        if (listSkill[i].level > listSkill[i].maxLevel)
        {
            idSkill.Remove(i);
        }
        int count = scrollView.childCount;
        for (int j = count - 1; j >= 0; j--)
        {
            Destroy(scrollView.GetChild(j).transform.gameObject);
        }
        CanvasManager.Instance.CloseMenuSkill();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable]
    class Skill
    {
        public string name;
        public int level;
        public int maxLevel = 4;
        public Sprite image;
        public string content;
    }
}
