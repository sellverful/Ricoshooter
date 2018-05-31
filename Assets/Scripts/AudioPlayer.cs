using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	static AudioPlayer instance = null;
	public AudioClip[] audioClips;
	public ArrayList a = new ArrayList();
	private AudioSource au;

	// Use this for initialization
	void Start () {
		au = GetComponent<AudioSource> ();
		if (a.Count != 0) {
			for (int i = 0; i < audioClips.Length; i++) {
				a.Add(audioClips[i]);
			}

		}

	}
	void OnDisable(){
		au.Stop ();
	}
	// Update is called once per frame
	void Update () {
		if (!au.isPlaying && a.Count>0) {
			int clip = Random.Range (0, a.Count);
			au.PlayOneShot (a[clip] as AudioClip);
			a.RemoveAt (clip);
		} 
		else if(a.Count==0){
			for (int i = 0; i < audioClips.Length; i++) {
				a.Add(audioClips[i]);
			};

		}
	}
}
