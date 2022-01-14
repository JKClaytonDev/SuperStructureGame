using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnableObject : MonoBehaviour
{
    public float shakeTime;
    public GameObject g;
    public float destroyTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        g.SetActive(true);
        if (destroyTime != 0)
            Destroy(g, destroyTime);
        if (shakeTime != 0)
        FindObjectOfType<CameraFollow>().shakeTime = Time.realtimeSinceStartup + shakeTime;
    }
}
