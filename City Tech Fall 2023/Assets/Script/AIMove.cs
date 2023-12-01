using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    //declare variable for AIspawner manager script
    private AIspawn m_AIManager;

    //declare variables for moving and turning
    private bool m_hasTarget = false;
    private bool m_isTurning;

    //variable for the current waypoint
    private Vector3 m_wayPoint;
    private Vector3 m_lastWayPoint = new Vector3(0f,0f,0f);

    //used for animation speed
    private Animator m_animator;
    private float m_speed;

    private Collider m_collider;
    
    void Start()
    {
        //get the AISpawner from its parent
        m_AIManager = transform.parent.GetComponentInParent<AIspawn>();
        m_animator = GetComponent<Animator>();

        SetUpNPC();
    }

    void SetUpNPC()
    {
        float m_Scale = Random.Range(0f, 4f);
        transform.transform.localScale += new Vector3(m_Scale * 1.5f, m_Scale, m_Scale);

        if(transform.GetComponent<Collider>() != null && transform.GetComponent<Collider>().enabled == true)
        {
            m_collider = transform.GetComponent<Collider>();
        }
        else if (transform.GetComponentInChildren<Collider>() != null && transform.GetComponentInChildren<Collider>().enabled == true)
        {
            m_collider = transform.GetComponentInChildren<Collider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if we have not found a wp to move to
        //if we found a wp we need to move there
        if (!m_hasTarget)
        {
            m_hasTarget = CanFindTarget();
        }
        else
        {
            //make sure we rotate npc to face its wp
            RotateNPC(m_wayPoint, m_speed);
            //move the npc in a straight line toward wp
            transform.position = Vector3.MoveTowards(transform.position, m_wayPoint, m_speed * Time.deltaTime);

            //check if collided if yes then lose target and look for new wp
            CollidedNPC();
        }

        //if NPC reaches wp reset target
        if(transform.position == m_wayPoint)
        {
            m_hasTarget = false;
        }
    }

    void CollidedNPC()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z))
        {
            //if collider has hit a waypoint or registers itself ignore raycast hit
            if (hit.collider == m_collider | hit.collider.tag == "waypoint")
            {
                return;
            }
            //otherwise have a random chance that npc will change direction
            int randomNum = Random.Range(1, 100);
            if (randomNum < 40)
            {
                m_hasTarget = false;
                Debug.Log(hit.collider.transform.name + " " + hit.collider.transform.parent.position);            
            }
        }
    }

    //get wp
    Vector3 GetWayPoint(bool isRandom)
    {
        //if is random is true then get a random position location
        if (isRandom)
        {
            return m_AIManager.RandomPosition();
        }
        //otherwise get a random wp from the list of wp gameObjects
        else
        {
            return m_AIManager.RandomWayPoint();
        }
    }

    bool CanFindTarget(float start = 1f, float end = 7f)
    {
        m_wayPoint = m_AIManager.RandomWayPoint();
        //make sure we dont set the same waypoint twice
        if(m_lastWayPoint == m_wayPoint)
        {
            //get a new waypoint
            m_wayPoint = GetWayPoint(true);
            return false;
        }
        else
        {
            //set the new waypoint as the last waypoint 
            m_lastWayPoint = m_wayPoint;
            //get random speed for movement and animation
            m_speed = Random.Range(start, end);
            m_animator.speed = m_speed;
            //sets bool as true to say we found a WP
            return true;
        }
    }

    void RotateNPC(Vector3 waypoint, float currentSpeed)
    {
        //get random speed for turn
        float TurnSpeed = currentSpeed * Random.Range(1f, 3f);

        //get new direction to look for target
        Vector3 LookAt = waypoint - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), TurnSpeed * Time.deltaTime);
    }
}
