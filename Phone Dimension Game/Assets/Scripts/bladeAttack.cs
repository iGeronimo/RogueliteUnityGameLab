using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeAttack : MonoBehaviour
{


   [SerializeField] private float Range = 3f;

    public LayerMask enemies;

    public int damage = 20;

    public int bladeCount = 3;
    public GameObject bladePrefab;
    public Transform bladeTransform;

   [SerializeField]private float _rotationSpeed = 10f;

    public GameObject[] blades;



    // Start is called before the first frame update
    void Start()
    {
        blades = new GameObject[bladeCount];
        bladeManager();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position, Range, enemies))
        {
           

        }
        moveBlade();
    }


    void bladeManager()
    {
        Vector3 beginDir = new Vector3(1, 0, 0);
        float angle = 360 / bladeCount; //the angle differnce based on how many blades you want to create
        for (int i = 0; i < bladeCount; i++)
        {
            Quaternion rotate = Quaternion.AngleAxis(angle * i, Vector3.up); //creates the rotational angle in quaternion 0*angle is the initial pos, then 1*angle the first actual displacement
            Vector3 rotateDir = rotate * beginDir;                             //adding the rotation to the point from where we start from
            GameObject newBlade = (GameObject)Instantiate(bladePrefab, bladeTransform.position, bladePrefab.transform.rotation, transform); //creates the blade
            //newBlade.transform.position = bladeTransform.position + rotateDir * 2; //adds the rotation into the transform position
            newBlade.transform.position = bladeTransform.position + beginDir * 2;
            newBlade.transform.RotateAround(transform.position, Vector3.up, angle * i);
            



            //Quaternion turn = Quaternion.LookRotation(-Vector3.right, -Vector3.up);
            //float LookAngle = Vector3.SignedAngle(newBlade.transform.position - transform.position, newBlade.transform.position + Vector3.right - newBlade.transform.position, Vector3.up);
            //transform.rotation = Quaternion.AngleAxis(LookAngle, Vector3.up);
            //newBlade.transform.rotation *= turn;
            //newBlade.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
            //newBlade.transform.LookAt(transform.position);

            newBlade.GetComponent<bladeDamage>().damage = damage;
            blades[i] = newBlade;
        }
    }



    // desired DIrection = bladeposition - this position
    // current direction = bladeposition*Vector3.right - bladeposition


    void moveBlade()
    {
        Debug.Log("attack!");
        transform.Rotate(Vector3.Lerp(new Vector3(0,0,0), new Vector3(0, -10 * _rotationSpeed, 0), Time.deltaTime));
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
