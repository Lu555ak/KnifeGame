using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask groundMask, playerMask;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInNearSightRange, playerInLongSightRange;

    //Sight
    private float inSightMeter = 0f;
    private float inNearSightMultiplier = 1.15f;
    private float inLongSightMultiplier = 0.75f;
    private float notInSight = 0.1f;
    private float inSight = 0.2f;
    private float cooldown = 0.5f;
    private float cooldownTimer = 0f;

    private float patrolCooldown = 5f;
    private float patrolCooldownTimer;
    public Image bar;
    public bool spotPlayer = false;

    private Animator animator;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //animator.SetBool("spotPlayer", spotPlayer);
        playerInNearSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInLongSightRange = Physics.CheckSphere(transform.position, attackRange, playerMask);


        if (!playerInNearSightRange && !playerInLongSightRange)
        {
            if (Time.time > patrolCooldownTimer)
            {
                inSightMeter -= notInSight;
                patrolCooldownTimer = Time.time + patrolCooldown;
            }
            Patroling();
        }

        if (playerInNearSightRange && !playerInLongSightRange)
        {
            if (Time.time > cooldownTimer)
            {
                inSightMeter += inSight * inNearSightMultiplier;
                cooldownTimer = Time.time + cooldown;
            }
            ChasePlayer();
        }

        if (playerInNearSightRange && playerInLongSightRange)
        {
            if (Time.time > cooldownTimer)
            {
                inSightMeter += inSight * inLongSightMultiplier;
                cooldownTimer = Time.time + cooldown;
            }
            SpotPlayer();
        }

        if(inSightMeter >= 1f)
        {
            Debug.Log(":)");
        }

        if(inSightMeter >= 0.76f)
        {
            bar.color = new Color32(255, 0, 0, 255);
        }
        else if(inSightMeter >= 0.51 && inSightMeter <= 0.75f)
        {
            bar.color = new Color32(255, 255, 0, 255);
        }
        else
        {
            bar.color = new Color32(0, 128, 0, 255);
        }

        bar.fillAmount = inSightMeter;
    }

    private void Patroling()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            enemy.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        spotPlayer = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);

        spotPlayer = false;
    }

    private void SpotPlayer()
    {
        enemy.SetDestination(transform.position);
        spotPlayer = true;
        transform.LookAt(player);
    }
}
