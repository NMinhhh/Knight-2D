using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRectWeapon;
    [SerializeField] private ScrollRect scrollRectCoin;
    [SerializeField] private ScrollRect scrollRectAvartar;

    public void ResetScrollRectItem()
    {
        scrollRectWeapon.horizontalNormalizedPosition = 0;
        scrollRectAvartar.horizontalNormalizedPosition = 0;
        scrollRectCoin.horizontalNormalizedPosition = 0;
    }
}
