using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fruitScript : MonoBehaviour {
	public GameObject player;
	public AudioSource fruitSound;


	void OnCollisionEnter2D ( Collision2D collisionInfo){

		if (collisionInfo.collider.name == "Player") {
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = Resources.Load("getFruit") as AudioClip;
			audioSource.Play();
			player.GetComponent<player>().score = player.GetComponent<player>().score + 10;
			if (player.GetComponent<player> ().score % 100 == 0) {
				player.GetComponent<player> ().objectSpeed = player.GetComponent<player> ().objectSpeed + 1f;
				GameObject.Find ("SpeedUp").GetComponent<Text>(). enabled = true;
				player.GetComponent<player>().executedTime = Time.time;
				player.GetComponent<player>().speedUpSoundPlay();
			}
			player.GetComponent<player>().scoreText.text = "Score: "+player.GetComponent<player>().score.ToString();
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player"); 
	}

	// Update is called once per frame
	void Update () {
		float tiltAroundZ = Input.GetAxis("Horizontal") ;
		float tiltAroundX = Input.GetAxis("Vertical") ;

		Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

		// Dampen towards the target rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 0.1f);
		if (player.GetComponent<player>().rightWall == true) {
			transform.Translate (Vector2.right * (player.GetComponent<player>().objectSpeed+1.5f) * Time.deltaTime);
		}else if (player.GetComponent<player>().leftWall == true) {
			transform.Translate (Vector2.right * (player.GetComponent<player>().objectSpeed-1.5f) * Time.deltaTime);
		}
		else {
			transform.Translate (Vector2.right * player.GetComponent<player>().objectSpeed * Time.deltaTime);
		}

		transform.eulerAngles = new Vector2 (0, -180);

		if(gameObject.transform.localPosition.x < -20){
			Destroy (gameObject);
		}

	}
}
