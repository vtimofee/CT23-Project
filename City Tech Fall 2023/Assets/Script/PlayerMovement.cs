using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody), typeof(PlayerMechanics))]
public class PlayerMovement : AbstractPlayer
{
    private const int V = 0;

    // Start is called before the first frame update
    public float horizontalspeed;
    public float verticalspeed;
    private Vector3 move;
    private Vector3 jump = new Vector3(0f, 2f, 0f);
    private float maxspeed = 5f;
    public float horizontalacceleration;
    private float minspeed = 0.5f;
    private Vector2 subrotation;
    public float subrotationspeedx;
    public float subrotationspeedy;
    public bool canCrash;
    [Range(0f, 1f)] public float crashvelocity;
    public float minCrash;
    private float maxCrash = 200;
    public bool GodMode;
    private bool invulnerable;
    private Rigidbody rigidBody;
    private PlayerMechanics DamageMechanics;
    private float damageFactor;
    public Transform target;
    public float smooth = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public buttonScript PauseRotation;
    private Vector3 velocityreset;

    private void Awake()
    {
        PauseRotation = gameObject.GetComponent<buttonScript>();
        rigidBody = GetComponent<Rigidbody>();
        DamageMechanics = GetComponent<PlayerMechanics>(); 
    }

    public void Update()
    {
        movePlayer();
        verticalMovement();
        Debug.Log(rigidBody.velocity);
        //Debug.Log(Physics.gravity);
        if (PauseRotation.isPaused == false)
        {
            subrotation.x += Input.GetAxis("Mouse X") * subrotationspeedx;
            subrotation.y += Input.GetAxis("Mouse Y") * subrotationspeedy;
        }
        transform.localRotation = Quaternion.Euler(-subrotation.y, subrotation.x, 0f);
        /*if(rigidBody.velocity.magnitude > 0)
        {
            rigidBody.velocity = Vector3.zero;
        }*/
        /*if (PauseMenu.isPaused == false)
        {
            movePlayer();
            verticalMovement();
            //Debug.Log(Physics.gravity);
            subrotation.x += Input.GetAxis("Mouse X") * subrotationspeedx;
            subrotation.y += Input.GetAxis("Mouse Y") * subrotationspeedy;
            transform.localRotation = Quaternion.Euler(-subrotation.y, subrotation.x, 0f);
            if (GodMode == true)
            {
                invulnerable = true;
            }
            else if (invulnerable == true)
            {

            }
        }*/
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
        if (horizontalspeed < maxspeed && !(Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D)))
        {
            horizontalspeed = 0f;
        }

            transform.Translate(movement * horizontalspeed * Time.deltaTime, Space.Self);
        //rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Vector3 movetoRotation = rigidBody.position + transform.TransformDirection(Input.GetAxis("Horizontal")/20, 0, Input.GetAxis("Vertical")/20);
        //rigidBody.MovePosition(movetoRotation);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rigidBody.velocity = Vector3.zero;
        }

    }//Utilizing Space.Self in order to move the submarine in relation to its own rotation instead of Space.World
    public void OnCollisionEnter(Collision collision)
    {
        float damageFactor = rigidBody.velocity.magnitude / minCrash;
        if (collision.gameObject.tag == "Terrain")
        {
            canCrash = true;
            //Debug.Log("crashtrue");

        }
        if (canCrash == true)
        {
            //Debug.Log("oncrash");
            OnCrash();
            canCrash = false;
        }
        
    }
    private void verticalMovement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Vector3 movetoRotation = rigidBody.position + transform.TransformDirection(Input.GetAxis("Horizontal") / 20, 0, Input.GetAxis("Vertical") / 20);
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

        if (Input.GetKey(KeyCode.LeftControl))
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

    
    public void OnCrash()
    {
        //Debug.Log("Dmgcalc");
        damageFactor = rigidBody.velocity.magnitude / minCrash;
        damageCalculation();
        rigidBody.velocity = Vector3.zero;
        if (damageFactor > crashvelocity){
            //Debug.Log("BIG BONK");
        }else if(damageFactor < crashvelocity)
        {
            //Debug.Log("little bonk");
        }
        //Sound and etc.
    }

    public void damageCalculation()
    {
        //private float damageFactor = rigidBody.velocity.magnitude / minCrash;
        if (damageFactor > crashvelocity && !GodMode)
        {

            if(damageFactor > 0.2)
            {
                damageFactor = 0.2f;
            }
            Debug.Log("Damage factor"+damageFactor);
            Debug.Log("Crashvel"+crashvelocity);
            Debug.Log("rigidBodyvel"+rigidBody.velocity.magnitude);
            velocityreset = new Vector3(0, 0, 0);
            rigidBody.velocity = velocityreset;
            DamageMechanics.Damage(damage * damageFactor * 5);
        }
    }
}
