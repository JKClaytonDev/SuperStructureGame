using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePause : MonoBehaviour
{
    public GameObject child;
    bool toggle = false;
    private void Start()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    private void OnEnable()
    {
        transform.parent = null;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            

            Cursor.visible = !child.activeInHierarchy;
            
            child.SetActive(!child.activeInHierarchy);
        }
        Time.timeScale = 1f;
        if (child.activeInHierarchy)
        {
            Time.timeScale = 0.1f;
        }
    }
}
