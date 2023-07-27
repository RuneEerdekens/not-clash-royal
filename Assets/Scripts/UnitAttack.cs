using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    private GameObject Target;
    public float Damage;
    public Transform projectileOrRendSpawn;
    [HideInInspector]
    public bool Attacking = false;
    private bool isRanged;
    public GameObject projectileOrRend;
    public float AttackSpeed;
    private int OtherTeamMask;
    public Animation Anim;


    private void Awake()
    {
        isRanged = GetComponent<UnitPathfinding>().isRanged;
    }

    public void StartAttackProj(GameObject closetObj, int MaskNum)
    {
        Target = closetObj;
        OtherTeamMask = MaskNum;
        Attacking = true;
        InvokeRepeating("AttackProj", AttackSpeed, AttackSpeed);
    }

    public void StopAttack()
    {
        Attacking = false;
        CancelInvoke();
    }

    private void AttackProj()
    {
        Anim.Play();
        if (isRanged)
        {
            GameObject tempObj = Instantiate(projectileOrRend, projectileOrRendSpawn.position, Quaternion.identity);
            tempObj.GetComponent<RangedProjectile>().Obj = Target;
            tempObj.GetComponent<RangedProjectile>().EnemieTeam = OtherTeamMask;
            tempObj.GetComponent<RangedProjectile>().Damage = Damage;
            tempObj.layer = gameObject.layer +2;
            if (!Attacking)
            {
                Destroy(tempObj);
            }
        }

        if (!Attacking)
        {
            CancelInvoke();
        }
    }

    public void StartAttackScan(GameObject closestObj)
    {
        Target = closestObj;
        Attacking = true;
        InvokeRepeating("AttackScan", AttackSpeed, AttackSpeed);
    }

    private void AttackScan()
    {
        Target.GetComponent<HealthScript>().TakeDamage(Damage);
        GameObject tmpObj = Instantiate(projectileOrRend, transform.position, Quaternion.identity);
        Vector3[] points = { projectileOrRendSpawn.position, Target.transform.position };
        tmpObj.GetComponent<LineRenderer>().SetPositions(points);
        Destroy(tmpObj, 0.5f);
    }
}
