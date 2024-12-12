using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    private EnemyMovement enemyMovement;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned = false;

    [Space]
    [Header(" Attack ")]
    [SerializeField] private float playerDetectionRange = 1.5f;
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequencyPerSec;
    private float attackCooldown;
    private float attackTimer;
    [Space]
    [SerializeField] GameObject explodeVfx;

    [Header("Health")]
    [SerializeField] private int maxHealth;
    [SerializeField] private TextMeshPro healthText;

    private int health;
    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();

        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("No player found! destroying");
            Destroy(gameObject);
        }

        enemyMovement.SetPlayer(player);
        
        attackCooldown = 1f / attackFrequencyPerSec;
        StartCoroutine(SpawnSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned) return;
        if (attackTimer >= attackCooldown)
        {
            TryAttack();
        }
        else
            Wait();
    }
    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void TryAttack()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < playerDetectionRange)
        {
            Attack();
            PassAway();
        }
    }

    private void Attack()
    {
        attackTimer = 0f;
        player.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);
        if(health <= 0)
        {
            PassAway();
        }
        healthText.text = health.ToString();
    }

    private void PassAway()
    {
        Instantiate(explodeVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator SpawnSequence()
    {

        spriteRenderer.enabled = false;
        spawnIndicator.enabled = true;

        yield return new WaitForSeconds(2f);

        spriteRenderer.enabled = true;
        spawnIndicator.enabled = false;
        hasSpawned = true;

        enemyMovement.SetMove(true);
    }

}
