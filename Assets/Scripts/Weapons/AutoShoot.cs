using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AutoShoot 
{
    public static bool isAutoShoot;

    public static bool isLockHandle;

    public static GameObject nearestObj;
    
    public static void SetAutoShoot(bool state)
    {
        isAutoShoot = state;
    }

    public static void SetNearestObj(GameObject go)
    {
        nearestObj = go;
    }

    public static void SetIsLockHandle(bool state)
    {
        isLockHandle = state;
    }
}
