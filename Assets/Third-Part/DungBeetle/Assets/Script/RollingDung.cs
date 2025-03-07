using UnityEngine;
using System.Collections;

public class RollingDung: MonoBehaviour {
	
	private Animator anim;
	int DungRolling;
	int Stop;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		DungRolling = Animator.StringToHash("DungRolling");
		Stop = Animator.StringToHash("Stop");
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Stop")) {  
				anim.SetBool (DungRolling, true); 
				anim.SetBool (Stop, false);                                      
			}
		} else if (Input.GetKeyUp (KeyCode.R)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("DungRolling")) {            //R to roll dung
				anim.SetBool (DungRolling, false); 
				anim.SetBool (Stop, true); 
			}
			}
		}
	}