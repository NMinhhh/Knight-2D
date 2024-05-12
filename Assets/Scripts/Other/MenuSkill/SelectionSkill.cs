using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSkill : MonoBehaviour
{
    public static SelectionSkill Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private MenuSkillUI menuSkillUI;

    [Header(" Button Change")]
    [SerializeField] private int energy;
    [Space]
    [Space]

    [Header("List Skills")]
    [SerializeField] private SkillData skillData;
    private List<Skill> listSkill;
    [Space]

    [Header("List Info Skill")]
    [SerializeField] private WeaponRotationSkill weaponRotationSkill;
    [SerializeField] private BomerangSpawnSkill bomerangSpawnSkill;
    [SerializeField] private RocketSpawnSkill rocketSpawnSkill;
    [SerializeField] private LightningSpawnSkill lightningSpawnSkill;
    [SerializeField] private BulletSpawnSkill bulletSpawnSkill;
    [SerializeField] private ElectricSkill electricSkill;
    [SerializeField] private ProtectionSkill protectionSkill;
    [SerializeField] private MeteorSpawnSkill meteorSpawnSkill;
    [SerializeField] private GunShootingSkill gunShootingSkill;
    [SerializeField] private SlowingSkill slowingSkill;
    [SerializeField] private LavaSkill lavaSkill;
    [SerializeField] private LaserSkill laserSkill;
    [Space]

    [Header("Generate Skill")]
    [SerializeField] private GameObject itemSkillTemplate;
    [SerializeField] private Transform contentSkill;

    private List<int> idSkill;
    private List<int> randomIdSkill;

    private Skill selectedSkill;
    private int selectedIdSkill;
    private int choosePointCur;
    private bool isChooseSkill;

    public bool isAllSkillFullLevel {  get; private set; }

    private Animator anim;

    [Space]
    [Space]
    [Header("List Item")]
    [SerializeField] private List<Item> listItems;
    [Space]
    [Space]

    [Header("Generate Item")]
    [SerializeField] private GameObject itemTemplate;
    [SerializeField] private Transform contentItem;
    [Space]
    [Space]

    [Header("Info Item")]
    private int itemPoint;
    private int selectedIdItem;
    private bool isChooseItem;
    private Item itemUse;

    void Start()
    {
        menuSkillUI = GetComponent<MenuSkillUI>();
        menuSkillUI.SetInfoItem("");
        this.listSkill = new List<Skill>();
        this.listSkill.AddRange(skillData.skills);
        this.anim = GetComponent<Animator>();
        GetId();
        GenerateItemUI();
        this.randomIdSkill = new List<int>();
        this.isAllSkillFullLevel = false;
    }

    private void Update()
    {
        menuSkillUI.SetEnergyPriceChange(this.energy);
    }

    public void DecreasePrice()
    {
        this.energy += 5;
    }

    public void ResetEnergy()
    {
        this.energy = 5;
    }

    public void OpenMenuSkill()
    {
        this.anim.SetTrigger("appear");
    }

    public void DisappearSkills()
    {
        this.anim.SetTrigger("reset");
    }

    void GetId()
    {
        this.idSkill = new List<int>();
        int count = this.listSkill.Count;
        for (int i = 0; i < count; i++)
        {
            this.idSkill.Add(i);
        }
    }

    public void ChangeSkill()
    {
        if (GameManager.Instance.HasEnoughEnergy(this.energy))
        {
            GameManager.Instance.UseEnergy(this.energy);
            DecreasePrice();
            int count = this.contentSkill.childCount;
            for (int j = count - 1; j >= 0; j--)
            {
                Destroy(this.contentSkill.GetChild(j).transform.gameObject);
            }
            this.anim.SetTrigger("disappear");
            OpenMenuSkill();
        }
        else
        {
            return;
        }
    }

    public void AppearMenuSkills()
    {
        this.choosePointCur = 0;
        this.isChooseSkill = false;
        this.randomIdSkill.Clear();
        this.randomIdSkill.AddRange(this.idSkill);
        for (int i = 0; i < 3; i++)
        {
            int id;
            int choosePoint = i;
            if (this.idSkill.Count < 3)
            {
                id = randomIdSkill[Random.Range(0, this.randomIdSkill.Count)];
            }
            else
            {
                id = randomIdSkill[Random.Range(0, this.randomIdSkill.Count)];
                this.randomIdSkill.RemoveAt(this.randomIdSkill.IndexOf(id));
            }
            Skill skill = this.listSkill[id];
            SkillItemUI weaponItemUI = Instantiate(itemSkillTemplate, contentSkill).GetComponent<SkillItemUI>();
            weaponItemUI.SetNameText(skill.skillName.ToString());
            weaponItemUI.SetLevelImage(skill.level);
            weaponItemUI.SetImage(skill.image, skill.sizeImage);
            weaponItemUI.SetInfoText(skill.info);
            weaponItemUI.SetIndexText(skill.index);
            weaponItemUI.SetIdSkill(id);
            weaponItemUI.OnClickSelectionSkill(choosePoint, ChooseSkill);
        }
    }

    void ChooseSkill(int i)
    {
        //Change skill UI
        SkillItemUI oldSkillItemUI = GetSkillItemUI(this.choosePointCur);
        SkillItemUI newSkillItemUI = GetSkillItemUI(i);
        oldSkillItemUI.UnChooseSkill();
        newSkillItemUI.ChooseSkill();
        this.selectedIdSkill = newSkillItemUI.GetIdSkill();
        this.selectedSkill = this.listSkill[this.selectedIdSkill];
        this.choosePointCur = i;
        this.isChooseSkill = true;
    }

    SkillItemUI GetSkillItemUI(int i)
    {
        return this.contentSkill.GetChild(i).GetComponent<SkillItemUI>();
    }

    public void Selection()
    {
        if (!this.isChooseSkill)
        {
            Debug.Log("Chon ky nang di ban oi!!!");   
            return;
        }
        this.isChooseSkill = false;
        switch (this.selectedSkill.skillName)
        {
            case Skill.Name.Shuriken:
                this.weaponRotationSkill.SetSkill(this.listSkill[this.selectedIdItem].level);
                this.listSkill[this.selectedIdSkill].level++;
                break; 
            case Skill.Name.Bomerang:
                this.bomerangSpawnSkill.LevelUp(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Roket:
                this.rocketSpawnSkill.AddDirSkill(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Lighting:
                this.lightningSpawnSkill.LevelUp(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Shooting:
                this.bulletSpawnSkill.LevelUp();
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Electric:
                this.laserSkill.LevelUp(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Shield:
                this.protectionSkill.AddLevelShield(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Meteor:
                this.meteorSpawnSkill.AddDir(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Gun:
                this.gunShootingSkill.LevelUp(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Slow:
                this.slowingSkill.LevelUp();
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Lava:
                this.lavaSkill.LevelUp(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;
            case Skill.Name.Laser:
                this.laserSkill.LevelUp(this.listSkill[this.selectedIdSkill].level);
                this.listSkill[this.selectedIdSkill].level++;
                break;

        }
        //Check skill is full level
        CheckSkillFullLevel(this.selectedIdSkill);
        //Delete card ckill
        int count = contentSkill.childCount;
        for (int j = count - 1; j >= 0; j--)
        {
            Destroy(this.contentSkill.GetChild(j).transform.gameObject);
        }
        //Close menu skill
        MenuSkillUI.Instance.CloseMenuSkill();
        this.anim.SetTrigger("disappear");
        //Reset price energy change
        ResetEnergy();
        //Reset item state
        ResetItemUI();
    }

    void CheckSkillFullLevel(int idSkill)
    {
        if (this.listSkill[idSkill].level > this.listSkill[idSkill].maxLevel)
        {
            this.idSkill.Remove(idSkill);
            if (this.idSkill.Count == 0)
            {
                this.isAllSkillFullLevel = true;
            }
        }
    }

    #region Item

    void ResetItemUI()
    {
        for (int i = 0; i < this.listItems.Count; i++)
        {
            int id = i;
            ItemUI itemUI = GetItemUI(id);
            itemUI.ResetItem();
        }
        menuSkillUI.SetInfoItem("");
        this.selectedIdItem = 0;
        this.itemPoint = 0;
        this.isChooseItem = false;
    }

    void GenerateItemUI()
    {
        for (int i = 0; i < this.listItems.Count; i++)
        {
            int id = i;
            ItemUI itemUI = Instantiate(this.itemTemplate, this.contentItem).GetComponent<ItemUI>();
            itemUI.SetImage(this.listItems[id].image);
            itemUI.SetPrice(this.listItems[id].price.ToString());
            itemUI.SetInfoItem(this.listItems[id].info);
            itemUI.OnClickSelectedItem(id, ChooseItem);
        }
    }

    void ChooseItem(int id)
    {
        this.isChooseItem = true;
        ItemUI oldItemUI = GetItemUI(itemPoint);
        ItemUI newItemUI = GetItemUI(id);
        oldItemUI.UnChooseItem();
        newItemUI.ChooseItem();
        menuSkillUI.SetInfoItem(newItemUI.GetInfoText());
        this.selectedIdItem = id;
        this.itemPoint = id;
        this.itemUse = this.listItems[id];
    }

    public void BuyItem()
    {
        if (!this.isChooseItem)
        {
            Debug.Log("Chon item de mua ban oi");
            return;
        }
        ItemUI itemUI = GetItemUI(this.selectedIdItem);
        if (GameManager.Instance.HasEnoughEnergy(this.listItems[selectedIdItem].price))
        {
            itemUI.ItemPurchased();
            GameManager.Instance.UseEnergy(this.listItems[selectedIdItem].price);
            this.isChooseItem = false;
            UseItem(itemUse);
        }
        else
        {
            Debug.Log("Khong du nang luong!!!");
        }
    }

    public void UseItem(Item item)
    {
        switch (item.Type)
        {
            case Item.ItemType.Health:
                Debug.Log("You use health");
                break;
            case Item.ItemType.Damage:
                Debug.Log("You use damage");
                break;
            case Item.ItemType.Speed:
                Debug.Log("You use speed");
                break;
        }
    }

    public ItemUI GetItemUI(int i)
    {
        return contentItem.GetChild(i).GetComponent<ItemUI>();
    }
    #endregion
}
