using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    public GameObject player;
    public GameObject continueButton;
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Start()
    {
        Time.timeScale = 1;
        Debug.Log("HAS KEY " + PlayerPrefs.HasKey("X"));    
        Cursor.visible = true;
    }
    private void Update()
    {
        continueButton.gameObject.SetActive(PlayerPrefs.HasKey("X"));
    }
    public void newGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Part1");
        return;
        
    }
    public void loadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void checkPoint()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void quit()
    {
        Application.Quit();
    }
    public void continueGame(){
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));
        return;
        Vector3 pos = new Vector3();
        pos.x = PlayerPrefs.GetFloat("X");
        pos.y = PlayerPrefs.GetFloat("Y");
        pos.z = PlayerPrefs.GetFloat("Z");
        player.transform.position = pos;
        FindObjectOfType<CameraFollow>().setDistance();
    }
}
