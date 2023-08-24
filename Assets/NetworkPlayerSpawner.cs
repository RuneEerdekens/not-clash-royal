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
            PlayerPrefab = PhotonNetwork.Instantiate(PlayerInfo.name, SpawnP1.position, SpawnP1.rotation);
            GameObject Player = PlayerPrefab.transform.GetChild(0).gameObject;
            view = Player.GetComponent<PhotonView>();
            view.RPC("SetTeamTag", RpcTarget.AllBuffered, "Team1");
            GameObject objectiveObj = PhotonNetwork.Instantiate(Objective.name, SpawnP1.position + new Vector3(0, 0.5f, -3), SpawnP1.rotation);
            view = objectiveObj.GetComponent<PhotonView>();
            view.RPC("SetTeamTag", RpcTarget.AllBuffered, "Team1");
        }
        else
        {
            PlayerPrefab = PhotonNetwork.Instantiate(PlayerInfo.name, SpawnP2.position, SpawnP2.rotation);
            GameObject Player = PlayerPrefab.transform.GetChild(0).gameObject;
            view = Player.GetComponent<PhotonView>();
            view.RPC("SetTeamTag", RpcTarget.AllBuffered, "Team2");
            GameObject objectiveObj = PhotonNetwork.Instantiate(Objective.name, SpawnP2.position + new Vector3(0, 0.5f, 3), SpawnP1.rotation);
            view = objectiveObj.GetComponent<PhotonView>();
            view.RPC("SetTeamTag", RpcTarget.AllBuffered, "Team2");
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(PlayerPrefab);
    }
}
