using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	Animator anim;
	public int life = 5;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//Enemyのアニメーションを再生する
		if (life == 0) 
		{
			FallAnimation ();
			Invoke("StandAnimation",10);
			life = 5;
		}
	}

	//EnemyのHPが減る
	public void Damage(){
		if (life > 0) {
			life -= 1;
		}
	}

	void FallAnimation(){
		anim.SetBool ("Enemy", false);
	}

	void StandAnimation(){
		anim.SetBool ("Enemy", true);
	}
}