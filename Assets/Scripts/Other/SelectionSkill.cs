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
    private List<int> randomIdSkill;

    public bool isAllSkillFullLevel {  get; private set; }

    private GameObject go;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetId();
        randomIdSkill = new List<int>();
        isAllSkillFullLevel = false;
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
    // 0 1 2 3
    // 1
    // 0 2 3
    public void AppearMenuSkills()
    {
        randomIdSkill.Clear();
        randomIdSkill.AddRange(idSkill);
        for (int i = 0; i < 3; i++)
        {
            int id;
            if (idSkill.Count <= 3)
            {
                id = randomIdSkill[Random.Range(0, randomIdSkill.Count)];
            }
            else
            {
                id = randomIdSkill[Random.Range(0, randomIdSkill.Count)];
                randomIdSkill.RemoveAt(randomIdSkill.IndexOf(id));
            }
            go = Instantiate(itemTemplate, scrollView);
            go.transform.GetChild(0).GetComponent<Text>().text = listSkill[id].name.ToUpper();
            go.transform.GetChild(1).GetComponent<Text>().text = "LV: " + listSkill[id].level;
            go.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = listSkill[id].image;
            go.transform.GetChild(3).GetComponent<Text>().text = listSkill[id].content;
            go.transform.GetChild(4).GetComponent<Image>().fillAmount = (float)((listSkill[id].level) / (float)(listSkill[id].maxLevel + 1));
            go.transform.GetChild(5).GetComponent<Text>().text = "+ 1 " + listSkill[id].name;
            go.transform.GetComponent<Button>().onClick.AddListener(() =>  Selection(id));
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
            case 3:
                LightningSpawnSkill lightningSpawnSkill = skill[i].GetComponent<LightningSpawnSkill>();
                lightningSpawnSkill.SetAmount(listSkill[i].level);
                listSkill[i].level++;
                break;
            case 4:
                BulletSpawnSkill bulletSpawnSkill = skill[i].GetComponent<BulletSpawnSkill>();
                bulletSpawnSkill.AddAmountPosSkill(listSkill[i].level);
                listSkill[i].level++;
                break;
            case 5:
                LightningSpawnSkill bombFire = skill[i].GetComponent<LightningSpawnSkill>();
                bombFire.SetAmount(listSkill[i].level);
                listSkill[i].level++;
                break; 
            case 6:
                LaserSkill laser = skill[i].GetComponent<LaserSkill>();
                laser.AddLaser(listSkill[i].level);
                listSkill[i].level++;
                break;
            case 7:
                ProtectionSkill pro = skill[i].GetComponent<ProtectionSkill>();
                pro.AddLevelShield(listSkill[i].level);
                listSkill[i].level++;
                break;
            case 8:
                MeteorSpawnSkill meteor = skill[i].GetComponent<MeteorSpawnSkill>();
                meteor.AddDir(listSkill[i].level);
                listSkill[i].level++;
                break;

        }
        if (listSkill[i].level > listSkill[i].maxLevel)
        {
            idSkill.Remove(i);
            if(idSkill.Count == 0) 
            {
                isAllSkillFullLevel = true;
            }
        }
        int count = scrollView.childCount;
        for (int j = count - 1; j >= 0; j--)
        {
            Destroy(scrollView.GetChild(j).transform.gameObject);
        }
        MenuSkillUI.Instance.CloseMenuSkill();

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
