using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log(tag + " lost");
        //load victory screen
    }
}
