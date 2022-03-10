using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour
{
    public Transform Enemy;
    [Range(0,360)]
    public float angle;

    public float radius;
    public LayerMask obstructionMask;
    public LayerMask EnemyMask;

    public bool canSeeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        searchForEnemy();
    }

    public void searchForEnemy()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, EnemyMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    Enemy.GetComponent<killerAI>().canSeeLight = true;
                    canSeeEnemy = true;
                }
                else
                {
                    Enemy.GetComponent<killerAI>().canSeeLight = false;
                    canSeeEnemy = false;
                }
            }
            else
            {
                Enemy.GetComponent<killerAI>().canSeeLight = false;
                canSeeEnemy = false;
            }
        }
        else if (Enemy.GetComponent<killerAI>().canSeeLight == true)
        {
            Enemy.GetComponent<killerAI>().canSeeLight = false;
            canSeeEnemy = false;
        }
    }
}
