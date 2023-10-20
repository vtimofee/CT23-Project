using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour
{
    public float hp;
    public float maxhp;
    public float minhp;
    public float damagetaken;
    public float maxdamagetaken;
    public float mindamagetaken;
    public float mincrash;
    public float maxcrash;
    public float currentcrash;
    public float repairspeed;
}
