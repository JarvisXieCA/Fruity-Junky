using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class foodScript : MonoBehaviour {
	public GameObject player;

	void OnCollisionEnter2D ( Collision2D collisionInfo){

		if (collisionInfo.collider.name == "Player") {
			if (player.GetComponent<player> ().state <= 2) {
				player.transform.localScale += new Vector3(3F, 3F,0f);
				if (player.GetComponent<player> ().state == 0) {
					Destroy(GameObject.Find("life0"));
				}else if (player.GetComponent<player> ().state == 1) {
					Destroy(GameObject.Find("life1"));
				}else if (player.GetComponent<player> ().state == 2) {
					Destroy(GameObject.Find("life2"));
				}
				player.GetComponent<player> ().state = player.GetComponent<player> ().state + 1;

			} else {
				player.transform.localScale += new Vector3(3F, 3F,0f);
				player.GetComponent<player> ().gameStarted = false;
				GameObject.Find ("instruction").GetComponent<Text>().text = "GAME OVER\nYour Score Was: "+player.GetComponent<player> ().score.ToString()+"\n\n Press SPACE to restart the game";
				GameObject.Find ("instruction").GetComponent<Text>(). enabled = true;
				player.GetComponent<player> ().gameOver = true;
				player.GetComponent<player>().gameOverSoundPlay();
				Time.timeScale = 0;

			}
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
			transform.Translate (Vector2.right * (player.GetComponent<player>().objectSpeed-1.5f)* Time.deltaTime);
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
