using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


	public float moveSpeed;
    private float moveSpeedTMP;
    private bool dashActive;
    public bool invincible;
    private Rigidbody myRig;
	public GameObject RestartUI;
	public GameObject unityChan;
	private Vector3 moveInput;
	private Vector3 moveVelocity;
    private bool dashCooldown;


    /*//added ->//
    public GameObject minigame;
    //<- added//*/

    private Camera mainCamera;
    private GameObject particleS;
    //added
    public Image[] healthImages;
	public Sprite[] healthSprites;
    public int score = 0;
	//added

	public Gun gun;
	public bool deflect = false;
	public float deflectCooldown = 2f;
	private float deflectCooldownTime = 0f;
    [HideInInspector]
    public float timeScore = 0;
    PauseMenu pause;

	//movement

	Animator anim;
	Vector3 camForward;
	Vector3 move;
	Vector3 moveInputCam;
	float forwardAmount;
	float turnAmount;

	//health
	//added

	public int startHearts = 3;
	public int curHealth;

	public bool undead = false;
	private int maxHealth;
	private int maxHeartAmount = 3;
	private int healthPerHeart = 1;
	private float distToGround;
	//added
	//public int hp = 3;
	public bool dead = false;
	void Start () {
		myRig = GetComponent<Rigidbody> ();
		mainCamera = FindObjectOfType<Camera> ();
		anim = GetComponentInChildren<Animator> ();
        particleS = GetComponentInChildren<ParticleSystem>().gameObject;
        particleS.SetActive(false);
        //added
        curHealth = startHearts * healthPerHeart;
		maxHealth = maxHeartAmount * healthPerHeart;
		checkHealthAmount ();
        moveSpeedTMP = moveSpeed;
        //added
        pause = GameObject.FindGameObjectWithTag("pause").GetComponent<PauseMenu>();
		distToGround = GetComponent<Collider> ().bounds.extents.y;
        /*//added ->//
        minigame.SetActive(false);
        //<- added//*/
    }
    //added
    void checkHealthAmount(){
		if (healthImages [0] == null)
			return;
		for(int i = 0; i < maxHeartAmount; i++){
			if (startHearts <= i) {
				healthImages [i].enabled = false;
			} else {
				healthImages [i].enabled = true;
			}
		}
		UpdateHearts ();
	}
	//added
	void UpdateHearts(){
		if (healthImages[0] == null)
			return;
		bool empty = false;
		int i = 0;
		foreach (Image image in healthImages) {
			if (empty) {
				image.sprite = healthSprites [0];
			} 
			else {
				i++;
				if (curHealth >= i * healthPerHeart) {
					image.sprite = healthSprites [healthSprites.Length - 1];
				} 
				else {
					int currentHeartHP = (int)(healthPerHeart - (healthPerHeart * i - curHealth));
					int healthPerImage = healthPerHeart; /// (healthSprites.Length - 1);
					int imageIndex = currentHeartHP / healthPerImage;
					image.sprite = healthSprites [imageIndex];
					empty = true;
				}
			}
		}
	}
    void HealHearts()
    {
        if (healthImages[0] == null || curHealth==3)
            return;
        bool empty = false;
        int i = 0;
        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[2];
                return;
            }
            else
            {
                i++;
                int currentHeartHP = (int)(healthPerHeart - (healthPerHeart * i - curHealth));
                int healthPerImage = healthPerHeart; /// (healthSprites.Length - 1);
                int imageIndex = currentHeartHP / healthPerImage;
                image.sprite = healthSprites[imageIndex];
                empty = true;
            }
        }
    }
    //added
    // Update is called once per frame
    Vector3 rememberPosition;
	void Update () {
        timeScore += Time.deltaTime;
        if (!dead) {
			MovePlayer ();
		}
		GodMode ();
		RotatePlayer ();
		Deflect ();
	}
    void RemainDead()
    {
        
    }
		//moving player
	void MovePlayer(){
		moveInput = new Vector3 (Input.GetAxisRaw("Horizontal"),0f, Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonDown("Fire2"))
        {
            OnDashInput();
        }
        moveVelocity = moveInput * moveSpeed;
		RightMovement ();
	}
	void GodMode(){
		if (Input.GetButtonDown ("Undead")) {
			undead = !undead;
		}
	}
	//Rotate player where mouse is
	void RotatePlayer(){
		if (!PauseMenu.GameIsPaused || dead) {
			Ray cameraRay = mainCamera.ScreenPointToRay (Input.mousePosition);
			Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
			float rayLength;
			if (groundPlane.Raycast (cameraRay, out rayLength)) {
				Vector3 pointToLook = cameraRay.GetPoint (rayLength);
				Debug.DrawLine (cameraRay.origin, pointToLook, Color.blue);
				transform.LookAt (new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
			}
		}
	}


	//Move in Right Direction
	void RightMovement(){
		if (!PauseMenu.GameIsPaused || dead) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical"); 
			camForward = Vector3.Scale (mainCamera.transform.up, new Vector3 (1, 0, 1)).normalized;
			move = vertical * camForward + horizontal * mainCamera.transform.right;

			if (move.magnitude > 1) {
				move.Normalize ();
			}
			RightMovementConvert (move);
		}
	}
	void RightMovementConvert(Vector3 move){
		if (!PauseMenu.GameIsPaused || dead == false) {
			this.moveInputCam = move;

			ConvertMoveInput ();
			UpdateAnimator ();
		}
	}
	void ConvertMoveInput(){
		if (!PauseMenu.GameIsPaused || dead) {
			Vector3 localMove = transform.InverseTransformDirection (moveInputCam);
			turnAmount = localMove.x;
			forwardAmount = localMove.z;
		}
	}
	void UpdateAnimator(){
        if (dead)
        {
            anim.SetTrigger("Dead");
            return;
        }
        if (!PauseMenu.GameIsPaused || dead) {
			anim.SetFloat ("Forward", forwardAmount, 0.1f, Time.deltaTime);
			anim.SetFloat ("Turn", turnAmount, 0.1f, Time.deltaTime);
		}
        
	}
	//------------------
	void OnTriggerEnter(Collider col){
		if (col.tag == "Door" && gun.currShootType==Gun.ShootType.Laser) {
			gun.ammoLazer--;
			gun.currShootType = Gun.ShootType.Gun;
		}
        if (col.tag == "Checkpoint")
        {
            GameObject.FindGameObjectWithTag("CheckpointManager").SendMessage("ChangeCheckPoint", col.transform);
        }
        /*//added ->//
        if(col.tag == "Terminal" && Input.GetKeyUp(KeyCode.E)){
            minigame.SetActive(true);
        }
        //<- added//*/
    }

    //deflect 
    void Deflect(){
		//deflectCooldownTime -= Time.deltaTime;
		//if (Input.GetButtonDown ("Deflect")) {
		//	if (deflectCooldownTime <= 0) {
		//		deflectCooldownTime = deflectCooldown;
		//		deflect = true;
		//		moveVelocity = Vector3.zero;
		//		Invoke ("DeflectFalse", deflectCooldown);
		//	}
		//} else if (Input.GetButtonUp ("Deflect")) {
		//	deflectCooldownTime = 0;
		//	deflect = false;
		//	CancelInvoke ();
		//}

	}
	void DeflectFalse(){
		deflect = false;
	}

    //added public
    public void DealDamage(int damage){
		if (undead || invincible)
			return;
		curHealth -= damage;
		StartCoroutine (Undead ());
		curHealth = Mathf.Clamp (curHealth, 0, startHearts * healthPerHeart);
		UpdateHearts ();
		if (curHealth <= 0) {
            dead = true;
            rememberPosition = transform.position;
            anim.SetTrigger("Dead");
            deflect = true;
            StartCoroutine(Restart());
            myRig.velocity = Vector3.zero;
			enabled = false;
			pause.enabled = false;
		}
	}
    void Heal(int heal)
    {
        if (dead || curHealth == 3)
            return;
        curHealth += heal;
        HealHearts();
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        RestartUI.SetActive(true);
        Time.timeScale = 0f;
    }
	IEnumerator Undead(){
		undead = true;
		StartCoroutine (Blicking ());
		yield return new WaitForSeconds (2.0f);
		undead = false;
	}
	IEnumerator Blicking(){
		Vector3 oldScale = unityChan.transform.localScale;
		while (undead) {
			unityChan.transform.localScale = new Vector3(0,0,0);
			yield return new WaitForSeconds (0.1f);
			unityChan.transform.localScale = oldScale;
			yield return new WaitForSeconds (0.1f);
		}
	}

    public void OnDashInput()
    {
        if (!dashCooldown)
        {
            StartCoroutine(DashCooldown());
            StartCoroutine(Dash());
        }

    }

    IEnumerator DashCooldown()
    {
        dashCooldown = true;
        yield return new WaitForSeconds(.6f);
        dashCooldown = false;
    }
    IEnumerator Dash()
    {
        moveSpeed = 75;
        dashActive = true;
        particleS.SetActive(true);
        //transform.GetComponent<CapsuleCollider>().enabled = false;
        invincible = true;
        yield return new WaitForSeconds(.1f);
        moveSpeed = moveSpeedTMP;
        dashActive = false;
        particleS.SetActive(false);
        //transform.GetComponent<CapsuleCollider>().enabled = true;
        invincible = false;

    }
    void FixedUpdate(){
		myRig.velocity = moveVelocity; 
	}
}
