using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newDiamondDatabase", menuName = "Shop/Diamond Data Base")]
public class DiamondData : ScriptableObject
{
    public Diamond[] diamonds;

    public int GetLength() => diamonds.Length;

    public Diamond GetDiamond(int i) => diamonds[i];

    public void SetSecondTimeLeft(int id,int secondTimeleft)
    {
        diamonds[id].secondTimeLeft = secondTimeleft;
    }

    public int GetSecondTimeLeft(int id) => diamonds[id].secondTimeLeft;

    public void ResetSecond()
    {
        for(int i = 0; i < diamonds.Length; i++)
        {
            SetSecondTimeLeft(i, 0);
        }
    }
}
