using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarController : MonoBehaviour {
	private Image ImageCooldown;
	public float cooldown = 5;
	bool isCooldown;
	void Start(){
		ImageCooldown = GetComponent<Image> ();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			Debug.Log ("Here");
			isCooldown = true;
		}
		if (isCooldown) {
			ImageCooldown.fillAmount -= 1 / cooldown * Time.deltaTime;

			if (ImageCooldown.fillAmount <= 0) {
				ImageCooldown.fillAmount = 1;
				isCooldown = false;
				gameObject.SetActive (false);
			}
		}
	}
}
