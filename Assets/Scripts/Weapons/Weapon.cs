using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private Transform hitDetectionPos;
    [SerializeField] private float hitDetectionRadius;

    [SerializeField] private float range;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] private float aimLerp = 10f;
    void Start()
    {
        
    }

    void Update()
    {
        AutoAim();

        Attack();
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionPos.position, hitDetectionRadius, enemyLayer);

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void AutoAim()
    {
        Transform closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            Vector2 diff = (closestEnemy.position - transform.position).normalized;
            transform.up = Vector2.Lerp(transform.up, diff, Time.deltaTime * aimLerp);
        }
    }

    private Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        float minDistance = 5000;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();

            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked.transform;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectionPos.position, hitDetectionRadius);
    }
}
