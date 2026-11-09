using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [Header("1. Tutorial Walls (Spawns in exact order)")]
    public GameObject[] tutorialWalls;

    [Header("2. Endless Walls (Spawns randomly after tutorial)")]
    public GameObject[] randomWalls;

    [Header("Timing & Position")]
    public float spawnInterval = 3.5f;
    private float timer = 0f;
    private int tutorialIndex = 0;

    void Start()
    {
        // spawn the very first tutorial wall immediately when the game starts
        SpawnNextWall();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnNextWall();
            timer = 0f; // reset the clock
        }
    }

    void SpawnNextWall()
    {
        //my 4 tutorial walls in the initial order
        if (tutorialIndex < tutorialWalls.Length)
        {
            if (tutorialWalls[tutorialIndex] != null)
            {
                Instantiate(tutorialWalls[tutorialIndex], transform.position, Quaternion.identity);
            }
            tutorialIndex++; // Move to the next tutorial wall in line
        }
        // the randomised walls after tut
        else if (randomWalls.Length > 0)
        {
            int randomIndex = Random.Range(0, randomWalls.Length);
            if (randomWalls[randomIndex] != null)
            {
                Instantiate(randomWalls[randomIndex], transform.position, Quaternion.identity);
            }
        }
    }
}