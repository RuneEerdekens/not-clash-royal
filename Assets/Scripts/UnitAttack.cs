using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    private GameObject Target;
    public bool Attacking = false;
    private bool isRanged;
    public GameObject projectile;
    public float AttackSpeed;
    private int OtherTeamMask;
    public Animation Anim;


    private void Awake()
    {
        isRanged = GetComponent<UnitPathfinding>().isRanged;
    }

    public void StartAttack(GameObject closetObj, int MaskNum)
    {
        Target = closetObj;
        OtherTeamMask = MaskNum;
        Attacking = true;
        InvokeRepeating("Attack", AttackSpeed, AttackSpeed);
    }

    public void StopAttack()
    {
        Attacking = false;
    }

    private void Attack()
    {
        Anim.Play();
        if (isRanged)
        {
            GameObject tempObj = Instantiate(projectile, transform.position, Quaternion.identity);
            tempObj.GetComponent<RangedProjectile>().Obj = Target;
            tempObj.GetComponent<RangedProjectile>().EnemieTeam = OtherTeamMask;
            tempObj.layer = gameObject.layer;
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
}
