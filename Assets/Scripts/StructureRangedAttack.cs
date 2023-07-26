using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureRangedAttack : MonoBehaviour
{
    public float Range;
    public float Damage;
    public float attackSpeed;

    public Transform LazerStart;

    private LayerMask OtherTeamLayer;
    private float ClosetsDistance;
    private Collider[] hits;
    private GameObject TargetObj;
    private bool LookingForTarget = true;
    public GameObject lineRend;
    private bool canAttack = true;
    

    private void Start()
    {
        ClosetsDistance = Range + 1;
        if (gameObject.layer == 6)
        {
            OtherTeamLayer = LayerMask.GetMask("Team2");
        }
        else if (gameObject.layer == 7)
        {
            OtherTeamLayer = LayerMask.GetMask("Team1");
        }
    }

    private void FixedUpdate()
    {
        if (LookingForTarget || !TargetObj)
        {
            hits = Physics.OverlapSphere(transform.position, Range, OtherTeamLayer);
            foreach (Collider hit in hits)
            {
                if (!hit.CompareTag("nonTarget"))
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
        if (TargetObj && !LookingForTarget && canAttack) { StartCoroutine(DelayAttack());  Attack(); }
    }

    private void ChekTarget()
    {
        if (TargetObj )
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

    private void Attack()
    {
        if (LookingForTarget || !TargetObj) { CancelInvoke(); return; }
            
        TargetObj.GetComponent<HealthScript>().TakeDamage(Damage);
        GameObject tmpObj = Instantiate(lineRend, transform.position, Quaternion.identity);
        Vector3[] points = { LazerStart.position, TargetObj.transform.position };
        tmpObj.GetComponent<LineRenderer>().SetPositions(points);
        Destroy(tmpObj, 0.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 255, 0);
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    private IEnumerator DelayAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
