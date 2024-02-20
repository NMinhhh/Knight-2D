using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public int coin;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void UseCoins(int amount)
    {
        coin -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return coin >= amount;
    }
}
