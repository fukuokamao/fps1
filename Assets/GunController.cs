using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject Sparkle;
	public AudioClip Shotgun;
	AudioSource audioSource;

	void Start (){
		audioSource = GetComponent<AudioSource>();

	}

	// 銃口に火をつける
	void Fire(){
		GameObject obj =
		Instantiate (
			Sparkle,
			transform.position,
			Quaternion.identity
		);

		Destroy (obj,0.2f);
	}


	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{	
			// 銃口に火をつける
			Fire();
			// 発射音を鳴らす
			audioSource.PlayOneShot(Shotgun,0.5f);

			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) 
			{
				//銃弾が当たった所で火をつける
				GameObject bullet =
					Instantiate(
						Sparkle,
						hit.point,
						Quaternion.identity
					);
				Destroy(bullet,0.2f);
			}
		}
	}
}
