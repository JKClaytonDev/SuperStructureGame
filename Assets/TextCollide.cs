using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextCollide : MonoBehaviour
{
    public string[] word;
    public GameObject text;
    Vector3 textPos;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    private IEnumerator OnTriggerEnter(Collider collision)
    {
        GetComponent<BoxCollider>().enabled = false;
        
        if (collision.gameObject.GetComponent<PlayerScript>())
        {
            for (int f = 0; f < word.Length; f++){
                textPos = collision.gameObject.transform.position;
                for (int i = 0; i < word[f].ToCharArray().Length; i++)
                {
                    cloneObject(word[f].ToCharArray()[i] + "", i, collision.gameObject);
                    yield return new WaitForSeconds(0.04f);
                }
                Debug.Log("LINE");
                yield return new WaitForSeconds(1.0f);
                foreach (floatText t in FindObjectsOfType<floatText>())
                {
                    if (t.gameObject.GetComponent<Rigidbody>())
                    {
                        if (t.GetComponent<Rigidbody>().velocity == new Vector3())
                        {
                            GameObject cloneText = t.gameObject;
                            cloneText.transform.position += new Vector3(0, 0, 1);
                            cloneText.transform.eulerAngles = new Vector3(0, 0, 0);
                            cloneText.GetComponent<Rigidbody>().velocity = Vector3.up * (1 + cloneText.GetComponent<floatText>().pos * 3) * 2;
                            Destroy(cloneText.GetComponent<floatText>());
                            Destroy(cloneText, 5);
                            cloneText.transform.parent = null;
                            cloneText.transform.eulerAngles = new Vector3(0, 0, 0);
                            if (f < word.Length - 1)
                                cloneText.transform.position += Vector3.up;
                        }
                    }
                }
            }
        }
        
        Debug.Log("DONE TYPING");
    }

    void cloneObject(string letter, float pos, GameObject collision)
    {
        GameObject cloneText = Instantiate(text);
        cloneText.GetComponent<floatText>().player = collision;
        cloneText.GetComponent<floatText>().localPos = new Vector3(0, 3, 0) + Vector3.right * (pos / 5);
        cloneText.transform.parent = null;
        cloneText.GetComponent<TextMesh>().text = letter;
        cloneText.transform.position = textPos + new Vector3(0, 3, 0) + Vector3.right * (pos / 5);
        cloneText.transform.eulerAngles = new Vector3(0, 0, 0);
        cloneText.GetComponent<Rigidbody>().velocity = new Vector3();
        cloneText.GetComponent<floatText>().moveText();


    }
}
