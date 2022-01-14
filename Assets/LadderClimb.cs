using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public bool moveTop;
    public bool moveBottom;
    public int flipDir = 1;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            if (!other.GetComponent<Animator>().GetBool("ClimbLadder"))
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    Vector3 pos = transform.position;
                    pos.z = other.transform.position.z;
                    pos.y = other.transform.position.y;
                    pos.x -= 0.3826f;
                    other.transform.position = pos;
                    Debug.Log("GO ALREADY");
                    other.GetComponent<Animator>().SetBool("ClimbLadder", true);
                    other.GetComponent<Animator>().Play("LadderClimb");
                }
            }
            else
            {

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            if (other.GetComponent<Animator>().GetBool("ClimbLadder"))
            {
                if (moveTop)
                {
                    other.GetComponent<Animator>().SetBool("ClimbLadder", false);

                    other.transform.position -= (transform.forward * 0.5f*flipDir) - transform.up * 0.5f;
                    other.GetComponent<Animator>().Play("JumpLand");
                }
            }
        }

    }
}
