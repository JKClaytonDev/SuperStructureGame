using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCamera : MonoBehaviour
{
    Camera c;
    // Start is called before the first frame update
    void Start()
    {
        c = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angles = transform.localEulerAngles;
        angles.y = -c.transform.position.x/5;
        transform.localEulerAngles = angles;
    }
}
