using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    float zPos;
    public Animator anim;
    AnimationEvent jumpEVT;
    AnimationEvent stepSound;
    AnimationEvent landSound;
    AnimationEvent lockPosEVT;
    public bool posLocked;
    Vector3 lockedPos;
    public LayerMask layers;
    public AudioClip[] stepSounds;
    public AudioClip[] jumpSounds;
    public AudioClip landSounds;
    Rigidbody rb;
    public float walkSpeed;
    public float climbSpeed;
    float speed;
    float slideTime;
    Vector3 wallPos;
    float runTime;
    public float horizontalAxis;
    float verticalAxis;
    public bool running;
    public bool mobileControls;
    public bool jump;
    // Start is called before the first frame update

    void Start()
    {
        zPos = transform.position.z;
        Cursor.visible = false;
        FindObjectOfType<CameraFollow>().player = gameObject;
        rb = GetComponent<Rigidbody>();
        stepSound = new AnimationEvent();
        stepSound.functionName = "Step";
        jumpEVT = new AnimationEvent();
        jumpEVT.functionName = "Jump";
        landSound = new AnimationEvent();
        landSound.functionName = "Land";
        lockPosEVT = new AnimationEvent();
        lockPosEVT.functionName = "lockPos";
        if (PlayerPrefs.HasKey("X"))
        {
            Vector3 pos;
            pos.x = PlayerPrefs.GetFloat("X");
            pos.y = PlayerPrefs.GetFloat("Y");
            pos.z = PlayerPrefs.GetFloat("Z");
            transform.position = pos;
            pos.z = Camera.main.transform.position.z;
            FindObjectOfType<CameraFollow>().transform.position = pos;
        }
    }
    void Jump()
    {
        GetComponent<AudioSource>().PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length-1)]);

        GetComponent<Rigidbody>().velocity = new Vector3(0, 6, 0);
        if (running)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(6, 4, 0);
            runTime = Time.realtimeSinceStartup + 1;
        }

    }
    void Land()
    {
        GetComponent<AudioSource>().PlayOneShot(landSounds);

    }
    void Step()
    {
        GetComponent<AudioSource>().PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length-1)]);
    }
    void lockPos()
    {
        if (!posLocked)
            return;
        lockedPos = wallPos;
        Vector3 pos = transform.position;
        pos.y = lockedPos.y + 2;
        transform.position = pos;
    }
    // Update is called once per frame
    void Update()
    {
        if (!mobileControls)
        {
            jump = Input.GetKey(KeyCode.Space);
            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");
            running = Input.GetKey(KeyCode.LeftShift);
        }
        Vector3 pos = transform.position;
        pos.z = zPos;
        transform.position = pos;
        if (posLocked)
        {
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            vel.y = 0;
            GetComponent<Rigidbody>().velocity = vel;
            
        }
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("LadderClimb"))
        {
            GetComponent<Rigidbody>().velocity = 2 * Vector3.up * climbSpeed * Input.GetAxis("Vertical");
            anim.speed = verticalAxis * 2;
            return;
        }
        anim.speed = 1;
        anim.SetBool("Walk", Mathf.Abs(horizontalAxis) > 0.5f);
        anim.SetBool("Jump", jump);
        anim.SetFloat("YVel", rb.velocity.y);
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);
        Vector3 angles = transform.localEulerAngles;
        angles.x = 0;

        if (Mathf.Abs(horizontalAxis) > 0.5f)
        {
            angles.y = 90;
            if (horizontalAxis < 0)
                angles.y = -90;
        }
        transform.localEulerAngles = angles;
        float horizVel = 0;
        anim.SetBool("Sliding", false);
        if (state.IsName("Run") && anim.GetBool("Jump"))
            anim.Play("Jump");
        if (hit.transform.gameObject.layer == 6 && hit.distance < 2)
        {
            if (Time.realtimeSinceStartup > slideTime + 0.1f)
            {
                speed += Time.deltaTime*3;
                anim.SetBool("Sliding", true);
                if (!state.IsName("Slide"))
                    anim.Play("Slide");
                transform.LookAt(transform.position + hit.transform.forward);
                horizVel = hit.transform.forward.x * 3 * speed;
            }
        }
        else
        {
            speed = 0;
            slideTime = Time.realtimeSinceStartup;
        }

        bool run = (running);
        anim.SetBool("Run", run);
        anim.SetFloat("Raycast", hit.distance);
        anim.SetBool("StairClimb", false);
        anim.SetBool("GrabWall", false);
        anim.SetBool("GrabDown", jump);
        anim.SetBool("Push", false);
        if (state.IsName("Walk") && run)
            anim.Play("Run");

        if (Physics.Raycast(transform.position + Vector3.up + transform.forward, Vector3.down, out hit, 4, layers))
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1) && hit.distance < 2)
            {
                wallPos = hit.point;
                anim.SetBool("GrabWall", true);
            }

        }
        if (Physics.Raycast(transform.position+Vector3.up, transform.forward, out hit, 1, layers))
        {
            if (hit.distance < 1 && hit.transform.gameObject != gameObject)
            {
                Debug.Log("COLLIDED WITH " + hit.transform.gameObject.name);
                anim.SetFloat("ForwardRaycast", hit.distance);
                anim.SetBool("Push", hit.distance < 1 && hit.transform.gameObject.GetComponent<Rigidbody>() && Input.GetAxis("Horizontal") != 0);

            }
        }
        else
        {
            anim.SetFloat("ForwardRaycast", 200);
            anim.SetBool("Push", false);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 4, layers))
            {
                anim.SetBool("StairClimb", hit.distance < 1 || (state.IsName("ClimbStairs")) && hit.distance < 5);
            }

        }
        
        if (anim.GetFloat("Raycast") < 0.4f || state.IsName("JumpGrab"))
        {
            Vector3 vel = rb.velocity;
            if (horizVel == 0)
                vel.x = transform.forward.x * walkSpeed;
            else
                vel.x = horizVel;
            if (state.IsName("Run"))
                vel.x *= 3;
            rb.velocity = vel;

        }
        else if (Time.realtimeSinceStartup < runTime)
        {
            Vector3 vel = rb.velocity;
                vel.x = transform.forward.x * 6;
  
            rb.velocity = vel;
        }
    }
}
