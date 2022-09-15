using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject dimensionControl;
    private ChangeDimension dimensionCheck;

    private SphereCollider bulletCollider;

    private Material bulletMaterial;
    private Color redBullet = Color.red;
    private Color whiteBullet = Color.white;
    private Color greenBullet = Color.green;

    public GameObject[] angleBoundaries;
    private Transform angleBoundary1;
    private Transform angleBoundary2;

    private Vector3 forwardsMovement;
    public float bulletSpeed = 5;

    private Rigidbody rb;


    private void Awake()
    {
        forwardsMovement = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        transform.position += forwardsMovement;
    }


    // Start is called before the first frame update
    void Start()
    {
        bulletMaterial = gameObject.GetComponent<Renderer>().material;
        rb = this.GetComponent<Rigidbody>();
        dimensionControl = GameObject.FindGameObjectWithTag("DimensionController");
        angleBoundaries = GameObject.FindGameObjectsWithTag("ShootingBoundary");
        //findAngleBoundaries();
        bulletCollider = this.GetComponent<SphereCollider>();
        dimensionCheck = dimensionControl.GetComponent<ChangeDimension>();
        forwardsMovement = new Vector3(0, 0, bulletSpeed);
        setBulletDimension();
        setDirection();
        this.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        
        //hittable();
    }

    private void FixedUpdate()
    {
        bulletMovement();
    }

    void findAngleBoundaries()
    {
        if(angleBoundaries[0].transform.position.x < angleBoundaries[1].transform.position.x)
        {
            angleBoundary1 = angleBoundaries[0].transform;
            angleBoundary2 = angleBoundaries[1].transform;
        }
        else
        {
            angleBoundary1 = angleBoundaries[1].transform;
            angleBoundary2 = angleBoundaries[0].transform;
        }
    }

    void bulletMovement()
    {
        rb.MovePosition(rb.position + (forwardsMovement * Time.deltaTime) * bulletSpeed);
    }

    void setDirection()
    {
        forwardsMovement = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        // forwardsMovement = new Vector3(Random.Range(angleBoundary1.transform.position.x, angleBoundary2.transform.position.x), angleBoundary1.transform.position.y, angleBoundary1.transform.position.z)- this.transform.position;
    }

    void setBulletDimension()
    {
        int dimension = Random.Range(1, 4);
        switch(dimension)
        {
            case 1:
                bulletMaterial.color = redBullet;
                break;
            case 2:
                bulletMaterial.color = whiteBullet;
                break;
            case 3:
                bulletMaterial.color = greenBullet;
                break;
            default:
                break;
        }
    }

    void hittable()
    {
        if(bulletMaterial.color == redBullet)
        {
            if(dimensionCheck.currentDimension == 0)
            {
                bulletCollider.enabled = false;
            }
            else
            {
                bulletCollider.enabled = true;
            }
        }

        if (bulletMaterial.color == greenBullet)
        {
            if (dimensionCheck.currentDimension == 1)
            {
                bulletCollider.enabled = false;
            }
            else
            {
                bulletCollider.enabled = true;
            }
        }

        if (bulletMaterial.color == whiteBullet)
        {
            if (dimensionCheck.currentDimension == 2)
            {
                bulletCollider.enabled = false;
            }
            else
            {
                bulletCollider.enabled = true;
            }
        }
    }
}
