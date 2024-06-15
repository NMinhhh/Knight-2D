using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance {  get; private set; }

    [SerializeField] private Transform player;

    Weapon weapon;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        transform.position = player.position;
    }

    public int CalculateSkillDamage(float levelupIndex, float levelupIndexPercent, int level)
    {
        if(weapon == null)
            weapon = GameManager.Instance.GetWeaponSelected();
        return Mathf.FloorToInt((weapon.damage + (levelupIndex * level)) * Mathf.Pow((1 + levelupIndexPercent / 100), level));
    }
}
