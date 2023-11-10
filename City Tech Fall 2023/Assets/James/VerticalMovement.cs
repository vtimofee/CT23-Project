using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{

    public float speed = 50.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up*Input.GetAxis("Vertical") * speed * Time.deltaTime;
    }
}
