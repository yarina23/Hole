using UnityEngine;
using TMPro; // Required to control TextMeshPro UI !!!!!!!!

public class WallSpawner : MonoBehaviour
{
    [Header("1. Tutorial Setup (Spawns in exact order)")]
    public GameObject[] tutorialWalls;
    public TMP_Text tutorialTextUI; // Drag UI 
    public string[] tutorialMessages; // u can change teh messages in inspector

    [Header("2. Endless Walls (Spawns randomly after tutorial)")]
    public GameObject[] randomWalls;

    [Header("Timing & Position")]
    public float spawnInterval = 6.0f;
    private float timer = 0f;
    private int tutorialIndex = 0;

    void Start()
    {
        SpawnNextWall();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnNextWall();
            timer = 0f;
        }
    }

    void SpawnNextWall()
    {
        // tutorial Walls + UI Prompts
        if (tutorialIndex < tutorialWalls.Length)
        {
            if (tutorialWalls[tutorialIndex] != null)
            {
                Instantiate(tutorialWalls[tutorialIndex], transform.position, Quaternion.identity);
            }

            // next wall=next writing
            if (tutorialTextUI != null && tutorialIndex < tutorialMessages.Length)
            {
                tutorialTextUI.gameObject.SetActive(true);
                tutorialTextUI.text = tutorialMessages[tutorialIndex];
            }

            tutorialIndex++;
        }
        // arrayed walls + no tutorial text
        else
        {
            // Turn off the tutorial text once the 4th wall has passed
            if (tutorialTextUI != null && tutorialTextUI.gameObject.activeSelf)
            {
                tutorialTextUI.gameObject.SetActive(false);
            }

            if (randomWalls.Length > 0)
            {
                int randomIndex = Random.Range(0, randomWalls.Length);
                if (randomWalls[randomIndex] != null)
                {
                    Instantiate(randomWalls[randomIndex], transform.position, Quaternion.identity);
                }
            }
        }
    }
}