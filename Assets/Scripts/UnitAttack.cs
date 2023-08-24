using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UnitAttack : MonoBehaviour
{
    private GameObject Target;
    public float Damage;
    public Transform projectileOrRendSpawn;
    [HideInInspector]
    public bool Attacking = false;
    public bool isRanged;
    public GameObject projectileOrRend;
    public float AttackSpeed;
    private string OtherTeamTag;
    public Animation Anim;

    private PhotonView TargetView;

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
            GameObject tempObj = PhotonNetwork.Instantiate(projectileOrRend.name, projectileOrRendSpawn.position, Quaternion.identity);

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

    public void StartAttackScan(GameObject closestObj)
    {
        Target = closestObj;
        Attacking = true;
        InvokeRepeating("AttackScan", AttackSpeed, AttackSpeed);
    }

    private void AttackScan()
    {
        TargetView = Target.GetComponent<PhotonView>();
        TargetView.RPC("TakeDamage", TargetView.Controller, Damage);            //send rpc call that damages the other dude

        GameObject tmpObj = PhotonNetwork.Instantiate(projectileOrRend.name, transform.position, Quaternion.identity);

        GetComponent<PhotonView>().RPC("SetPoints", RpcTarget.AllBuffered, projectileOrRendSpawn.position, Target.transform.position, tmpObj.GetComponent<PhotonView>().ViewID);
        StartCoroutine(SelfDestruct(tmpObj, 0.5f));
    }
    
    [PunRPC]
    private void SetPoints(Vector3 p1, Vector3 p2, int ID)
    {
        Vector3[] points = { p1, p2 };
        PhotonNetwork.GetPhotonView(ID).GetComponent<LineRenderer>().SetPositions(points);

    }


     
    private IEnumerator SelfDestruct(GameObject obj, float delay)
    {

        yield return new WaitForSeconds(delay);

        PhotonNetwork.Destroy(obj);
    }
}
