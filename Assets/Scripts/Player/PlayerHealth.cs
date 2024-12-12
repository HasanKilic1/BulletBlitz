using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxHealth;
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI healthText;
    private int health;

    void Start()
    {
        health = maxHealth;    
        UpdateHealthBar();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);

        if (health <= 0)
        {
            Die();
            //SceneManager.LoadScene(0);
        }
        UpdateHealthBar();
    }

    private void Die()
    {
        Debug.Log("Player dead");
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = (float)health / maxHealth;
        healthText.text = health + " / " + maxHealth;
    }
}
