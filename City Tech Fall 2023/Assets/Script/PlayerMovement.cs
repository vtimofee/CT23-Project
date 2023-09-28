using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontalspeed;
    public int verticalspeed;
    private Vector3 move;
    private Vector3 jump = new Vector3(0f, 2f, 0f);

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector3>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    
    public void Update()
    {
        movePlayer();
        //jumpPlayer();
        verticalMovement();
        Debug.Log(Physics.gravity);
    }
    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.z);
        transform.Translate(movement * horizontalspeed * Time.deltaTime, Space.World);
    }
    
    /*public void jumpPlayer()
    {
        Vector2 jumping = new Vector2(0f, jump.y);
        transform.Translate(jumping * verticalspeed * Time.deltaTime, Space.World);
    }*/
    private void verticalMovement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0f, verticalspeed * Time.deltaTime, 0f);
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
