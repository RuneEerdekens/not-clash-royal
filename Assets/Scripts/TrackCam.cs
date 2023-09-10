using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCam : MonoBehaviour
{
    public Vector3 offset;

    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(offset);
    }
}
