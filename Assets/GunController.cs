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
	int bullet = 30;
	int bulletbox = 150;
	public int hitpoint;
	public GameObject headmaker;
	float cooltime = 0;

	void Start (){
		audioSource = GetComponent<AudioSource>();
		shotbullet [0].text = "BulletBox : " + bulletbox;
		shotbullet [1].text = "Bullet : " + bullet + "/30";
	}

	// Update is called once per frame
	void Update (){
		if (Input.GetMouseButtonDown (0) && bullet > 0) 
		{	
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

						float distance = Vector3.Distance (headmaker.transform.position, hit.point);

						if (distance < 0.1f) {
							hitpoint = 100;
							print (hitpoint);
						} else if (distance > 0.7f) {
							hitpoint = 30;
							print (hitpoint);
						} else {
							hitpoint = 50;
							print (hitpoint);
						}
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
}
