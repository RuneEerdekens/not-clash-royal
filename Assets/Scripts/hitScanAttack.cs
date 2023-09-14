using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class hitScanAttack : MonoBehaviour
{
    
    [HideInInspector]
    public GameObject Target;

    [HideInInspector]
    public bool Attacking = false;

    public Transform EffectSpawn;
    public GameObject Effect;

    public float Damage;
    public float AttackSpeed;

    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void StopAttack()
    {
        Attacking = false;
        CancelInvoke();
    }

    public void StartAttack(GameObject ClosestObj)
    {
        Target = ClosestObj;
        Attacking = true;
        InvokeRepeating("Attack", 0, AttackSpeed);
    }

    private void Attack()
    {
        Target.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, Damage);

        view.RPC("SpawnEffect", RpcTarget.All, EffectSpawn.transform.position, EffectSpawn.transform.rotation);
    }

    [PunRPC]
    private void SpawnEffect(Vector3 pos, Quaternion rot)
    {
        Destroy(Instantiate(Effect, pos, rot), 3f);
    }
}
