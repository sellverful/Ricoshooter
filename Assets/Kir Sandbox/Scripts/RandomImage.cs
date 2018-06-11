using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RandomImage : MonoBehaviour {
	public Image randomImage;
    public int size;
	public Sprite s0;
	public Sprite s1;
	public Sprite s2;
	public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public Sprite s7;
    public Sprite s8;
    public Sprite s9;
    public Sprite s10;
    public Sprite s11;
    public Sprite s12;
    public Sprite s13;
    public Sprite s14;
    public Sprite s15;
    public Sprite s16;
    public Sprite s17;
    public Sprite s18;
    public Sprite s19;
    public Sprite s20;
    public Sprite s21;
    public Sprite s22;
    public Sprite s23;
    public Sprite s24;

    public Sprite[] images;

    public Sprite p0;
    public Sprite p1;
    public Sprite p2;
    public Sprite p3;
    public Sprite p4;
    public Sprite p5;
    public Sprite p6;
    public Sprite p7;
    public Sprite[] pass_word;
    private bool pword;
    private SetPassword setpassword;
	// Use this for initialization
	void Start () {

        images = new Sprite[25];
        images [0] = s0;
		images [1] = s1;
		images [2] = s2;
		images [3] = s3;
        images [4] = s4;
        images [5] = s5;
        images [6] = s6;
        images [7] = s7;
        images [8] = s8;
        images [9] = s9;
        images [10] = s10;
        images [11] = s11;
        images [12] = s12;
        images [13] = s13;
        images [14] = s14;
        images [15] = s15;
        images [16] = s16;
        images [17] = s17;
        images [18] = s18;
        images [19] = s19;
        images [20] = s20;
        images [21] = s21;
        images [22] = s22;
        images [23] = s23;
        images [24] = s24;
        pword = false;
        changeImage();
        setpassword = GameObject.FindGameObjectWithTag("PWRD").GetComponent<SetPassword>();
        Image[] pw1 = setpassword.pw1;
        Image[] pw2 = setpassword.pw2;

        pass_word = new Sprite[8];
        pass_word[0] = p0;
        pass_word[1] = p1;
        pass_word[2] = p2;
        pass_word[3] = p3;
        pass_word[4] = p4;
        pass_word[5] = p5;
        pass_word[6] = p6;
        pass_word[7] = p7;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	public void changeImage()
	{
		int num = UnityEngine.Random.Range(0, images.Length - 1);
		randomImage.sprite = images[num];
        Invoke("changeImage", 1f);
        transform.gameObject.tag = "DaImage";
        if (pword == true)
        {
            pword = false;
        }
	}
    public void changeToPass(int index)
    {
        randomImage.sprite = pass_word[index];
        CancelInvoke("changeImage");
        Invoke("changeImage", 5f);
        if (pword == false)
        {
            pword = true;
        }
    }
    public void changeToPass1(int index)
    {
        randomImage.sprite = pass_word[index];
        CancelInvoke("changeImage");
        Invoke("changeImage", 5f);
        transform.gameObject.tag = "password1";
        if (pword == false)
        {
            pword = true;
        }
    }
    public void changeToPass2()
    {
        randomImage.sprite = images[4];
        CancelInvoke("changeImage");
        Invoke("changeImage", 5f);
        transform.gameObject.tag = "password2";
        if (pword == false)
        {
            pword = true;
        }
    }
    /*private void OnTriggerStay(Collider collision)
    {
        
        
        if(collision.gameObject.tag == "LeftCursor" || collision.gameObject.tag == "RightCursor")
        {
            if (randomImage.sprite == pass)
            {
                Debug.Log("Oh, hi Mark");
            }
        }
    }*/
}
