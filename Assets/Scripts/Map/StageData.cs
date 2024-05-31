using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newStageData",menuName = "Stage/Stage Data")]
public class StageData : ScriptableObject
{
    public Stage[] stages;

    public int GetStageLength() => stages.Length;

    public Stage GetStage(int i) => stages[i];
}
