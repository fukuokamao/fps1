//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class HitController : MonoBehaviour {
//
//	public int hitpoint;
//	Vector2 position1 = GameObject.Find("HeadMarker").transform.position;
//	Vector2 position2 = hit.collider.gameObject.position;
//	int magnitude = (position1 - position2).magnitude;
//
//	// Use this for initialization
//	void Start () {
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		Hit();
//	}
//
//	public void Hit(){
//		if (magnitude < 3) {
//			hitpoint = 20;
//		} else if (magnitude < 10) {
//			hitpoint = 5;
//		} else {
//			hitpoint = 10;
//		}
//	}
//}