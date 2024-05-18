using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static Cinemachine.DocumentationSortingAttribute;

public class ProtectionSkill : MonoBehaviour
{
    [Header("Shield Obj")]
    [SerializeField] private GameObject[] shields;

    [Header("Rotation value and Color")]
    [SerializeField] private float valueRotationZ;
    [SerializeField] private Color[] colors;
    private float rotationZ;

    [Header("Flash after touch enemy")]
    [SerializeField] private float numberOfFlash;
    [SerializeField] private float timeFlash;
    [Space]
    [Space]

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    private bool isDamage;

    private GameObject shieldCur;
    private int levelShieldMax;
    private int levelShield;

    private SpriteRenderer spriteR;
    private Player player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        shieldCur = shields[0];
    }
    void Update()
    {
        if (!isDamage && levelShield < levelShieldMax)
        {
            Cooldown();
        }
    }

    private void FixedUpdate()
    {
        RotationZ();
    }

    public void LevelUp(int level)
    {
        if (level > 1)
        {
            shieldCur.SetActive(false);
        }
        levelShieldMax = level;
        levelShield = levelShieldMax;
        shieldCur = shields[level - 1];
        shieldCur.SetActive(true);
    }

    public void RotationZ()
    {
        rotationZ += valueRotationZ;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        if (rotationZ >= 360)
        {
            rotationZ = 0;
        }

    }
    
 
    void ChangeColor(GameObject go, Color color)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            spriteR = go.transform.GetChild(i).GetComponent<SpriteRenderer>();
            spriteR.color = color;
        }
    }

    void DamageShield()
    {
        if(!isDamage)
            StartCoroutine(Flash());
    }
    
    IEnumerator Flash()
    {
        isDamage = true;
        player.ProtectionSkillOn();
        for (int i = 0; i < numberOfFlash; i++)
        {
            ChangeColor(shieldCur, colors[1]);
            yield return new WaitForSeconds(timeFlash / (numberOfFlash * 2));
            ChangeColor(shieldCur, colors[0]);
            yield return new WaitForSeconds(timeFlash / (numberOfFlash * 2));
        }
        player.ProtectionSkillOff();
        ChangeShield();
        isDamage = false;
        timer = 0;
    }

    void ChangeShield()
    {
        shieldCur.SetActive(false);
        levelShield--;
        if(levelShield > 0)
        {
            shieldCur = shields[levelShield - 1];
        }
        else
        {
            return;
        }
        shieldCur.SetActive(true);
    }

    void Cooldown()
    {
        timer += Time.deltaTime;
        if(timer >= cooldown)
        {
            levelShield++;
            if(levelShield > 1)
            {
                shieldCur.SetActive(false);
            }
            shieldCur = shields[levelShield - 1];
            shieldCur.SetActive(true);
            timer = 0;
        }
    }

}
