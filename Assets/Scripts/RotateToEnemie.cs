using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateToEnemie : MonoBehaviour
{

    private StructureRangedAttack AttackScript;
    private PhotonView view;
    public Transform BodyToRotate;
    public Vector3 rotationOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        AttackScript = GetComponent<StructureRangedAttack>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (AttackScript.TargetObj)
            {
                Transform TargetTransform = AttackScript.TargetObj.transform;
                BodyToRotate.LookAt(TargetTransform.position, Vector3.up);
                Vector3 eulerAngles = BodyToRotate.eulerAngles;
                eulerAngles.x = 0;
                eulerAngles.z = 0;
                eulerAngles += rotationOffset;
                BodyToRotate.eulerAngles = eulerAngles;
            }
        }
    }
}
