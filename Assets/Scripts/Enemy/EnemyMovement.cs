using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    [Header( " Movement ")]
    [SerializeField] private float moveSpeed = 4f;
    private bool canMove = true;
    void Update()
    {
        if(player != null)
            FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        Vector3 targetPosition = transform.position + moveSpeed * Time.deltaTime * direction.normalized;

        transform.position = targetPosition;
    }
    
    public void SetPlayer(Player player) => this.player = player;
    public void SetMove(bool move) => canMove = move;
}
