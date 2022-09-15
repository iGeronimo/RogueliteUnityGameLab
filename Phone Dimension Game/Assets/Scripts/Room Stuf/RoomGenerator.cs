using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;



public class RoomGenerator : MonoBehaviour
{





    // Start is called before the first frame update
    void Start()
    {
        portalScript.onPortalEnter += generateNewRoom;
    }

    private void OnDestroy()
    {
        portalScript.onPortalEnter -= generateNewRoom;
    }

    private void generateNewRoom()
    {
        float roomValue = UnityEngine.Random.value;
        Debug.Log($"Generated number: {roomValue}..");

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("enemyRoomMerge"))
        {
            switch (roomValue)
            {
                case < 0.1f: // 10% shop
                    SceneManager.LoadScene("Shop");
                    Debug.Log("generating shop..");
                    break;
                case < 0.2f: // 10% treasure
                    SceneManager.LoadScene("TreasureRoom");
                    Debug.Log("generating treasure room.."); break;
                case < 0.4f: // 20% elite enemy
                    SceneManager.LoadScene("EliteEnemyRoom");
                    Debug.Log("generating elite enemy room.."); break;
                case <= 1.0f: // 60% enemy
                    SceneManager.LoadScene("enemyRoom");
                    Debug.Log("generating enemy room.."); break;
            }
        }
        else
        {
            SceneManager.LoadScene("enemyRoomMerge");
        }
    }
}
