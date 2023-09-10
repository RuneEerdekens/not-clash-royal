using TMPro;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject GameEndText;
    public Transform GameEndTextSpawn;

    [PunRPC]
    private void TeamLost(string lossTag)
    {
        GameObject tmp = Instantiate(GameEndText, GameEndTextSpawn.position, GameEndTextSpawn.rotation);
        if (lossTag == tag)
        {
            tmp.GetComponent<TextMeshPro>().text = "You lost.";
        }
        else if (lossTag != tag)
        {
            tmp.GetComponent<TextMeshPro>().text = "You won.";
        }
    }
}
