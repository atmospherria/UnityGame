using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEnter : MonoBehaviour
{
	public int level;
	public GameObject wall;
	
	void Start() {
		
	}
	public void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			Invoke("LoadLevel",1.5f);
			wall.SetActive(false);

		}
			
	}
	 void LoadLevel () {
		SceneManager.LoadScene(level);
	}
}
