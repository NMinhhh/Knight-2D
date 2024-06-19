using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill 
{
    public enum Name
    {
        Shuriken,
        Bomerang,
        Roket,
        Lightning,
        Shooting,
        Electric,
        Meteor,
        Gun,
        Slow,
        Lava,
        Laser,
        Dice
    }

    public Name skillName;

    public int level = 1;
    public int maxLevel = 4;
    public Sprite image;
    public Vector2 sizeImage;
    public string info;
    public string[] index;
}
