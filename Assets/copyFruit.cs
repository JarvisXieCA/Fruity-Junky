using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyFruit : MonoBehaviour {
	public GameObject prefab;
	GameObject prefabClone;

	float[] heights = new float[] {-0.1f , 0.5f, 1.0f, 1.5f, 1.75f, 2f};
	void Start(){
		

	}

	void Update(){
		int ranNum = Random.Range(0,650);
		if (ranNum == 200 && GameObject.Find("Player").GetComponent<player>().gameStarted == true) {
			int h = Random.Range (0, 6);
			prefabClone = Instantiate (prefab, new Vector3(9.1f,heights[h],0f), Quaternion.identity) as GameObject;
			prefabClone.transform.localScale = new Vector3 (4.4f, 4.4f, 4.4f);
			prefabClone.AddComponent<fruitScript>();
			Rigidbody2D pfcRigidBody = prefabClone.AddComponent<Rigidbody2D>();
			pfcRigidBody.gravityScale = 0;
			Collider2D pfcCollider = prefabClone.AddComponent<BoxCollider2D> ();
		}
		if (prefabClone != null) {
			if(prefabClone.transform.localPosition.y < -20){
				Debug.Log ("deleted");
				Destroy (prefabClone);
			}
		}
	}

}
