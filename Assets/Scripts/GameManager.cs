using TMPro;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject GameEndText;
    public Transform GameEndTextSpawn;

    private bool GameEnded = false;

    [PunRPC]
    private void TeamLost(string lossTag)
    {
        if (!GameEnded)
        {
            GameEnded = true;
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
}
