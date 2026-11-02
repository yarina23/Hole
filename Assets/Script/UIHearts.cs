using UnityEngine;
using UnityEngine.UI;
public class UIHearts : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Sprite redHeart;
    public Sprite whiteHeart;

    public Image[] hearts;

    public PlayerHealth playerHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        health = playerHealth.currentHealth;
        maxHealth = playerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = redHeart;
            }
            else
            {
                hearts[i].sprite = whiteHeart;
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;

            }

        }
    }
}
