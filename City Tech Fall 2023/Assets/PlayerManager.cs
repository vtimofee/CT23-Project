using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth -= Time.deltaTime;
        Debug.Log("player health : " + playerHealth);
        
    }
}
