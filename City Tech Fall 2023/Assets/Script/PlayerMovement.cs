using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontalspeed;
    public float verticalspeed;
    private Vector3 move;
    private Vector3 jump = new Vector3(0f, 2f, 0f);
    public Rigidbody submarine;
    private float maxspeed = 5f;
    public float horizontalacceleration;
    private float minspeed = 0.5f;
    public Vector2 subrotation;
    public float subrotationspeedx;
    public float subrotationspeedy;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector3>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.ReadValue<Vector2>();
    }
    // Update is called once per frame

    private void Start()
    {
        submarine.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        movePlayer();
        //jumpPlayer();
        verticalMovement();
        //Debug.Log(Physics.gravity);
        subrotation.x += Input.GetAxis("Mouse X") * subrotationspeedx;
        subrotation.y += Input.GetAxis("Mouse Y") * subrotationspeedy;
        transform.localRotation = Quaternion.Euler(-subrotation.y, subrotation.x, 0f);
    }
    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.z);
        if(horizontalspeed < maxspeed && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            horizontalspeed += horizontalacceleration * Time.deltaTime;
            
        }
        else if(horizontalspeed > minspeed)
        {
            horizontalspeed -= horizontalacceleration * Time.deltaTime;
        }
        transform.Translate(movement * horizontalspeed * Time.deltaTime, Space.Self);
    }//Utilizing Space.Self in order to move the submarine in relation to its own rotation instead of Space.World
    
    /*public void jumpPlayer()
    {
        Vector2 jumping = new Vector2(0f, jump.y);
        transform.Translate(jumping * verticalspeed * Time.deltaTime, Space.World);
    }*/
    private void verticalMovement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 vertical = new Vector3(0f, move.y, move.z);
            transform.Translate(vertical * verticalspeed * Time.deltaTime, Space.Self);
            //submarine.velocity = transform.up * verticalspeed;
            //transform.position += new Vector3(0f, verticalspeed * Time.deltaTime, 0f);
            Physics.gravity = new Vector3(0f, 1f, 0f);
            //Creating a negative gravity force to create upwards movement to counteract drag.
            //Prevents gravity from continuing to drag submarine down and create acceleration.
        }else
        {
            Physics.gravity = new Vector3(0f, -1f, 0f);
            //Changes gravity back to default once we are no longer going up.
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftControl))
        {
            transform.position -= new Vector3(0f, verticalspeed * Time.deltaTime, 0f);
            //Moves down.
        }
    }
}
