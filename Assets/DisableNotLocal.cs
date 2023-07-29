using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisableNotLocal : MonoBehaviourPunCallbacks
{
    public GameObject[] objList;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (!photonView.IsMine)
        {
            foreach (GameObject obj in objList)
            {
                obj.SetActive(false);
            }
        }
    }
}
