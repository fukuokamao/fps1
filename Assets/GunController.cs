using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {

	public GameObject Sparkle;
	public AudioClip shotgun;
	public AudioClip reload;
	AudioSource audioSource;
	public Text[] text = new Text[2];
	int Bullet = 30;
	int BulletBox = 150;

	void Start (){
		audioSource = GetComponent<AudioSource>();
		text [0].text = "BulletBox : " + BulletBox;
		text [1].text = "Bullet : " + Bullet + "/30";
	}

	// 銃口に火をつける
	void Fire(){
		GameObject fire = Instantiate (Sparkle,transform.position,Quaternion.identity);
		Destroy (fire,0.2f);
	}
		
	//弾数を減らす
	void Shot(){
		Bullet -= 1;
		text [1].text = "Bullet : " + Bullet + "/30";
	}

	//リロード機能
	void Reload(){
		BulletBox = BulletBox - (30 - Bullet);
		Bullet = (30 - Bullet) + Bullet;
		text [0].text = "BulletBox : " + BulletBox;
		text [1].text = "Bullet : " + Bullet + "/30";
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && Bullet > 0 )
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
				GameObject bullet = Instantiate(Sparkle,hit.point,Quaternion.identity);
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
