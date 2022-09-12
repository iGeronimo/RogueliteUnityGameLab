using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeDamage : MonoBehaviour
{

    public int damage = 0;
    public GameObject effect;

    private Transform effectSpawn;

    // Start is called before the first frame update
    void Start()
    {
        effectSpawn = transform.GetChild(0);
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
            GameObject effectIns = (GameObject)Instantiate(effect, effectSpawn.position, transform.rotation);
            Destroy(effectIns, 2f);
        }
    }
}
