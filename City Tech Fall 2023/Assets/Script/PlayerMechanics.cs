using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMechanics : MonoBehaviour
{
    public float maxhp;
    private float hp;
    public float repairhps;
    private float repairtimer;
    private bool canrepair;
    public float repairtools;
    private float oxygen;
    public float maxoxygen;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update()
    {
        Repair();
        
    }
    public void Damage(float damage)
    {
        hp -= damage;
        Debug.Log(hp+ " hp remaining");
        if (hp <= 0) Destroyed();
    }
    public void Destroyed()
    {
        SunkenScene();
        //Particle and etc.
    }
    public void Repair()
    {
        if (Input.GetKey("f") && canrepair == true)
        {
            Debug.Log(hp);
            hp = Mathf.Clamp(hp + repairhps, 0, maxhp);
            canrepair = false;
        }
        else if(Input.GetKey("f") && canrepair == !true) 
        {
            repairtimer += Time.deltaTime;
            if(repairtimer >= 1)
            {
                canrepair = true;
                repairtimer = 0;
            }
        }
    }
    public void Oxygen(float oxygenloss)
    {
        oxygen -= oxygenloss;
        if (oxygen < 0) Destroyed();
    }
    private void OnEnable()
    {
        hp = maxhp;
        oxygen = maxoxygen;
    }

    public void SunkenScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }
}