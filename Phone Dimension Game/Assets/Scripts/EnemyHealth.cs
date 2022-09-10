using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    private int _health = 0;
    [SerializeField] private Gradient colorgradient;

    public int Health
    {
        set { _health = value; }
        get { return _health; }
    }

    private void Start()
    {
        _health = maxHealth;
    }

    public void takeDamage(int dmg)
    {
        Debug.Log("TAKE DAMAGE");
        _health -= dmg;
        changeColor();
        if(_health < 0)
        {
            die();
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }


    private void changeColor()
    {
        GetComponent<MeshRenderer>().material.color = colorgradient.Evaluate((float)_health / maxHealth);
    }
}
