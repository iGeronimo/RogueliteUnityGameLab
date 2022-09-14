using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Upgrade
{


    public enum UpgradeType { ATTACK, SHIELD, STAT }

    public UpgradeType upgradeType;

    public Attack Attack = new Attack();


    //Attack
    public int damage;

    //Shield
    public int defense;

    //Stat
    public int crit;
    public int hp;
    public float speed;

    public bool controllable;



}









