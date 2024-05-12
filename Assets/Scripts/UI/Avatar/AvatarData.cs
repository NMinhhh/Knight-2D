using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAvatarData",menuName = "Avatar/Avatar Data")]
public class AvatarData : ScriptableObject
{
    public Avatar[] avatars;

    public int GetAvatarLenth()
    {
        return avatars.Length;
    }

    public Avatar GetAvatar(int id)
    {
        return avatars[id];
    }

    public void AvatarPurchased(int id)
    {
        avatars[id].isPurchased = true;
    }
}
