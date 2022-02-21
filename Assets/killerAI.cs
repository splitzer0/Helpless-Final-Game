using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class killerAI : MonoBehaviour
{
    public NavMeshAgent navm;
    public Transform Player;
    public float radius;
    public bool playerInRange;

    public bool wanderPosSet;
    public Vector3 wandertarget;
    public float minWanderRange;
    public float maxWanderRange;
    public LayerMask bonkbonk;
    // Start is called before the first frame update
    void Start()
    {
        navm = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        SearchRadius();
        if (playerInRange)
        {
            chase();
        }
        if (!playerInRange)
        {
            wander();
        }
    }

    public void SearchRadius()
    {
        if(Vector3.Distance(transform.position, Player.position) > radius)
        {
            playerInRange = false;
        }
        else
        {
            playerInRange = true;
        }
    }

    public void chase()
    {
        navm.SetDestination(Player.position);
    }

    public void wander()
    {
        if (!wanderPosSet)
        {
            wandertarget = new Vector3(Random.Range(minWanderRange, maxWanderRange), Random.Range(minWanderRange, maxWanderRange), Random.Range(minWanderRange, maxWanderRange));

            RaycastHit hit;
            if (Physics.Raycast(wandertarget, -transform.up, out hit, 3, bonkbonk))
            {
                wanderPosSet = true;
            }
            else
            {
                wanderPosSet = false;
            }
        }

        if(wanderPosSet) {
            navm.SetDestination(wandertarget);
            if (Vector3.Distance(wandertarget, transform.position) < 4)
            {
                wanderPosSet = false;
            }
        }
    }

}
