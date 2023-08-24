using UnityEngine;
using Photon.Pun;

public class StructureRangedAttack : MonoBehaviour
{
    public float Range;

    private bool isAttacking = false;
    private string OtherTeamTag;
    private float closestDistance;
    private Collider[] hits;
    private GameObject TargetObj;
    public LayerMask Targetable;

    public UnitAttack AttackScript;
    private PhotonView view;


    private void Start()
    {
        closestDistance = Range + 1;

        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
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

    private void FixedUpdate()
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
                if (closestDistance > Range && isAttacking)
                {
                    TargetObj = null;
                    isAttacking = false;
                    AttackScript.StopAttack();
                }
                if(closestDistance <= Range && !isAttacking)
                {
                    isAttacking = true;
                    AttackScript.StartAttackScan(TargetObj);
                }
            }
        }
    }

    private GameObject FindTarget()
    {
        GameObject Tmp = null;
        hits = Physics.OverlapSphere(transform.position, Range, Targetable);
        closestDistance = Range;
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
