using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TagManger : MonoBehaviour
{
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            tag = "Team1";
        }
        else
        {
            tag = "Team2";
        }
    }
}
