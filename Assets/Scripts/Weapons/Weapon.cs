using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum EState
    {
        Idle,
        Attack
    }

    private EState state;
    [SerializeField] private Animator animator;
    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private Transform hitDetectionPos;
    [SerializeField] private BoxCollider2D hitCollider;
    [SerializeField] private float hitDetectionRadius;
    [SerializeField] private float attackInterval;
    private float attackTimer;
    private List<Enemy> damagedEnemies = new();

    [SerializeField] private float range;
    [SerializeField] LayerMask enemyLayer;

    void Start()
    {
        state = EState.Idle;
    }

    void Update()
    {

        switch (state)
        {
            case EState.Idle:
                AutoAim(out bool enemyFound);
                animator.speed = enemyFound ? 0f : 1f;
                break;

            case EState.Attack:
                Attacking();
                break;
        }

        CheckAndGiveDamageNearbyEnemies();
    }

    [ContextMenu("Start Attack")]
    private void StartAttack()
    {
        animator.Play("Attack");
        state = EState.Attack;
        damagedEnemies.Clear();

        animator.speed = 1f / attackInterval;
    }
    private void Attacking()
    {
        animator.speed = 1f;
        CheckAndGiveDamageNearbyEnemies();
    }
    //Animator
    private void StopAttack()
    {
        state = EState.Idle;
        damagedEnemies.Clear();
    }

    private void CheckAndGiveDamageNearbyEnemies()
    {
        IncreaseTimer();
        Collider2D[] enemies = Physics2D.OverlapBoxAll(hitDetectionPos.position, hitCollider.bounds.size,
                                                       hitDetectionPos.localEulerAngles.z,
                                                       enemyLayer);

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].TryGetComponent(out Enemy enemy))
            {
                if (damagedEnemies.Contains(enemy)) 
                    continue;

                enemy.TakeDamage(damage);
                damagedEnemies.Add(enemy);
            }
        }
    }

    private void AutoAim(out bool enemyFound)
    {
        Transform closestEnemy = GetClosestEnemy();

        if (closestEnemy != null)
        {
            Vector2 targetUpVector = (closestEnemy.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttack();
        }
        enemyFound = closestEnemy != null;
    }

    private void ManageAttack()
    {
        if(attackTimer > attackInterval)
        {
            attackTimer = 0f;
            state = EState.Attack;
            StartAttack();
        }
    }

    private void IncreaseTimer()
    {
        attackTimer += Time.deltaTime;
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
