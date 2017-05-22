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

	void Start (){
		audioSource = GetComponent<AudioSource>();
		shotbullet [0].text = "BulletBox : " + bulletbox;
		shotbullet [1].text = "Bullet : " + bullet + "/30";
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

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && bullet > 0 )
		{	
			// 銃口に火をつける
			Fire();

			//弾数を減らす
			Shot();

			// 発射音を鳴らす
			audioSource.PlayOneShot(shotgun,0.5f);

			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) 
			{
				//銃弾が当たった所で火をつける
				GameObject bullet = Instantiate(sparkle,hit.point,Quaternion.identity);
				Destroy(bullet,0.2f);
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
}
