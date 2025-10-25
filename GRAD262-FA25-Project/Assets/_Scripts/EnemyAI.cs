using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    public GameObject projectile;
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public float rotSpeed;
    public float timeBetweenAttacks;
    public float attackRange;
    public float deathDuration;
    public int attackDamage;
    public bool rangedEnemy = true;
    public float attackKnockBackForce;

    private GameObject player;
    private PlayerControls playerControls;
    private Attack attack;
    private bool playerInAttackRange;
    private bool alreadyAttacked = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerControls = player.GetComponent<PlayerControls>();
        attack = player.GetComponent<Attack>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Defines the attack range around the enemy.
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Makes the enemy always look at the player.
        transform.LookAt(player.transform.position);

        //Ranged Enemy States
        if (playerInAttackRange && rangedEnemy) 
            ShootPlayer();
        if (!playerInAttackRange && rangedEnemy)
            ChasePlayer();

        //Melee Enemy States
        if (!rangedEnemy && !alreadyAttacked)
            ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void ShootPlayer()
    {
        //Keeps the enemy from moving
        agent.SetDestination(transform.position);
        
        if(!alreadyAttacked)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            Attack attack = newProjectile.GetComponent<Attack>();
            attack.damage = attackDamage;
            attack.knockBackForce = attackKnockBackForce;
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            agent.SetDestination(transform.position);
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
