using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public GameObject projectile;
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public float rotSpeed;
    public float timeBetweenAttacks;
    public float attackRange;
    public float deathDuration;
    public int attackDamage;
    public bool playerInAttackRange;
    
    
    bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Defines the attack range around the enemy.
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Makes the enemy always look at the player.
        transform.LookAt(player.position);

        if (playerInAttackRange) 
            AttackPlayer();
        if (!playerInAttackRange)
            ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Keeps the enemy from moving
        agent.SetDestination(transform.position);

        
        if(!alreadyAttacked)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Attack>().damage = attackDamage;
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
            Invoke(nameof(DestroyEnemy), deathDuration);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
