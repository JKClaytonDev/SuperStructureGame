using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAt : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        return;
        if (Mathf.Abs(transform.localEulerAngles.y) > 90)
        {
            transform.localEulerAngles = new Vector3();
        }
    }
}
