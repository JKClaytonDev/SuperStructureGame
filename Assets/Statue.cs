using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Statue : MonoBehaviour
{
    Animator anim;
    GameObject player;
    Rigidbody rb;
    public bool go = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!player)
            player = FindObjectOfType<PlayerScript>().gameObject;
        transform.LookAt(player.transform);
        Vector3 angles = transform.localEulerAngles;
        angles.x = 0;
        angles.z = 0;
        transform.localEulerAngles = angles;
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        if (go)
        {
            Vector3 vel = transform.forward * 5.9f;
            vel.y = rb.velocity.y;
            rb.velocity = vel;
        }

    }
}
