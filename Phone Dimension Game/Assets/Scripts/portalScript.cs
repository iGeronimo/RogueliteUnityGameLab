using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class portalScript : MonoBehaviour
{

    public static event Action onPortalEnter;

    public GameObject portalEffect;
    private BoxCollider trigger;


    private void Start()
    {
        RoomManager.onRoomCleared += activatePortal;
        trigger = GetComponent<BoxCollider>();
        trigger.enabled = false;
        portalEffect.SetActive(false);
    }

    private void OnDestroy()
    {
        RoomManager.onRoomCleared -= activatePortal;
    }


    private void activatePortal()
    {
        trigger.enabled = true;
        portalEffect.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("portal entered");
            onPortalEnter();
        }
    }
}
