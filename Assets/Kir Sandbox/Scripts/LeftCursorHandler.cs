using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftCursorHandler : MonoBehaviour {
    private string d;
    private RectTransform rect;
    public Image Cursor;
    public Sprite right;
    public Sprite wrong;
    public Sprite defaultsp;
    public bool passGet;
    public RightCursorHandler rch;
    public int countwin;
    public int countlose;

    public bool press;

    public SetPassword sp;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();

        countwin = 0;
        countlose = 0;
        //passGet = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.W) && rect.anchoredPosition.y < -1.3f)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y+13);
        }
        else if (Input.GetKeyUp(KeyCode.S) && rect.anchoredPosition.y > -117)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y - 13);
        }
        else if (Input.GetKeyUp(KeyCode.A) && rect.anchoredPosition.x > 27)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - 13, rect.anchoredPosition.y );
        }
        else if (Input.GetKeyUp(KeyCode.D) && rect.anchoredPosition.x < 102)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + 13, rect.anchoredPosition.y);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && press == true)
        {
            if (passGet == true && rch.passGet == true)
            {
                Cursor.sprite = right;
                countwin++;
                press = false;
                Debug.Log("wins  " + countwin);
                
            }
            else
            {
                Cursor.sprite = wrong;
                countlose++;
                
                Debug.Log("loses  " + countlose);
            }
            Invoke("changeBack", 1f);
        }
    }
    void changeBack()
    {
        Cursor.sprite = defaultsp;
    }
    private void OnTriggerStay(Collider collision)
    {

        if (collision.gameObject.tag == "password1" /*&& collision.gameObject.tag == "password2"*/)
        {
            passGet = true;
            Debug.Log("PASS LEFT------------");
        }
        else
        {
            passGet = false;
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "password1" /*&& collision.gameObject.tag == "password2"*/)
        {
            passGet = false;
        }
    }
}
