using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeDamage : MonoBehaviour
{

    public int damage = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<IDamageable>().takeDamage(damage);
        }
    }
}
