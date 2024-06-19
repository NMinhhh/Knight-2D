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


    #region Skill

    [Header("Energy")]
    [SerializeField] private int energy;
    [Space]
    [Space]

    [Header("Skill Data")]
    [SerializeField] private SkillData skillData;
    [Space]

    [Header("Skill Info")]
    [SerializeField] private WeaponRotationSkill weaponRotationSkill;
    [SerializeField] private BomerangSpawnSkill bomerangSpawnSkill;
    [SerializeField] private RocketSpawnSkill rocketSpawnSkill;
    [SerializeField] private LightningSpawnSkill lightningSpawnSkill;
    [SerializeField] private BulletSpawnSkill bulletSpawnSkill;
    [SerializeField] private ElectricSkill electricSkill;
    [SerializeField] private MeteorSpawnSkill meteorSpawnSkill;
    [SerializeField] private GunShootingSkill gunShootingSkill;
    [SerializeField] private SlowingSkill slowingSkill;
    [SerializeField] private LavaSkill lavaSkill;
    [SerializeField] private LaserSkill laserSkill;
    [SerializeField] private DiceSkill diceSkill;
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

    #endregion

    #region Item

    [Space]
    [Space]
    [Header("Item Data")]
    [SerializeField] private ItemData itemData;
    [Space]
    [Space]

    [Header("Generate Item")]
    [SerializeField] private GameObject itemTemplate;
    [SerializeField] private Transform contentItem;
    [Space]
    [Space]

    [Header("Item Info")]
    [SerializeField] private Player player;
    [SerializeField] private Transform contentGun;
    private ReloadBullets reloadBullets;
    private PlayerShooting playerShooting;
    private bool isGetScript;


    private int itemPoint;
    private int selectedIdItem;
    private bool isChooseItem;
    private Item itemUse;

    #endregion

    void Start()
    {
        MenuSkillUI.Instance.SetInfoItem("");
        skillData.ResetLevelSkill();
        anim = GetComponent<Animator>();
        GetId();
        GenerateItemUI();
        randomIdSkill = new List<int>();
        isAllSkillFullLevel = false;
    }

    private void Update()
    {
        MenuSkillUI.Instance.SetEnergyPriceChange(energy);
    }

    #region Skill Function

    public void DecreasePrice()
    {
        energy += 5;
    }

    public void ResetEnergy()
    {
        energy = 5;
    }

    public void AppearSkill()
    {
        anim.SetTrigger("appear");
    }

    public void DisappearSkills()
    {
        anim.SetTrigger("reset");
    }

    void GetId()
    {
        idSkill = new List<int>();
        int count = skillData.GetSkillLength();
        for (int i = 0; i < count; i++)
        {
            idSkill.Add(i);
        }
    }

    public void ChangeSkill()
    {
        if (MapManager.Instance.HasEnoughEnergy(energy))
        {
            MapManager.Instance.UseEnergy(energy);
            DecreasePrice();
            int count = contentSkill.childCount;
            for (int j = count - 1; j >= 0; j--)
            {
                Destroy(contentSkill.GetChild(j).transform.gameObject);
            }
            anim.SetTrigger("disappear");
            AppearSkill();
        }
        else
        {
            return;
        }
    }

    public void AppearMenuSkills()
    {
        choosePointCur = 0;
        isChooseSkill = false;
        randomIdSkill.Clear();
        randomIdSkill.AddRange(idSkill);
        for (int i = 0; i < 3; i++)
        {
            int id;
            int choosePoint = i;
            if (idSkill.Count < 3)
            {
                id = randomIdSkill[Random.Range(0, randomIdSkill.Count)];
            }
            else
            {
                id = randomIdSkill[Random.Range(0, randomIdSkill.Count)];
                randomIdSkill.RemoveAt(randomIdSkill.IndexOf(id));
            }
            Skill skill = skillData.GetSkill(id);
            SkillItemUI weaponItemUI = Instantiate(itemSkillTemplate, contentSkill).GetComponent<SkillItemUI>();
            weaponItemUI.SetNameText(skill.skillName.ToString());
            weaponItemUI.SetLevelImage(skill.level);
            weaponItemUI.SetImage(skill.image, skill.sizeImage);
            weaponItemUI.SetInfoSkill(skill.info, skill.index);
            weaponItemUI.SetIdSkill(id);
            weaponItemUI.OnClickSelectionSkill(choosePoint, ChooseSkill);
        }
    }

    void ChooseSkill(int i)
    {
        //Change skill UI
        SkillItemUI oldSkillItemUI = GetSkillItemUI(choosePointCur);
        SkillItemUI newSkillItemUI = GetSkillItemUI(i);
        oldSkillItemUI.UnChooseSkill();
        newSkillItemUI.ChooseSkill();
        selectedIdSkill = newSkillItemUI.GetIdSkill();
        selectedSkill = skillData.GetSkill(selectedIdSkill);
        choosePointCur = i;
        isChooseSkill = true;
    }

    SkillItemUI GetSkillItemUI(int i)
    {
        return contentSkill.GetChild(i).GetComponent<SkillItemUI>();
    }

    public void Selection()
    {
        if (!isChooseSkill)
        {
            Debug.Log("Chon ky nang di ban oi!!!");   
            return;
        }
        isChooseSkill = false;
        switch (selectedSkill.skillName)
        {
            case Skill.Name.Shuriken:
                weaponRotationSkill.LevelUp(skillData.GetSkill(selectedIdItem).level);
                skillData.LevelUp(selectedIdSkill);
                break; 
            case Skill.Name.Bomerang:
                bomerangSpawnSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Roket:
                rocketSpawnSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Lightning:
                lightningSpawnSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Shooting:
                bulletSpawnSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Electric:
                electricSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Meteor:
                meteorSpawnSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Gun:
                gunShootingSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Slow:
                slowingSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Lava:
                lavaSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Laser:
                laserSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;
            case Skill.Name.Dice:
                diceSkill.LevelUp(skillData.GetSkill(selectedIdSkill).level);
                skillData.LevelUp(selectedIdSkill);
                break;

        }
        //Check skill is full level
        CheckSkillFullLevel(selectedIdSkill);
        //Delete card ckill
        int count = contentSkill.childCount;
        for (int j = count - 1; j >= 0; j--)
        {
            Destroy(contentSkill.GetChild(j).transform.gameObject);
        }
        //Close menu skill
        MenuSkillUI.Instance.CloseMenuSkill();
        anim.SetTrigger("disappear");
    }

    void CheckSkillFullLevel(int idSkill)
    {
        if (skillData.GetSkill(idSkill).level > skillData.GetSkill(idSkill).maxLevel)
        {
            this.idSkill.Remove(idSkill);
            if (this.idSkill.Count == 0)
            {
                isAllSkillFullLevel = true;
            }
        }
    }

    #endregion

    #region Item Function

    public void ResetItemUI()
    {
        GetComponent();
        for (int i = 0; i < itemData.GetItemsLength(); i++)
        {
            int id = i;
            ItemUI itemUI = GetItemUI(id);
            itemUI.ResetItem();
        }
        MenuSkillUI.Instance.SetInfoItem("");
        MenuSkillUI.Instance.ResetVerticelItem();
        selectedIdItem = 0;
        itemPoint = 0;
        isChooseItem = false;
        playerShooting.ResetWeaponDamage();
        reloadBullets.ResetBullet();
        player.ResetPlayer();
        ResetEnergy();
    }

    void GenerateItemUI()
    {
        for (int i = 0; i < itemData.GetItemsLength(); i++)
        {
            int id = i;
            Item item = itemData.GetItem(id);
            ItemUI itemUI = Instantiate(itemTemplate, contentItem).GetComponent<ItemUI>();
            itemUI.SetImage(item.image);
            itemUI.SetPrice(item.price.ToString());
            itemUI.SetInfoItem(item.info);
            itemUI.OnClickSelectedItem(id, ChooseItem);
        }
    }

    void ChooseItem(int id)
    {
        isChooseItem = true;
        ItemUI oldItemUI = GetItemUI(itemPoint);
        ItemUI newItemUI = GetItemUI(id);
        oldItemUI.UnChooseItem();
        newItemUI.ChooseItem();
        MenuSkillUI.Instance.SetInfoItem(newItemUI.GetInfoText());
        selectedIdItem = id;
        itemPoint = id;
        itemUse = itemData.GetItem(id);
    }

    public void BuyItem()
    {
        if (!isChooseItem)
        {
            Debug.Log("Chon item de mua ban oi");
            return;
        }
        ItemUI itemUI = GetItemUI(selectedIdItem);
        if (MapManager.Instance.HasEnoughEnergy(itemData.GetItem(selectedIdItem).price))
        {
            itemUI.ItemPurchased();
            MapManager.Instance.UseEnergy(itemData.GetItem(selectedIdItem).price);
            isChooseItem = false;
            UseItem(itemUse);
        }
        else
        {
            Debug.Log("Khong du nang luong!!!");
        }
    }

    void GetComponent()
    {
        int count = contentGun.childCount;
        for (int i = 0; i < count; i++) 
        {
            GameObject go = contentGun.GetChild(i).gameObject;
            if (go.activeInHierarchy)
            {
                playerShooting = go.GetComponent<PlayerShooting>();
                reloadBullets = go.GetComponent<ReloadBullets>();
            }
        }
    }

    public void UseItem(Item item)
    {
        switch (item.Type)
        {
            case Item.ItemType.Health:
                player.AddHealth(item.amount);
                break;
            case Item.ItemType.Damage:
                playerShooting.IncreaseDamage(item.amount);
                break;
            case Item.ItemType.Speed:
                player.IncreaseSpeed(item.amount);
                break;
            case Item.ItemType.Bullet:
                reloadBullets.IncreseaBullets((int)item.amount);
                break;
        }
    }

    public ItemUI GetItemUI(int i)
    {
        return contentItem.GetChild(i).GetComponent<ItemUI>();
    }
    #endregion
}
