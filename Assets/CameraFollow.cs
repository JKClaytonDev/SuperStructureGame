using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 startPos;
    public GameObject player;
    public float shake;
    public float shakeTime;
    Vector3 angles;
    public float distance = 25;
    private void Start()
    {
        startPos = transform.position;
        angles = transform.localEulerAngles;
        Vector3 pos = player.transform.position;
        pos.z = transform.position.z;
        //pos.y = (pos.y + startPos.y)/2;
        pos.y += 3;
        transform.position = pos;
        distance = 2555555;
    }
    public void setDistance()
    {
        Vector3 pos = player.transform.position;
        pos.z = transform.position.z;
        distance = 2555555;// Mathf.Max(25, Vector3.Distance(transform.position, pos));
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeTime > Time.realtimeSinceStartup && shakeTime < Time.realtimeSinceStartup + 0.1f)
        {
            shakeTime = 0;
            shake = 5;
        }
        Vector3 pos = player.transform.position;
        pos.z = transform.position.z;
        //pos.y = (pos.y + startPos.y)/2;
        pos.y += 3;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * distance);
        if (shake < 0.5)
        {
            if (shake != 0)
                transform.localEulerAngles = angles;
            shake = 0;
        }
        else {
        transform.localEulerAngles = new Vector3() + new Vector3(0, Random.Range(-shake, shake), 0);
        shake /= 1 + Time.deltaTime * 2;
    }

    }
}
