using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnim : MonoBehaviour
{
    public GameObject player;
    AnimationEvent enablePlayer;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("X"))
        {
            playerEnable();
        }
        enablePlayer = new AnimationEvent();
        enablePlayer.functionName = "playerEnable";
        anim = GetComponent<Animator>();
    }
    public void playerEnable()
    {
        player.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
