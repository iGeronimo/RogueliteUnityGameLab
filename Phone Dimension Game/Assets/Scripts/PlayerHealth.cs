using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    private int _health = 100;

    public int Health
    {
        set { _health = value; }
        get { return _health;  }
    }



    public void takeDamage(int dmg)
    {
        _health -= dmg;
    }
}
