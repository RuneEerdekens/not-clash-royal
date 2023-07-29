using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to server");
        base.OnConnectedToMaster();
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        options.IsVisible = true;
        options.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("Main room", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player joined room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
