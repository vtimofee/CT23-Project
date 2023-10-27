using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    // Start is called before the first frame update
    public float oxygen_total;
    public float oxygen_Amnt;
    public int oxygen_level;
    void Start()
    {
        oxygen_total = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(oxygen_level)
        {
            case 1: oxygen_Amnt = .5f;
                break;
            case 2: oxygen_Amnt = 1f;
                break;
            case 3: oxygen_Amnt = 2f;
                break;

        }
    }

    //Oxygen starts replenishing in chamber, Oxygen depletes when hitting an enemy, when hitting a wall 


}
