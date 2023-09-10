using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UnitAttack : MonoBehaviour
{
    public GameObject Target;
    public float Damage;
    public Transform projectileSpawn;
    [HideInInspector]
    public bool Attacking = false;
    public bool isRanged;
    public GameObject projectile;
    public float AttackSpeed;
    private string OtherTeamTag;
    public Animation Anim;

    public void StartAttackProj(GameObject closetObj, string TagString)
    {
        Target = closetObj;
        OtherTeamTag = TagString;
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
            GameObject tempObj = PhotonNetwork.Instantiate(projectile.name, projectileSpawn.position, Quaternion.identity);

            tempObj.GetComponent<RangedProjectile>().Obj = Target;
            tempObj.GetComponent<RangedProjectile>().EnemieTeam = OtherTeamTag;
            tempObj.GetComponent<RangedProjectile>().Damage = Damage;
            tempObj.tag = tag;
            if (!Attacking)
            {
                PhotonNetwork.Destroy(tempObj);
            }
        }

        if (!Attacking)
        {
            CancelInvoke();
        }
    }
}
