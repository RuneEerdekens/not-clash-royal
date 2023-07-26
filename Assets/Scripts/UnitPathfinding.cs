using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitPathfinding : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform Target;
    public float AttackRange;
    public UnitAttack AttackScript;

    [SerializeField]
    private bool isAttacking = false;

    public bool isRanged;

    private int Team;

    public float sightRange;
    private Collider[] hits;
    private LayerMask OtherTeamLayer;
    private int OtherTeamInt;
    private float closestDistance;
    private GameObject ClosestObj;
    public HealthScript HealthScript;
    // Start is called before the first frame update


    private void Start()
    {
        closestDistance = sightRange;

        agent.stoppingDistance = AttackRange; 

        Team = HealthScript.Team;
        OtherTeamLayer = HealthScript.OtherTeamLayer;
        OtherTeamInt = HealthScript.OtherTeamInt;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAttacking || !Target)   
        {
            hits = Physics.OverlapSphere(transform.position, closestDistance, OtherTeamLayer);
            foreach (Collider hit in hits)
            {
                float d = Vector3.Distance(transform.position, hit.transform.position);
                if (d < closestDistance)
                {
                    ClosestObj = hit.gameObject;
                    closestDistance = d;
                }
            }
            if (ClosestObj) { closestDistance = Vector3.Distance(transform.position, ClosestObj.transform.position); }
        }
        
        if (ClosestObj)
        {

            if(Vector3.Distance(transform.position, ClosestObj.transform.position) <= AttackRange && !isAttacking)
            {
                isAttacking = true;
                AttackScript.StartAttack(ClosestObj, OtherTeamInt);
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
