using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float playerDetectionRange = 1.5f;
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        if(player == null)
        {
            Debug.LogWarning("No player found! destroying");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        FollowPlayer();
        TryAttack();
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
            Destroy(gameObject);
        }
    }
}
