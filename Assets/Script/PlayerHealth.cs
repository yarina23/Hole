using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")] // the player's maximum and current health
    public int maxHealth = 3;
    public int currentHealth;

    public Manager manager; // reference to the Manager script to handle game over UI

    [Header("Invulnerability Settings")]
    public float damageCooldown = 1.0f; // the cooldown after taking damage before the player can take damage again
    private float lastHitTime = -1.0f;
    private Renderer playerRenderer;
    public Color flashColor = Color.red;
    private Color originalColor;

    [Header("Fall Detection")]
    public float fallThresholdY = -2.0f; // whereby the player is considered to have fallen off the platform

    [Header("Restart Delay")]
    public float restartDelay = 1.0f; // seconds of delay before restarting the scene after death
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Player spawned with " + currentHealth + " lives."); //debug message for me and me only TEMPORARY 
        playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }
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

        if (playerRenderer != null)
        {
            StartCoroutine(FlashRoutine());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRoutine()
    {
        // Flash red and back 3 times
        for (int i = 0; i < 3; i++)
        {
            playerRenderer.material.color = flashColor;
            yield return new WaitForSeconds(0.1f);
            playerRenderer.material.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Die()
    {
        isDead = true;
        Debug.Log("Game Over!");

        if (manager != null)
        {
            manager.GameOver(); // Show Game Over UI
        }
    }

        IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
