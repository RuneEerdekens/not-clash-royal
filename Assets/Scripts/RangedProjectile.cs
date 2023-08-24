using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RangedProjectile : MonoBehaviour
{
    public GameObject Obj;
    public Rigidbody rb;
    public float lifetime;
    public float speed;
    [HideInInspector]
    public string EnemieTeam;

    [HideInInspector]
    public float Damage;

    private PhotonView view;
    private PhotonView TargetView;



    private void Awake()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            print("we got here");
            Invoke("SelfDestruct", lifetime);
        }
    }

    private void FixedUpdate()
    {
        if (Obj && view.IsMine)
        {
            transform.LookAt(Obj.transform.position);
            transform.Rotate(90, 0, 0);

            rb.velocity = transform.up * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(EnemieTeam) && view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
            TargetView = other.gameObject.GetComponent<PhotonView>();

            TargetView.RPC("TakeDamage", TargetView.Controller, Damage);
            //animation
        }
    }

    private void SelfDestruct()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
