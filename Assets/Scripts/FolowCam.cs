using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowCam : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        obj.transform.position = transform.position;
    }
}
