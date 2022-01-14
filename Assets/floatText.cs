using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatText : MonoBehaviour
{
    public int pos;
    public GameObject player;
    public Vector3 localPos;
    private void Update()
    {
        if (player)
            transform.position = player.transform.position + localPos;
    }
    public void moveText()
    {
        transform.position = player.transform.position + localPos;
    }
}
