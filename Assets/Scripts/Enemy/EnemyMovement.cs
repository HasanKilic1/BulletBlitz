using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    [SerializeField] private float moveSpeed = 4f;
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
        Vector3 direction = player.transform.position - transform.position;
        Vector3 targetPosition = transform.position + moveSpeed * Time.deltaTime * direction.normalized;

        transform.position = targetPosition;
    }
}
