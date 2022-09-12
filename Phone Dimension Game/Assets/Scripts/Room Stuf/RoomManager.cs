using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    public enum RoomType { ENEMY, ELITE, SHOP, TREASURE }
    RoomType roomtype;

    public static event Action onRoomCleared;

    Transform enemyManager;
    [SerializeField] private int enemyCount = 0;



    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth.onEnemyDeath += checkEnemyRoomCon;
        checkRoom();
       
        if(roomtype == RoomType.ENEMY || roomtype == RoomType.ELITE)
        {
            enemyManager = GameObject.FindGameObjectWithTag("enemyManager").transform;
            enemyCount = enemyManager.childCount;
        }
    }

    private void OnDestroy()
    {
        EnemyHealth.onEnemyDeath -= checkEnemyRoomCon;

    }

    private void checkRoom()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "enemyRoom":
                roomtype = RoomType.ENEMY;
                break;
            case "EliteEnemyRoom":
                roomtype = RoomType.ELITE;
                break;
            case "Shop":
                roomtype = RoomType.SHOP;
                break;
            case "TreasureRoom":
                roomtype = RoomType.TREASURE;
                break;
        }
    }

    private void checkEnemyRoomCon()
    {
        Debug.Log("checkRoomCon");
        enemyCount--;
        if (enemyCount <= 0)
        {
            Debug.Log("roomCleared");
            onRoomCleared();
        }
    }
}
