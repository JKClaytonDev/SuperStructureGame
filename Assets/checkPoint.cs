using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class checkPoint : MonoBehaviour
{
    public float spawnDistance;
    public bool setCheckpoint;
    // Start is called before the first frame update
    private void Start()
    {
        spawnDistance = Vector3.Distance(transform.position, FindObjectOfType<PlayerScript>().transform.position);
        //GetComponent<BoxCollider>().enabled = ;
        if (setCheckpoint)
        {
            PlayerPrefs.SetFloat("X", transform.position.x);
            PlayerPrefs.SetFloat("Y", transform.position.y);
            PlayerPrefs.SetFloat("Z", FindObjectOfType<PlayerScript>().gameObject.transform.position.z);
            PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }
    }
    private void Update()
    {
        if (spawnDistance == 0)
            spawnDistance = Vector3.Distance(transform.position, FindObjectOfType<PlayerScript>().transform.position);

    }
    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = false;

    }
    private void OnTriggerExit(Collider other)
    {
        PlayerPrefs.SetFloat("X", other.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("Y", other.gameObject.transform.position.y);
        PlayerPrefs.SetFloat("Z", other.gameObject.transform.position.z);
        PlayerPrefs.Save();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (other.transform.position.x > transform.position.x && spawnDistance > 15) {
            sceneIndex++;
        //else if (other.transform.position.x < transform.position.x && spawnDistance < 15)
        //    sceneIndex--;
        PlayerPrefs.SetInt("Scene", sceneIndex);
        PlayerPrefs.Save();
        
        if (sceneIndex != SceneManager.GetActiveScene().buildIndex)
        SceneManager.LoadScene(sceneIndex);
        }
    }

}
