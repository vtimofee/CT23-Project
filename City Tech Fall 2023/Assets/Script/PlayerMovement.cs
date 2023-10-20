using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : AbstractPlayer
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
    public Terrain terrain;
    public bool crashed;
    public bool canCrash;
    public float crashdelay;

    private void Start()
    {
        submarine.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        hp = maxhp;
    }

    public void Update()
    {
        movePlayer();
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
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            canCrash = true;

        }
        if (crashed == true)
        {
            OnCrash();
            crashed = false;
            canCrash = false;
        }
    }
    private void verticalMovement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 vertical = new Vector3(0f, move.y, move.z);
            transform.Translate(vertical * verticalspeed * Time.deltaTime, Space.Self);
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
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector3>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.ReadValue<Vector2>();
    }
    public void crashSpeed()
    {
        if(mincrash < horizontalacceleration)
        {
            damagetaken = mincrash * horizontalspeed / 2;
        }
        else if(horizontalspeed >= maxspeed)
        {
            damagetaken = maxcrash * horizontalspeed / 2;
        }
        else
        {
            currentcrash = maxcrash * horizontalspeed / 2;
            damagetaken = currentcrash;
        }
    }
    
    public void OnCrash()
    {
        crashSpeed();
        damageCalculation();
        //Sound and etc.
    }

    public void damageCalculation()
    {
        if(damagetaken > maxdamagetaken)
        {
            damagetaken = maxdamagetaken;
        }
        else if (damagetaken < mindamagetaken)
        {
            damagetaken = mindamagetaken;
        }
        hp = -damagetaken;
        if(damagetaken > 0)
        {
            damagetaken = 0;
        }
    }
}
