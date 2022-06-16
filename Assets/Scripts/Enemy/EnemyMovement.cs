using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;



    //Sight
    private float awarenessMeter;
    private float awarenessTimer;
    public Image bar;

    private Animator animator;


    private enum EnemyState { Patrolling, Attacking }
    private EnemyState currentState = EnemyState.Patrolling;
    private bool standing = false;
    private Vector3[] patrolPoints;
    private Vector3 destinationPoint;


    private Camera enemyFOV;

    [Header("Detection Settings")]
    [SerializeField] private float sphereRadius = 8f;


    private void GetPatrolPoints()
    {
        int childCount = GameObject.Find("PatrolPointLocations").transform.childCount;
        patrolPoints = new Vector3[childCount];

        for (int i = 0; i < childCount; i++)
            patrolPoints[i] = GameObject.Find("PatrolPointLocations").transform.GetChild(i).transform.position;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetComponentInChildren<Animator>();
        enemyFOV = GetComponentInChildren<Camera>();

        agent.autoBraking = false;
        GetPatrolPoints();
        ChooseDestinationPoint();
    }

    void Update()
    {
        Patrol();

        SphereDetection();
        ConeDetection();

        MoveTowardsPlayer();

        AwarenessBar();

        if (currentState != EnemyState.Attacking)
             AwarenessFall(1f);

        if (standing == true)
		{
            animator.SetBool("standing", standing);
            agent.updateRotation = false;
            agent.SetDestination(transform.position);
        }
		else
		{
            animator.SetBool("standing", standing);
            agent.updateRotation = true;
        }
    }


    private void Patrol()
    {
        if(currentState == EnemyState.Patrolling)
		{
            agent.SetDestination(destinationPoint);
            currentState = EnemyState.Patrolling;

            DestinationPointReached();
        }
    }
    private void ChooseDestinationPoint()
	{
        destinationPoint = patrolPoints[Random.Range(0, patrolPoints.Length)];
        Debug.Log(Random.Range(0, patrolPoints.Length - 1));
    }
    private void DestinationPointReached()
	{
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            ChooseDestinationPoint();
	}



    private void MoveTowardsPlayer()
	{
        if(currentState == EnemyState.Attacking)
		{
            agent.SetDestination(player.position);
        }         
    }
 
    private void SphereDetection()
	{
        RaycastHit hit;
        Physics.Raycast(transform.position, (player.position - transform.position), out hit, Mathf.Infinity);

        Collider[] sphereCollider = Physics.OverlapSphere(transform.position, sphereRadius, LayerMask.GetMask("Player"));
        for (int i = 0; i < sphereCollider.Length; i++)
        {
            if (sphereCollider[i].CompareTag("Player") && hit.transform == player)
			{
                currentState = EnemyState.Attacking;
                AwarenessRaise(0.5f);
            }
        }        
    }

    private void ConeDetection()
	{
        Vector3 screenPoint = enemyFOV.WorldToViewportPoint(player.position);
        bool inFOV = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.position - transform.position), out hit, Mathf.Infinity))
        {
            if (hit.transform == player && inFOV)
            {
                AwarenessRaise(1f);
                standing = true;
            }
            else if (standing == true)
			{
                currentState = EnemyState.Patrolling;
                standing = false;
			}
        }
    }

    private void AwarenessRaise(float cooldown)
	{
        if(Time.time > awarenessTimer)
		{
            awarenessMeter += 0.1f;

            if (awarenessMeter >= 1)
                awarenessMeter = 1;

            awarenessTimer = Time.time + cooldown;
        }
	}

    private void AwarenessFall(float cooldown)
    {
        if (Time.time > awarenessTimer)
        {
            awarenessMeter -= 0.1f;

            if (awarenessMeter <= 0)
                awarenessMeter = 0;

            awarenessTimer = Time.time + cooldown;
        }
    }

    private void AwarenessBar()
	{
        if (awarenessMeter >= 1f)
        {
            Debug.Log(":)");
        }

        if (awarenessMeter >= 0.76f)
        {
            bar.color = new Color32(255, 0, 0, 255);
        }
        else if (awarenessMeter >= 0.51 && awarenessMeter <= 0.75f)
        {
            bar.color = new Color32(255, 255, 0, 255);
        }
        else
        {
            bar.color = new Color32(0, 128, 0, 255);
        }

        bar.fillAmount = awarenessMeter;
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

    }
}
