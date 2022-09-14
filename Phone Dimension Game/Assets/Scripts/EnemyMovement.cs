using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform boundary1;
    public Transform boundary2;

    [Range(0.0f, 10.0f)]
    public float speed;

    private Vector2 speedVector;
    private Vector2 movementVector;
    private Vector2 leftMovement;
    private Vector2 rightMovement;

    private int startDirection;

    private void Awake()
    {
        leftMovement = new Vector2(-speed, 0);
        rightMovement = new Vector2(speed, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        startDirection = Random.Range(1, 3);
        if(startDirection == 1)
        {
            speedVector = leftMovement;
        }
        else
        {
            speedVector = rightMovement;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (this.transform.position.x < boundary1.position.x)
        {
            speedVector = rightMovement;
        }
        if(this.transform.position.x > boundary2.position.x)
        {
            speedVector = leftMovement;
        }

        movementVector = speedVector * Time.deltaTime;

        this.transform.position = this.transform.position + new Vector3(movementVector.x, movementVector.y, 0);
    }
}
