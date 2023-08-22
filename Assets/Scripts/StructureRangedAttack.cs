using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureRangedAttack : MonoBehaviour
{
    public float Range;

    private string OtherTeamTag;
    private float ClosetsDistance;
    private Collider[] hits;
    private GameObject TargetObj;
    private bool LookingForTarget = true;
    private bool canAttack = true;

    public UnitAttack AttackScript;



    private void Start()
    {
        ClosetsDistance = Range + 1;

        if (tag == "Team1")
        {
            OtherTeamTag = "Team2";
        }
        else if(tag == "Team2")
        {
            OtherTeamTag = "Team1";
        }
    }

    private void FixedUpdate()
    {
        if (LookingForTarget || !TargetObj)
        {
            hits = Physics.OverlapSphere(transform.position, Range);
            foreach (Collider hit in hits)
            {
                if (hit.tag == OtherTeamTag)
                {
                    float d = Vector3.Distance(transform.position, hit.transform.position);
                    if (d < ClosetsDistance)
                    {
                        ClosetsDistance = d;
                        TargetObj = hit.gameObject;
                    }
                }
            }
            if (TargetObj) { LookingForTarget = false; }
        }
        ChekTarget();
        if (TargetObj && !LookingForTarget && canAttack) {AttackScript.StartAttackScan(TargetObj);}
        if (LookingForTarget || !TargetObj) { AttackScript.CancelInvoke();}
    }

    private void ChekTarget()
    {
        if (TargetObj)
        {
            if (Vector3.Distance(transform.position, TargetObj.transform.position) > Range)
            {
                TargetObj = null;
                LookingForTarget = true;
            }
        }
        if(!TargetObj && !LookingForTarget)
        {
            LookingForTarget = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 255, 0);
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
