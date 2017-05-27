using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {

	public GameObject sparkle;
	public AudioClip shotgun;
	public AudioClip reload;
	AudioSource audioSource;
	public Text[] shotbullet = new Text[2];
	public Text[] timer = new Text[1];
	public Text[] point = new Text[1];
	int bullet = 30;
	int bulletbox = 150;
	public int hitpoint = 0;
	public GameObject headmaker;
	float cooltime = 0;
	int sum = 0;
	public GameObject reticle;
	float gametime = 20;

	void Start (){
		audioSource = GetComponent<AudioSource>();
		shotbullet [0].text = "BulletBox : " + bulletbox;
		shotbullet [1].text = "Bullet : " + bullet + "/30";
	}

	// Update is called once per frame
	void Update (){
		gametime -= Time.deltaTime;
		if (gametime > 0.00f) {
			gametime = Mathf.Floor(gametime * 10)/10;
			timer [0].text = "Time : " + gametime + "S";
		}

		if (Input.GetMouseButtonDown (0) && bullet > 0) 
		{	
			//レティクルを表示する
//			Scope();
			cooltime += Time.deltaTime;
			if (cooltime >= 0.2f) {

				// 銃口に火をつける
				Fire ();

				//弾数を減らす
				Shot ();

				// 発射音を鳴らす
				audioSource.PlayOneShot (shotgun, 0.5f);

				Ray ray = new Ray (transform.position, transform.forward);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit)) {
					//銃弾が当たった所で火をつける
					GameObject bullet = Instantiate (sparkle, (hit.point - (transform.forward / 5)), Quaternion.identity);
					Destroy (bullet, 0.2f);

					EnemyController enemyCon = hit.collider.gameObject.transform.parent.GetComponent<EnemyController> ();
					if (enemyCon != null) {
						//EnemyのHPが減る
						enemyCon.Damage ();

						//得点が加算される
						float distance = Vector3.Distance (headmaker.transform.position, hit.point);

						if (distance < 0.1f) {
							hitpoint = 100;
						} else if (distance > 0.7f) {
							hitpoint = 30;
						} else {
							hitpoint = 50;
						}
						sum += hitpoint;
						point [0].text = "Pt : " + (sum);
					}
				}
				cooltime = 0;
			}
		}

		if (Input.GetKey (KeyCode.R)) 
		{
			//リロード機能
			Reload();

			//リロード音を鳴らす
			audioSource.PlayOneShot(reload,0.5f);
		}
	}
		
	// 銃口に火をつける
	void Fire(){
		GameObject fire = Instantiate (sparkle,transform.position,Quaternion.identity);
		Destroy (fire,0.2f);
	}

	//弾数を減らす
	void Shot(){
		bullet -= 1;
		shotbullet [1].text = "Bullet : " + bullet + "/30";
	}

	//リロード機能
	void Reload(){
		bulletbox = bulletbox - (30 - bullet);
		bullet = (30 - bullet) + bullet;
		shotbullet [0].text = "BulletBox : " + bulletbox;
		shotbullet [1].text = "Bullet : " + bullet + "/30";
	}

//	//レティクルを表示する
//	void Scope(){
//		GameObject scope = Instantiate (reticle,transform.position,Quaternion.identity);
//		Destroy (scope,1.0f);
//	}

	//得点が加算される
//	void Addpoint(){
//		RaycastHit hit;
//
//	}
}
