using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mobileControls : MonoBehaviour
{
    public GameObject player;
    float walkTime;
    public float walkDir;
    float jumpTime;
    private void Start()
    {
        player = FindObjectOfType<PlayerScript>().gameObject;
    }
    public void walkLeft()
    {
        walkDir -= 1;
        walkTime = Time.realtimeSinceStartup + 0.1f;
    }
    public void walkRight()
    {
        walkDir += 1;
        walkTime = Time.realtimeSinceStartup + 0.1f;
    }
    void Update()
    {
        walkDir = Mathf.Max(-2, Mathf.Min(2, walkDir));
        player.GetComponent<PlayerScript>().horizontalAxis = Mathf.Max(-1, Mathf.Min(1, walkDir));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<PlayerScript>().jump = jumpTime > Time.realtimeSinceStartup;
    }
    public void run()
    {
        bool running = player.GetComponent<PlayerScript>().running;
        player.GetComponent<PlayerScript>().running = !player.GetComponent<PlayerScript>().running;
    }
    public void stopMove()
    {
        walkDir = 0;
    }
    public void jump()
    {
        jumpTime = Time.realtimeSinceStartup + 0.5f;
    }
    public void onRelease()
    {
        walkDir = 0;
    }
}
