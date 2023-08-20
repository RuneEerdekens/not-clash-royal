using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableNotLocal : MonoBehaviourPunCallbacks
{
    public GameObject[] objList;
    public GameObject[] objList2;

    public List<MonoBehaviour> scripts;

    public PhotonView view;

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

            foreach (GameObject obj in objList2)
            {
                obj.SetActive(true);
            }

            enabled = false;
        }
    }
}
