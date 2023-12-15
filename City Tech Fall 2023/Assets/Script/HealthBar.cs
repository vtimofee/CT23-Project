using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the health slider
    public float maxHealth = 100f; // Maximum health
    public float healthDecreaseRate = 1f; // Health decrease rate per second
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;
    public string gameOverSceneName = "DeathScene"; // Name of the Game Over scene

    private float currentHealth; // Current health

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        // Decrease health over time
        DecreaseHealth(Time.deltaTime * healthDecreaseRate);

        // Update the health bar
        UpdateHealthBar();
    }

    void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        // Ensure health doesn't go below 0
        currentHealth = Mathf.Max(0, currentHealth);

        // Check if health is zero and perform actions if needed
        if (currentHealth == 0)
        {
            HandleZeroHealth();
        }
    }

    void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        // Interpolate color between fullHealthColor and zeroHealthColor based on current health
        Color lerpedColor = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / maxHealth);

        // Apply the color to the Fill of the health slider
        healthSlider.fillRect.GetComponent<Image>().color = lerpedColor;
    }

    void HandleZeroHealth()
    {
        // Destroy the GameObject
        Destroy(gameObject);

        // Load the Game Over scene
        SceneManager.LoadScene(gameOverSceneName);
    }
}


