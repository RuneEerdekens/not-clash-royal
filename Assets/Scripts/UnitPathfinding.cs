using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public class UnitPathfinding : MonoBehaviour
{

    public NavMeshAgent agent;
    public LayerMask Targetable;

    private bool isAttacking = false;

    public bool isRanged;
    public bool isHitscan;

    public float AttackRange;
    public float sightRange;

    private Collider[] hits;
    private string OtherTeamTag;
    private float closestDistance;
    private GameObject TargetObj;

    public UnitAttack AttackScript;
    public HealthScript HealthScript;

    private PhotonView view;
    // Start is called before the first frame update


    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            agent.stoppingDistance = AttackRange - 0.5f;

            if (tag.Equals("Team1"))
            {
                OtherTeamTag = "Team2";
            }
            else if (tag.Equals("Team2"))
            {
                OtherTeamTag = "Team1";
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (!TargetObj)
            {
                TargetObj = FindTarget();

                if (isAttacking)
                {
                    isAttacking = false;
                    AttackScript.StopAttack();
                }
            }

            if (TargetObj)
            {
                closestDistance = Vector3.Distance(transform.position, TargetObj.transform.position);
                if (closestDistance > sightRange)
                {
                    TargetObj = null;
                    if (isAttacking)
                    {
                        isAttacking = false;
                        AttackScript.StopAttack();
                    }
                }
                else if(closestDistance < sightRange)
                {
                    agent.SetDestination(TargetObj.transform.position);
                }

                if(closestDistance > AttackRange && isAttacking)
                {
                    isAttacking = false;
                    AttackScript.StopAttack();
                }
                else if (closestDistance <= AttackRange && !isAttacking)
                {
                    print("start");
                    isAttacking = true;
                    AttackScript.StartAttackProj(TargetObj, OtherTeamTag);
                }
            }
        }
    }

    private GameObject FindTarget()
    {
        GameObject Tmp = null;
        hits = Physics.OverlapSphere(transform.position, sightRange, Targetable);
        closestDistance = sightRange;
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag(OtherTeamTag))
            {
                float d = Vector3.Distance(transform.position, hit.transform.position);
                if (d < closestDistance)
                {
                    closestDistance = d;
                    Tmp = hit.gameObject;
                }
            }
        }
        return Tmp;
    } //returns closest valid target in sightrange or null if there are none
}
