using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightCursorHandler : MonoBehaviour
{
    private string d;
    private RectTransform rect;
    public Image Cursor;
    public Sprite right;
    public Sprite wrong;
    public Sprite defaultsp;
    public bool passGet;
    public LeftCursorHandler lfh;

    public bool press;
    // Use this for initialization
    void Start()
    {
        rect = GetComponent<RectTransform>();
        //passGet = false;
        press = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && rect.anchoredPosition.y < -1.3f)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + 13);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && rect.anchoredPosition.y > -117)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y - 13);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && rect.anchoredPosition.x > 27)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - 13, rect.anchoredPosition.y);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && rect.anchoredPosition.x < 102)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + 13, rect.anchoredPosition.y);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && press == true)
        {
            if (passGet == true && lfh.passGet == true)
            {
                Cursor.sprite = right;
                //Debug.Log("Right " + passGet);
                press = false;
            }
            else
            {
                Cursor.sprite = wrong;
                //Debug.Log("Right " + passGet);
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
            Debug.Log("------PASS RIGHT");
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
            //passGet = false;
        }
    }
}
