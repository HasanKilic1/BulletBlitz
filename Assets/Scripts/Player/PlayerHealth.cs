using System;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxHealth;
    private int health;
    void Start()
    {
        health = maxHealth;    
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
        }
    }

    private void Die()
    {
        Debug.Log("Player dead");
    }
}
