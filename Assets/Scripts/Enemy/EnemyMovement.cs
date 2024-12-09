using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private Player player;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float playerDetectionRange = 1.5f;
    [SerializeField] GameObject explodeVfx;
    private bool hasSpawned = false;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        if(player == null)
        {
            Debug.LogWarning("No player found! destroying");
            Destroy(gameObject);
        }

        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        if (!hasSpawned) return;
        FollowPlayer();
        TryAttack();            
    }

    private IEnumerator SpawnRoutine()
    {

        spriteRenderer.enabled = false;
        spawnIndicator.enabled = true;

        yield return new WaitForSeconds(2f);

        spriteRenderer.enabled = true;
        spawnIndicator.enabled = false;
        hasSpawned = true;
    }

    private void FollowPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        Vector3 targetPosition = transform.position + moveSpeed * Time.deltaTime * direction.normalized;

        transform.position = targetPosition;
    }

    private void TryAttack()
    {
        float distance = Vector2.Distance(transform.position , player.transform.position);
        if(distance < playerDetectionRange)
        {
            Instantiate(explodeVfx, transform.position , Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
