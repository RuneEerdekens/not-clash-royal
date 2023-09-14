using UnityEngine;
using Photon.Pun;

public class TagManger : MonoBehaviour
{
    [PunRPC]
    void SetTeamTag(string newTag)
    {
        gameObject.tag = newTag;
    }
}
