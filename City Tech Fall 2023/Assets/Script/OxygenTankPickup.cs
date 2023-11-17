using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("object contact with : " + other.name);
            this.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
