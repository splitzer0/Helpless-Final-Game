using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class killerAI : MonoBehaviour
{
    public NavMeshAgent navm;
    public GameObject PlayerTarget;
    public LayerMask PlayerMask;
    public LayerMask obstructionMask;
    public float radius;
    [Range(0,360)]
    public float angle;

    public bool canSeePlayer;
    public bool canHearPlayer;
    public bool canSeeLight;

    public bool wanderPosSet;
    public Vector3 wandertarget;
    public float walkPointRange;
    public LayerMask bonkbonk;
    // Start is called before the first frame update
    void Start()
    {
        navm = GetComponent<NavMeshAgent>();
        StartCoroutine(AIchecksCO());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AIchecksCO()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            AIChecks();
        }
    }

    public void AIChecks()
    {
        SearchRadius();
        if (canSeePlayer || canHearPlayer || canSeeLight)
        {
            chase();
        }
        if (!canSeePlayer && !canHearPlayer && !canSeeLight)
        {
            wander();
        }
    }

    public void SearchRadius()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, PlayerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    public void chase()
    {
        navm.SetDestination(PlayerTarget.transform.position);
    }

    public void wander()
    {
        if (!wanderPosSet) searchWalkPoint();
        if (wanderPosSet)
        {
            navm.SetDestination(wandertarget);
        }

        Vector3 distandceToWalkPoint = transform.position - wandertarget;

        if(distandceToWalkPoint.magnitude < 1f)
        {
            wanderPosSet = false;
        }
    }

    public void searchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        wandertarget = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(wandertarget, -transform.up, 2f, bonkbonk))
        {
            wanderPosSet = true;
        }
    }
}
