using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject PlayerInfo;
    public GameObject Objective;
    private GameObject PlayerPrefab;

    public Transform SpawnP1;
    public Transform SpawnP2;
    private PhotonView view;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(PlayerInfo.name, SpawnP1.position, SpawnP1.rotation);
            PhotonNetwork.Instantiate(Objective.name, SpawnP1.position + new Vector3(0, 0.5f, -3), SpawnP1.rotation);
        }
        else
        {
            PhotonNetwork.Instantiate(PlayerInfo.name, SpawnP2.position, SpawnP2.rotation);
            PhotonNetwork.Instantiate(Objective.name, SpawnP2.position + new Vector3(0, 0.5f, 3), SpawnP1.rotation);
        }
       
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(PlayerPrefab);
    }
}
