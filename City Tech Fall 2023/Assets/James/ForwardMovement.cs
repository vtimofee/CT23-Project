using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Movement/Move Forward")]

public class ForwardMovement : MonoBehaviour
{
    public float speed = 1.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
