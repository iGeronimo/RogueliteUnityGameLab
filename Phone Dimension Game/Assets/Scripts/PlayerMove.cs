using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Rigidbody rb;

    private PlayerInput pI;

    private bool isMoving = false;

    public Vector2 direction;

    public Vector3 move;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pI = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            move = new Vector3(-direction.y, 0, direction.x);
            Movement(move);
        }
    }

    public void OnAction(InputAction.CallbackContext ctx)
    {
        if(ctx.action.phase == InputActionPhase.Performed)
        {
            if (direction != ctx.ReadValue<Vector2>())
            {
                direction = ctx.ReadValue<Vector2>();
            }

            isMoving = true;
        }
        else if (ctx.action.phase == InputActionPhase.Canceled)
        {
            isMoving = false;
            direction = Vector2.zero;
        }
    }

    public void Movement(Vector3 direc)
    {
       
        rb.MovePosition(rb.position + move * _speed * Time.fixedDeltaTime);
        transform.LookAt(move.normalized + transform.position);
    }
}


//             move = context.ReadValue<Vector2>() * speed * Time.fixedDeltaTime;
