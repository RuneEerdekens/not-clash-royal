using UnityEngine;
using Photon.Pun;

public class ObjectiveScript : MonoBehaviour
{
    [HideInInspector]
    public PhotonView PlayerView;

    private void OnDisable()
    {
        if (PlayerView)
        {
            if (PlayerView.IsMine)
            {
                PlayerView.RPC("TeamLost", RpcTarget.AllBuffered, tag);
            }
        }
    }
}
