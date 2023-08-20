using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCam : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.LookAt(Camera.current.transform.position);
    }
}
