using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSetCamZ : MonoBehaviour
{
    public float z;
    public float fov;
    private void OnTriggerEnter(Collider other)
    {

        Vector3 v = Camera.main.transform.position;
        v.z = z;
        Camera.main.transform.position = v;
        Camera.main.fieldOfView = fov;
    }
}
