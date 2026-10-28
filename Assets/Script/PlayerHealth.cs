using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")] // the player's maximum and current health
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Invulnerability Settings")]
    public float damageCooldown = 1.0f; // the cooldown after taking damage before the player can take damage again
    private float lastHitTime = -1.0f;

    [Header("Fall Detection")]
    public float fallThresholdY = -2.0f; // whereby the player is considered to have fallen off the platform

    [Header("Restart Delay")]
    public float restartDelay = 1.0f; // seconds of delay before restarting the scene after death
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Player spawned with " + currentHealth + " lives."); //debug message for me and me only TEMPORARY 
    }

    void Update()
    {
        // Check if player has fallen off the platform
        if (transform.position.y < fallThresholdY && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the Wall and damage cooldown has passed
        if (collision.gameObject.CompareTag("Wall") && !isDead)
        {
            if (Time.time >= lastHitTime + damageCooldown)
            {
                TakeDamage(1);
                lastHitTime = Time.time;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Ouch! Player hit. Lives remaining: " + currentHealth); //another debug for mw

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Game Over! Restarting level...");

        // Disable movement
        playerMovement movement = GetComponent<playerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }

        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
