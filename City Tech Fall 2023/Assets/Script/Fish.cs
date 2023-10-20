using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public List<GameObject> Fishies;
    public List<GameObject> BeegFish;
    public List<GameObject> THEBIGONE;
    public GameObject Fish1;
    public GameObject Fish2;
    public GameObject Fish3;
    public GameObject Fish4;
    public GameObject Fish5;

    void Start()
    {
        for (int i = 0; i < Fishies.Count; i++)
        {
            Debug.Log("fishspawn");
            Instantiate(Fishies[i], new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
    void Update()
    {

    }
}
