using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ProtectionSkill : MonoBehaviour
{
    [SerializeField] private List<Vector3> colorShield;
    [SerializeField] private GameObject shieldGo;

    [SerializeField] private float flashTimer;
    [SerializeField] private int numberOfFlash;

    Player player;

    [SerializeField] private float cooldown;
    private float timer;

    private int levelShiled;
    private int amountOfShield;

    private bool damageShield;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        spriteRenderer = shieldGo.GetComponent<SpriteRenderer>();
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfShield < levelShiled && !damageShield)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                amountOfShield++;
                if(amountOfShield == 1)
                {
                    shieldGo.SetActive(true);
                }
                SetShield(amountOfShield - 1);
                timer = cooldown;
            }

        }

    }

    //take damage shield protection player

    void DamageShield()
    {
        if(!damageShield)
        {
            amountOfShield--;
            StartCoroutine(Protection());
        }
    }
    // effect flash
    IEnumerator Protection()
    {
        damageShield = true;
        player.ProtectionSkillOn();
        for (int i = 0; i < numberOfFlash; i++)
        {
            spriteRenderer.color = new Color(.95f, .55f, .55f, 1);
            yield return new WaitForSeconds(flashTimer / (numberOfFlash * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashTimer / (numberOfFlash * 2));
        }
        timer = cooldown;
        player.ProtectionSkillOff();
        damageShield = false;
        if (amountOfShield == 0)
        {
            shieldGo.SetActive(false);
        }
        else
        {
            SetShield(amountOfShield - 1);
        }
   
    }
    // level shield
    public void AddLevelShield(int i)
    {
        if (!shieldGo.activeInHierarchy)
        {
            shieldGo.SetActive(true);
        }
        levelShiled = i;
        amountOfShield = i;
        SetShield(i - 1);
    }
    // set color shield
    void SetShield(int i)
    {
        spriteRenderer.color = new Color(colorShield[i].x, colorShield[i].y, colorShield[i].z, 0.5f);
    }
}
