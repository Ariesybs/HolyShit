using UnityEngine;
using System.Collections;

public class DungBeetleExample : MonoBehaviour {
	private Animator anim;
	int Idle;
	int RollDung;
	int WalkFWD;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		Idle = Animator.StringToHash("Idle");
		RollDung = Animator.StringToHash("RollDung");
		WalkFWD = Animator.StringToHash("WalkFWD");
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {  
				anim.SetBool (RollDung, true); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (WalkFWD, false);
			}
		} else if (Input.GetKeyUp (KeyCode.R)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("RollDung")) {            //R to roll dung
				anim.SetBool (RollDung, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (WalkFWD, false);
			}
		} else if (Input.GetKeyDown (KeyCode.W)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				anim.SetBool (RollDung,false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (WalkFWD, true);
			}
		} else if (Input.GetKeyUp (KeyCode.W)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("WalkFWD")) {          //W to Walk forward
				anim.SetBool (RollDung, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (WalkFWD, false);
			}
			}
		}
	}