using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedTrigger : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.speed = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        FindObjectOfType<CameraFollow>().shake = 1;
        this.enabled = false;
        anim.speed = 1;
    }
}
