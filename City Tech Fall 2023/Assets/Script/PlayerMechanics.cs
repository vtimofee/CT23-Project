using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    public float maxhp;
    private float hp;
    private void OnEnable() => hp = maxhp;
    public float repairhps;
    private float repairtimer;
    private bool canrepair;
    public float repairtools;
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
        Debug.Log("Submarine down");
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
}