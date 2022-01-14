using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnimatorCutscene : MonoBehaviour
{
    Animator anim;
    AnimationEvent evt;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        evt = new AnimationEvent();
        evt.functionName = "nextScene";
    }
    void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Space", Input.GetKey(KeyCode.Space));
    }
}
