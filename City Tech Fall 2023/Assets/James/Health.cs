using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;
    public int healthAmount = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = healthAmount;
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        currentHealth--;
        healthBar.value = (float)currentHealth / (float)healthAmount;
    }
}
