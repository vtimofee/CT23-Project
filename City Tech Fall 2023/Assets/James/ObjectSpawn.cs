using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{

    public GameObject objectSpawn;
    public float delay = 1.0f;

    void Start()
    {
        Invoke("Spawn", delay);
    }

    // Update is called once per frame
    void Spawn()
    {
        Instantiate(objectSpawn, transform.position, Quaternion.identity);
        Invoke("Spawn", delay);
    }
}
