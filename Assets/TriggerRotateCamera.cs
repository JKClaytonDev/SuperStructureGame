using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotateCamera : MonoBehaviour
{
    public Vector3 rotation;
    bool startRot;
    bool start;
    private void OnTriggerEnter(Collider other)
    {
        start = true;
    }
    private void Update()
    {
        if (start)
        {
            Camera.main.transform.localEulerAngles += (rotation-Camera.main.transform.localEulerAngles)*Time.deltaTime*5;
            if (Vector3.Distance(Camera.main.transform.localEulerAngles, rotation) < 0.1f)
            {
                Camera.main.transform.localEulerAngles = rotation;
                start = false;
            }
        }
    }
}
