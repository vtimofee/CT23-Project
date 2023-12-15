using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawns : MonoBehaviour
{
    public List<GameObject> OxygenSpawner;
    public List<GameObject> ItemLocation;
    public List<Vector3> OxygenTankVectors;
    
    private void Awake()
    {
        oxygenSpawn();
    }
    void oxygenSpawn()
    {
        for (int i = 0; i < OxygenSpawner.Count; i++)
        {
            Vector3 OxygenTankVectors3 = ItemLocation[i].transform.position;
            Instantiate(OxygenSpawner[i], OxygenTankVectors3, Quaternion.identity);
        }
    }
}
