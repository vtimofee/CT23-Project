using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public Slider oxygenBar;
    public float oxygenAmount = 10.0f;
    private float currentOxygen;
    private bool isUnderwater = false;

    void Start()
    {
        currentOxygen = oxygenAmount;
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        isUnderwater = true;
        currentOxygen -= Time.deltaTime;
        oxygenBar.value = currentOxygen / oxygenAmount;
    }

    void Update()
    {
        if (!isUnderwater)
        {
            currentOxygen += Time.deltaTime;
            if (currentOxygen >= oxygenAmount)
            {
                currentOxygen = oxygenAmount;
            }
            oxygenBar.value = currentOxygen / oxygenAmount;
        }

        isUnderwater = false;
    }

}
