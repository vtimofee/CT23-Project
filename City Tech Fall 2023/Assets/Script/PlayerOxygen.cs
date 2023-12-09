using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{


    //public Slider oxygenBar;
    public float oxygenAmount = 100.0f;
    private float currentOxygen;
    private bool isUnderwater = false;


    public delegate void OxygenEvent();
    public static event OxygenEvent OnNoOxygen;


    // Start is called before the first frame update
    void Start()
    {
        currentOxygen = oxygenAmount;
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        isUnderwater = false;
        currentOxygen -= Time.deltaTime;
       // oxygenBar.Value = currentOxygen/(float)oxygenAmount;


        if (currentOxygen <= 0)
        {
            currentOxygen = 0;

            if (OnNoOxygen != null)
            {
                OnNoOxygen();
            }
        }
    }

    void Update()
    {
       if(! isUnderwater)
        {
            //currentOxygen += Time.delta.Time;
            if(currentOxygen >= oxygenAmount)
            {
                currentOxygen = oxygenAmount;
            }
          //  oxygenBar.Value = currentOxygen / (float)oxygenAmount;

        }
        isUnderwater = true;
    }
}
