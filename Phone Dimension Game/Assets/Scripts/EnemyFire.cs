using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletManager;
    private BulletManagement bulletManagement;

    [SerializeField]
    private float maxFirerate = 0.5f;
    [SerializeField]
    private float minFirerate = 2f;
    private float currentFirerate;
    // Start is called before the first frame update
    void Start()
    {
        bulletManagement = bulletManager.GetComponent<BulletManagement>();
        currentFirerate = Random.Range(minFirerate, maxFirerate);
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if(currentFirerate < 0)
        {
            currentFirerate = Random.Range(minFirerate, maxFirerate);
            bulletManagement.SpawnBullet(this.transform);
        }
        else
        {
            currentFirerate -= Time.deltaTime;
        }
    }
}
