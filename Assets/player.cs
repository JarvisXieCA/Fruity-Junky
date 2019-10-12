using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class player : MonoBehaviour {

	public AudioSource bgm;
	public AudioSource jumpSound;
	public AudioSource walkSound;
	public AudioSource spaceSound;
	public AudioSource fruitSound;
	public AudioSource foodSound;
	public AudioSource gameOverSound;
	public AudioSource speedUpSound;

	Animator ani;
	public bool leftWall;
	public bool rightWall;
	public float jumpForce;
	public bool isGrounded;
	Rigidbody2D rb;

	public int state;
	public int score;
	public float objectSpeed;
	public Text scoreText;
	public bool gameStarted;
	public bool gameOver;

	public float currentTime = 0.0f, executedTime = 0.0f, timeToWait = 1.0f;//for the SpeedUp text
	// Use this for initialization

	public void gameOverSoundPlay(){
		gameOverSound.Play ();
	}

	public void speedUpSoundPlay(){
		speedUpSound.Play ();
	}

	void Start () {
		GameObject.Find ("SpeedUp").GetComponent<Text>().enabled = false;
		objectSpeed = 3f;
		bgm.loop = true;
		bgm.Play ();
		gameOver = false;
		gameStarted = false;
		Time.timeScale = 0;
		ani = GetComponent<Animator> ();
		leftWall = false;
		rightWall = false;
		state = 0;
		score = 0;
		rb = GetComponent<Rigidbody2D>();
		scoreText.text = "Score: 0";
		GameObject.Find ("Muted").GetComponent<Text>().enabled = false;
	}

	// Update is called once per frame
	void OnCollisionEnter2D ( Collision2D collisionInfo){
		if (collisionInfo.collider.name == "burger(Clone)" || collisionInfo.collider.name == "chicken(Clone)" || collisionInfo.collider.name == "fries(Clone)" || collisionInfo.collider.name == "hotdog(Clone)" || collisionInfo.collider.name == "pizza(Clone)" || collisionInfo.collider.name == "popcorn(Clone)") {
			foodSound.Play ();
		} else if(collisionInfo.collider.name == "apple(Clone)" || collisionInfo.collider.name == "banana(Clone)" || collisionInfo.collider.name == "cherry(Clone)" || collisionInfo.collider.name == "grape(Clone)" || collisionInfo.collider.name == "kiwi(Clone)"|| collisionInfo.collider.name == "pear(Clone)"|| collisionInfo.collider.name == "pineapple(Clone)"|| collisionInfo.collider.name == "strawberry(Clone)"|| collisionInfo.collider.name == "watermelon(Clone)"){
			fruitSound.Play ();
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.M)){
			 AudioListener.pause = !AudioListener.pause;
			 GameObject.Find ("Muted").GetComponent<Text>().enabled = !GameObject.Find ("Muted").GetComponent<Text>().enabled;
		}
		currentTime = Time.time;
		if(executedTime != 0.0f)
		{
			if(currentTime - executedTime > timeToWait)
			{
				GameObject.Find ("SpeedUp").GetComponent<Text>().enabled = false;
				executedTime = 0.0f;

			}
		}
		if (gameOver) {
			if(Input.GetKey("space")){
				SceneManager.LoadScene( SceneManager.GetActiveScene().name);
			}
		}
		if (!gameStarted) {
			if (Input.GetKeyDown ("space")&&!gameOver) {
				spaceSound.Play ();
				if (bgm.isPlaying == false) {
					bgm.Play ();
				}
				Time.timeScale = 1;
				GameObject.Find ("instruction").GetComponent<Text>(). enabled = false;
				gameStarted = true;
			}
		} else {
			if (Input.GetKeyDown ("space")) {
				spaceSound.Play ();
				bgm.Pause ();
				Time.timeScale = 0;
				GameObject.Find ("instruction").GetComponent<Text>(). enabled = true;
				gameStarted = false;
			}
		}
		if ((gameObject.transform.position.y <= 0.7&&state==3) ||(gameObject.transform.position.y <= 0.5&&state==2)||(gameObject.transform.position.y <= 0.4&&state==1) ||gameObject.transform.position.y <= 0.25) {
			isGrounded = true;
			//Debug.Log ("isGrounded: true");
		} else {
			isGrounded = false;
			//Debug.Log ("isGrounded: false");
		}

		if((Input.GetKey("up")||Input.GetKey(KeyCode.W)) && isGrounded){
			jumpSound.Play ();
			rb.AddForce(new Vector2(0f, jumpForce));
			ani.SetTrigger ("Jump");
			isGrounded = false;
		}
		leftWall = false;
		rightWall = false;
		Movement ();

		{
			float move = Input.GetAxis ("Horizontal");
			if (!(Input.GetKey ("right")||Input.GetKey(KeyCode.D)) && !(Input.GetKey ("left")||Input.GetKey(KeyCode.A))) {
				ani.SetBool ("Moving", false);
			}
		}

			
	}

	void Movement(){
		float walkSpeed = 0f;
		if (state == 0) {
			walkSpeed = 3f;
		} else if (state == 1) {
			walkSpeed = 2.3f;
		} else if (state == 2) {
			walkSpeed = 1.8f;
		} else if (state == 3) {
			walkSpeed = 1.6f;
		}
		if ( (Input.GetKey ("right")||Input.GetKey(KeyCode.D)) && gameStarted) {
			if (isGrounded) {
				walkSound.Play ();
			}
			if (gameObject.transform.position.x < 0) {
				transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
				transform.eulerAngles = new Vector2 (0, 0);
				ani.SetBool ("Moving", true);
			} else {
				if (GameObject.Find ("BrickHouse").transform.localScale.x < 5f) {
					GameObject.Find ("BrickHouse").transform.localScale += new Vector3 (0.001F, 0.001F, 0f);
				}
				rightWall = true;
			}
		}
		if ((Input.GetKey ("left")||Input.GetKey(KeyCode.A)) && gameStarted) {
			if (isGrounded) {
				walkSound.Play ();
			}

			if (gameObject.transform.position.x > -7.5) {
				transform.Translate (Vector2.right * walkSpeed * Time.deltaTime);
				transform.eulerAngles = new Vector2 (0, -180);
				ani.SetBool ("Moving", true);
			} else {
				if (GameObject.Find ("BrickHouse").transform.localScale.x > 1) {
					GameObject.Find("BrickHouse").transform.localScale += new Vector3(-0.0012F, -0.001F,0f);
				}
				leftWall = true;
			}
		}
	}


}
