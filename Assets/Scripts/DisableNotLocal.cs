using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableNotLocal : MonoBehaviourPunCallbacks
{
    public GameObject[] objList;

    public List<MonoBehaviour> scripts;

    public PhotonView view;

    public GameObject player;

    public void FixedUpdate()
    {
        if (!view.IsMine)
        {
            foreach (GameObject obj in objList)
            {
                obj.SetActive(false);
            }
            foreach (var script in scripts)
            {
                script.enabled = false;
            }

            enabled = false;
        }
        if (view.IsMine && player)
        {
            player.SetActive(true);
        }
    }
}
