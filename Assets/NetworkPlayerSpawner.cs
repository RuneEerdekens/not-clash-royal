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

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.IsMasterClient)
        {
            PlayerPrefab = PhotonNetwork.Instantiate(PlayerInfo.name, SpawnP1.position, SpawnP1.rotation);
            GameObject Player = PlayerPrefab.GetComponentInChildren<PlaceUnit>().gameObject;
            GameObject PlayerObjetive = PhotonNetwork.Instantiate(Objective.name, SpawnP1.position + new Vector3(0, 0.5f, -3), SpawnP1.rotation);

            Player.layer = 6;
            PlayerObjetive.layer = 6;
        }
        else
        {
            PlayerPrefab = PhotonNetwork.Instantiate(PlayerInfo.name, SpawnP2.position, SpawnP2.rotation);
            GameObject Player = PlayerPrefab.GetComponentInChildren<PlaceUnit>().gameObject;
            GameObject PlayerObjetive = PhotonNetwork.Instantiate(Objective.name, SpawnP2.position + new Vector3(0, 0.5f, 3), SpawnP1.rotation);

            Player.layer = 7;
            PlayerObjetive.layer = 7;
        }
       
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(PlayerPrefab);
    }
}
