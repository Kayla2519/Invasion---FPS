using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int enemyScoreValue = 100;
    [SerializeField] float healAmount = 25;

    public NavMeshAgent agent;
    public Transform player;
    public EnemyGun gun;

    public LayerMask whatIsGround, whatIsPlayer;

    public float maxHealth = 50f;
    public float currentHealth = 0f;

    private ItemDrop itemDrop;
    public EnemyHealthBar healthBar;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks = 1f;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public bool isDying = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        gun = GetComponentInChildren<EnemyGun>();

        currentHealth = maxHealth;
        itemDrop = GetComponent<ItemDrop>();

        if (healthBar == null)
        {
            Debug.LogError("HealthBar is not assigned in the Inspector! Make sure to assign it.");
            return; 
        }

        healthBar.SetHealth(currentHealth / maxHealth);
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!isDying)
        {
            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            gun.Shoot();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0f) currentHealth = 0f;

        healthBar.SetHealth(currentHealth / maxHealth);

        if (currentHealth == 0f && !agent.isStopped)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isDying = true;

        // Make the enemy fall when they die
        agent.isStopped = true;
        agent.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        rb.AddTorque(new Vector3(Random.Range(1f, 3f), 0f, Random.Range(1f, 3f)) * 5f, ForceMode.Impulse);


        // Wait 1.5 seconds before removing their body
        yield return new WaitForSeconds(1.5f);

        // Have enemies drop items when they die
        if (itemDrop != null)
        {
            itemDrop.dropItem(transform.position);
        }

        // Remove body
        DestroyEnemy();

        // Update player score
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.score += enemyScoreValue;
            playerScript.UpdateScoreUI();
            playerScript.Heal(healAmount);
        }
    }


    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

// Source: https://www.youtube.com/watch?v=UjkSFoLxesw
