using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isPlayer = false;
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        if (isPlayer && healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (isPlayer && healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isPlayer)
        {
            Debug.Log("GAME OVER");
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(1);
            }
            Destroy(gameObject);
        }
    }
}