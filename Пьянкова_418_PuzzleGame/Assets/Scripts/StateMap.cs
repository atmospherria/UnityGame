using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMap : MonoBehaviour
{
	public GameObject cameraPlayer;
	public GameObject cameraAll;
	bool visible;
	int levelComplete;
	// Start is called before the first frame update
	void Start()
    {
		levelComplete = PlayerPrefs.GetInt("LevelComplete");
		GameObject temp;
		switch (levelComplete) {
			case 0:
				visible = true;
				break;
			case 2:
				temp = GameObject.Find("L1");
				temp.SetActive(false);
			break;
			case 3:
				temp = GameObject.Find("L2");
				temp.SetActive(false);
				temp = GameObject.Find("L1");
				temp.SetActive(false);
				break;
			case 4:
				temp = GameObject.Find("L3");
				temp.SetActive(false);
				temp = GameObject.Find("L2");
				temp.SetActive(false);
				temp = GameObject.Find("L1");
				temp.SetActive(false);
				break;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(1)) {
			if (cameraPlayer.active) {
				cameraAll.active = true;
				cameraPlayer.active = false;
			} else {
				cameraAll.active = false;
				cameraPlayer.active = true;
			}
		}
        
    }
	// Підсказки по навчанню
	void OnGUI() {
		if (visible) {
			GUI.Box(new Rect((Screen.width - 300) / 2, (Screen.height - 300) / 2, 300, 300), "Навчання:");
			GUI.Label(new Rect((Screen.width - 300) / 2 + 5, (Screen.height - 300) / 2 + 20, 290, 250), "Привіт, все дуже просто:" +
				"\nУправління вібувається буквами клавіатури: A,W,D,S"+"\nВибирай пазл та пазл з яким потрібно поміняти місцями." +
				"\nОсновне завдання : Скласти пазл");
			if (GUI.Button(new Rect((Screen.width - 100) / 2 - 50, (Screen.height - 300) / 2 + 250, 100, 40), "Так")) {
				visible = false;
			}
		}
	}
}
