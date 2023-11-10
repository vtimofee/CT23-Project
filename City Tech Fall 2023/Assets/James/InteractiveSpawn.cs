using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Spawn/Spawn on Button")]

public class InteractiveSpawn : MonoBehaviour
{
    public GameObject objectToSpawn;
    public string button = "Fire1";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(button))
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }
}
