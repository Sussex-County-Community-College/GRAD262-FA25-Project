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
    public bool rangedEnemy = true;
    public float health;
    public int XP_Gained; //The amount of XP that is gained from killing the enemy.
    public float rotSpeed;
    public float timeBetweenAttacks;
    public float attackRange;
    public float deathDuration; //Might be useful for animations.
    public int attackDamage;
    public float attackKnockBackForce;
    public float attackKnockBackDelay = 0.15f;
    public int touchDamage;
    public float touchKnockBackForce;
    public float touchKnockBackDelay = 0.15f;

    private GameObject player;
    private PlayerControls playerControls;
    private RoomManager roomManager;
    private bool playerInAttackRange;
    private bool alreadyAttacked = false;
    private bool dying = false;

    private void OnEnable()
    {
        player = GameObject.Find("Player"); //Finds the player to follow them.

        //Retrieves components from the player and enemy.
        playerControls = player.GetComponent<PlayerControls>();
        agent = GetComponent<NavMeshAgent>();
        roomManager = GetComponentInParent<RoomManager>();

        //Restricts the navMesh from rotating the enemy, this will be handled manually.
        agent.updateRotation = false;
    }

    void Update()
    {
        //Defines the attack range around the enemy.
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Makes the enemy always look at the player.
        transform.LookAt(player.transform.position + Vector3.up);

        //Ranged Enemy States
        if (playerInAttackRange && rangedEnemy) 
            ShootPlayer();
        if (!playerInAttackRange && rangedEnemy)
            ChasePlayer();

        //Melee Enemy States
        if (!rangedEnemy && !alreadyAttacked)
            ChasePlayer();
    }

    //Lets the enemy follow the player when they are not within attack range.
    //Used for both melee & ranged enemies.
    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    //Ranged Enemy Attack.
    private void ShootPlayer()
    {
        //Keeps the enemy from moving
        agent.SetDestination(transform.position);
        
        if(!alreadyAttacked) //Checks if the enemy has attacked yet.
        {
            //Creates a projectile and defines the damage & knockback of its attack.
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            Attack attack = newProjectile.GetComponent<Attack>();
            attack.damage = attackDamage;
            attack.forceDelay = attackKnockBackDelay;
            attack.knockBackForce = attackKnockBackForce;

            //Sets bool to already attacked and will call the reset attack after a publicly determined amount of time.
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    //Melee Enemy Attack.
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player")) //Checks if it collided with the player.
        {
            agent.SetDestination(transform.position); //Stops the enemy from moving.

            //Only "attack" if it's a melee enemy.
            //If the player runs into a ranged enemy, it's their own fault and does not affect the enemy's rate of attacks.
            if(!rangedEnemy)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }

            //Deals damage to the player for touching the enemy.
            playerControls.TakeDamage(touchDamage, gameObject, touchKnockBackForce, touchKnockBackDelay);
        }
    }

    //Sets the bool to false to let the enemy attack again.
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //A public function for other classes to call to deal damage to the enemy.
    public void TakeDamage(int damage)
    {
        health -= damage;

        //Destroys the enemy when they reach 0 HP.
        if(health <= 0 && !dying)
        {
            
            Invoke(nameof(DestroyEnemy), deathDuration);
            playerControls.XP += XP_Gained;
            roomManager.DecreaseEnemies();
            dying = true;
        }
            
    }

    //Destroys the enemy.
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
