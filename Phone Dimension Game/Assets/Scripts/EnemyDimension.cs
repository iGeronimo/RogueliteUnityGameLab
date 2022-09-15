using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDimension : MonoBehaviour
{
    public int enemyDimension;
    public GameObject DimensionManager;
    private ChangeDimension _dimensionManager;
    private int _currentDimension;
    private int _lastDimension;

    private EnemyHealth _enemyHealth;


    void Start()
    {
        _dimensionManager = DimensionManager.GetComponent<ChangeDimension>();
        _currentDimension = _dimensionManager.currentDimension;
        _enemyHealth = this.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        checkCurrentDimension();
        canBeAttacked();
    }

    void checkCurrentDimension()
    {
        if (_currentDimension != _dimensionManager.currentDimension)
        {
            _currentDimension = _dimensionManager.currentDimension;
        }
    }

    void canBeAttacked()
    {
        if(_lastDimension != _currentDimension)
        {
            _lastDimension = _currentDimension;
            if(_currentDimension == enemyDimension || _currentDimension == 0)
            {
                _enemyHealth.Attackable = true;
                Debug.Log("An enemy can be attacked");
            }
            else
            {
                _enemyHealth.Attackable = false;
                Debug.Log("An enemy is invinsible!");
            }
        }
    }
}
