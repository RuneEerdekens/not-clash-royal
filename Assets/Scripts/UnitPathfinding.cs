using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitPathfinding : MonoBehaviour
{

    public NavMeshAgent agent;
    private Transform Target;

    private bool isAttacking = false;

    public bool isRanged;
    public bool isHitscan;

    public float AttackRange;
    public float sightRange;

    private Collider[] hits;
    private string OtherTeamTag;
    private float closestDistance;
    private GameObject ClosestObj;

    public UnitAttack AttackScript;
    public HealthScript HealthScript;
    // Start is called before the first frame update


    private void Start()
    {
        closestDistance = sightRange;

        agent.stoppingDistance = AttackRange;

        if(tag == "Team1")
        {
            OtherTeamTag = "Team2";
        }
        else if(tag == "Team2")
        {
            OtherTeamTag = "Team1";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAttacking || !Target)   
        {
            hits = Physics.OverlapSphere(transform.position, closestDistance);
            foreach (Collider hit in hits)
            {
                if (hit.tag == OtherTeamTag)
                {
                    float d = Vector3.Distance(transform.position, hit.transform.position);
                    if (d < closestDistance)
                    {
                        ClosestObj = hit.gameObject;
                        closestDistance = d;
                    }
                }
            }
            if (ClosestObj) { closestDistance = Vector3.Distance(transform.position, ClosestObj.transform.position); }
        }
        
        if (ClosestObj)
        {

            if(Vector3.Distance(transform.position, ClosestObj.transform.position) <= AttackRange && !isAttacking)
            {
                isAttacking = true;
                if (isHitscan)
                {
                    AttackScript.StartAttackScan(ClosestObj);
                }
                else
                {
                    AttackScript.StartAttackProj(ClosestObj, OtherTeamTag);
                }
            }

            if(Vector3.Distance(transform.position, ClosestObj.transform.position) > AttackRange && isAttacking)
            {
                isAttacking = false;
                closestDistance = sightRange;
                AttackScript.StopAttack();
            }
            if (!isAttacking)
            {
                agent.SetDestination(ClosestObj.transform.position);
            }
        }
        else if(isAttacking)
        {
            isAttacking = false;
            AttackScript.StopAttack();
        }
        if (!ClosestObj) { closestDistance = sightRange; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawWireSphere(transform.position, closestDistance);
        Gizmos.color = new Color(255, 0, 255);
        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
    }
}
