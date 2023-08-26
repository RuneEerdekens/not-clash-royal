using TMPro;
using UnityEngine;
using Photon.Pun;

public class ObjectiveScript : MonoBehaviour
{
    private PhotonView view;
    public GameObject GameEndText;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnDestroy()
    {
        if (view.IsMine)
        {
            view.RPC("TeamLost", RpcTarget.AllBuffered, tag);
        }
    }

    [PunRPC]
    private void TeamLost(string lossTag)
    {
        GameObject tmp = Instantiate(GameEndText, Vector3.up * 10, Quaternion.identity);
        if (lossTag == tag)
        {
            tmp.GetComponent<TextMeshPro>().text = "You lost.";
        }
        else if(lossTag != tag)
        {
            tmp.GetComponent<TextMeshPro>().text = "You won.";
        }
    }
}
