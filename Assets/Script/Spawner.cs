using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public List<GameObject> spawnObjects = new List<GameObject>();
    public float SpawnInterval = 2.0f;

    private float spawnCounter = 0.0f;
    private int numberOfObjects = 0;
    void Start()
    {
        numberOfObjects = spawnObjects.Count;
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter += Time.deltaTime;
        if(spawnCounter >= SpawnInterval)
        {
            spawnCounter = 0.0f;
            int randomIndex = Random.Range(0, numberOfObjects);
            Instantiate(spawnObjects[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
