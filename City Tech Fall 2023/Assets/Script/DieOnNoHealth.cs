using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnNoHealth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }



    void OnEnable()

    {
        PlayerHealth.OnNoHealth += OnNoHealth;

    }


    void OnDisable()
    {

        PlayerHealth.OnNoHealth -= OnNoHealth;

    }

    void OnNoHealth()
    {
      //  Destroy(gameobject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
