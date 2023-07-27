using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    public GameObject Obj;
    public Rigidbody rb;
    public float speed;
    [HideInInspector]
    public int EnemieTeam;

    [HideInInspector]
    public float Damage;



    private void Awake()
    {
        Destroy(gameObject, 10);
    }

    private void FixedUpdate()
    {
        if (Obj)
        {
            transform.LookAt(Obj.transform.position);
            transform.Rotate(90, 0, 0);

            rb.velocity = transform.up * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == EnemieTeam && !other.CompareTag("nonTarget"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<HealthScript>().TakeDamage(Damage);
            //animation
        }
    }
}
