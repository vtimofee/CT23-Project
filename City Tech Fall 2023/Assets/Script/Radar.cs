using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{

    public GameObject[] trackedNPCs;
    public GameObject[] trackedOxyTanks;
    public GameObject[] trackedPayload;
    List<GameObject> radarObjects;
    public GameObject radarPrefab;
    public GameObject oxygenPrefab;
    public GameObject payloadPrefab;
    List<GameObject> borderObjects;
    public float switchDistance;
    public Transform helpTransform;

    // Start is called before the first frame update
    void Start()
    {
        createRadarObjects();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < radarObjects.Count; i++)
        {
            if (Vector3.Distance(radarObjects[i].transform.position, transform.position) > switchDistance)
            {
                //switch to borderObjects
                helpTransform.LookAt(radarObjects[i].transform);
                borderObjects[i].transform.position = transform.position + switchDistance * helpTransform.forward;
                borderObjects[i].layer = LayerMask.NameToLayer("MiniMap");
                radarObjects[i].layer = LayerMask.NameToLayer("Invisible");
            }
            else
            {
                //switch back to radarObjects
                borderObjects[i].layer = LayerMask.NameToLayer("Invisible");
                radarObjects[i].layer = LayerMask.NameToLayer("MiniMap");
            }
        }
    }

    void createRadarObjects()
    {
        radarObjects = new List<GameObject>();
        borderObjects = new List<GameObject>();
        foreach (GameObject o in trackedNPCs)
        {
            GameObject k = Instantiate(radarPrefab, o.transform.position, Quaternion.identity) as GameObject;
            radarObjects.Add(k);
            GameObject j = Instantiate(radarPrefab, o.transform.position, Quaternion.identity) as GameObject;
            borderObjects.Add(j);
        }
        foreach (GameObject o in trackedOxyTanks)
        {
            GameObject k = Instantiate(oxygenPrefab, o.transform.position, Quaternion.identity) as GameObject;
            radarObjects.Add(k);
            GameObject j = Instantiate(oxygenPrefab, o.transform.position, Quaternion.identity) as GameObject;
            borderObjects.Add(j);
        }

        foreach (GameObject o in trackedPayload)
        {
            GameObject k = Instantiate(payloadPrefab, o.transform.position, Quaternion.identity) as GameObject;
            radarObjects.Add(k);
            GameObject j = Instantiate(payloadPrefab, o.transform.position, Quaternion.identity) as GameObject;
            borderObjects.Add(j);
        }
    }

}
