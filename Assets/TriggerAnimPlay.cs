using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimPlay : MonoBehaviour
{
    public Animator animator;
    public string animatorName;
    public bool setSpeed;
    public bool statue;
    // Start is called before the first frame update
    private void Start()
    {
        if (setSpeed)
            animator.speed = 0;
        GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerScript>())
            return;
        if (statue)
        {
            animator.GetComponent<Statue>().go = true;
        }
        if (setSpeed)
            animator.speed = 1;
        GetComponent<BoxCollider>().enabled = false;
        animator.Play(animatorName);
    }
}
