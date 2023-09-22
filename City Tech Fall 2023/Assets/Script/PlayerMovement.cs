using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int jumpheight;
    private Vector3 move;
    private Vector3 jump;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector3>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        movePlayer();
        jumpPlayer();
    }
    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.z);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
    
    public void jumpPlayer()
    {
        Vector2 jumping = new Vector2(0f, jump.y);
        transform.Translate(jumping * jumpheight * Time.deltaTime, Space.World);
    }
}
