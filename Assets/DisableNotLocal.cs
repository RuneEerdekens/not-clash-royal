using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DisableNotLocal : MonoBehaviourPunCallbacks
{
    public GameObject[] objList;
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (!photonView.IsMine)
        {
            foreach (GameObject obj in objList)
            {
                obj.SetActive(false);
            }
        }
    }
}
