using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AIObjects
{
    //dfjkdfkjd
    public string AIGroupName { get { return m_aiGroupName; } }
    public GameObject objectPrefab { get { return m_prefab; } }
    public int maxAI { get { return m_maxAI; } }
    public int spawnRate { get { return m_spawnRate; } }
    public int spawnAmount { get { return m_maxSpawnAmount; } }
    public bool randomizeStats { get { return m_randomizeStats; } }
    public bool enableSpawner { get { return m_enableSpawner; } }

    //Serialize private variables 
    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    [Range(0f, 30f)]
    private int m_maxAI;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_spawnRate;
    [SerializeField]
    [Range(0f, 10f)]
    private int m_maxSpawnAmount;

    [Header("Main Settings")]
    [SerializeField]
    private bool m_randomizeStats;
    [SerializeField]
    private bool m_enableSpawner;

    public AIObjects(string Name, GameObject Prefab, int MaxAI, int SpawnRate, int SpawnAmount, bool RandomizeStats)
    {
        this.m_aiGroupName = Name;
        this.m_prefab = Prefab;
        this.m_maxAI = MaxAI;
        this.m_spawnRate = SpawnRate;
        this.m_maxSpawnAmount = SpawnAmount;
        this.m_randomizeStats = RandomizeStats;
    }

    public void setValues(int MaxAI, int SpawnRate, int SpawnAmount)
    {
        this.m_maxAI = MaxAI;
        this.m_spawnRate = SpawnRate;
        this.m_maxSpawnAmount = SpawnAmount;
    }

}

public class AIspawn : MonoBehaviour
{
    //Using a list because we dont know the size of it, array would need to set size first
    public List<Transform> Waypoints = new List<Transform>();

    public float spawnTimer { get { return m_SpawnTimer; } }
    public Vector3 spawnArea { get { return m_SpawnArea; } }
    [Header("Global Stats")]
    [Range(0f, 600f)]
    [SerializeField]
    private float m_SpawnTimer; //global value for how often we run the spawner
    [SerializeField]
    private Color m_SpawnColor = new Color(1.000f, 0.000f, 0.000f, 0.300f); //use the color for gizmo
    [SerializeField]
    private Vector3 m_SpawnArea = new Vector3(20f, 10f, 20f);

    //Create array from new class
    [Header("AI Group Settings")]
    public AIObjects[] AIObject = new AIObjects[5];
    void Start()
    {
        GetWayPoints ();
        RandomizeGroups ();
        createAIGroups();
        InvokeRepeating("SpawnNPC", 0.5F, spawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnNPC()
        
    {   //Loop thru AI groups
        for (int i = 0; i < AIObject.Count(); i++)
        {
            //check to make sure spawner is enabled
            if (AIObject[i].enableSpawner && AIObject[i].objectPrefab != null)
            {
                //make sure AI group doesnt have max NPCS
                GameObject tempGroup = GameObject.Find(AIObject[i].AIGroupName);
                if (tempGroup.GetComponentInChildren<Transform>().childCount < AIObject[i].maxAI)
                {
                    //Spawn random number of NPCS from 0 to MaxSpawnAmount
                    for (int y = 0; y < Random.Range(0,AIObject[i].spawnAmount); y++)
                    {
                        //get random rotation
                        Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);
                        //create spawner gameobject
                        GameObject tempSpawn;
                        tempSpawn = Instantiate(AIObject[i].objectPrefab, RandomPosition(), randomRotation);
                        //put spawned NPC as child of group
                        tempSpawn.transform.parent = tempGroup.transform;
                        //Add the AImove script and class to the new NPC
                        tempSpawn.AddComponent<AIMove>();
                    }
                }
            }

        }
    }

    //public method for Randfom position within the spawn area
    public Vector3 RandomPosition()
    {
        //get a random position within spawn area
        Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(-spawnArea.y, spawnArea.y),
                Random.Range(-spawnArea.z, spawnArea.z)
            );
        randomPosition = transform.TransformPoint(randomPosition * .5f);
        return randomPosition;
    }

    public Vector3 RandomWayPoint()
    {
        int randomWP = Random.Range(0,(Waypoints.Count-1));
        Vector3 randomWayPoint = Waypoints[randomWP].transform.position;
        return randomWayPoint;
    }

    //METHOD FOR PUTTING RANDOM VALUES IN THE AI GROUP SETTING
    void RandomizeGroups()
    {
        for (int i = 0; i < AIObject.Count(); i++)
        {
            if(AIObject[i].randomizeStats)
            {
                //Randomizing our set values which is maxAI, spawnRate, and spawnAmount
                //AIObject[i] = new AIObjects(AIObject[i].AIGroupName, AIObject[i].objectPrefab, Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10), AIObject[i].randomizeStats);
                AIObject[i].setValues(Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10));
            }
        }
    } 

    //METHOD FOR CREATING THE EMPTY WORLD OBJECT GROUPS
    void createAIGroups()
    {
        //Empty Game Object to keep our AI in
        GameObject AIGroupSpawn;
        for (int i = 0; i < AIObject.Count(); i++)
        {
            AIGroupSpawn = new GameObject(AIObject[i].AIGroupName);
            AIGroupSpawn.transform.parent = this.gameObject.transform;
        }
    }


    //METHOD FOR GETTING WAYPOINTS AND ADDING TO LIST
    void GetWayPoints()
    {
        //Looking through nested children
        Transform[] wpList = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < wpList.Length; i++)
        {
            if (wpList[i].tag == "waypoint")
            {
                //Adding to the list
                Waypoints.Add (wpList[i]);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = m_SpawnColor;
        Gizmos.DrawCube(transform.position, spawnArea);
    }
}
