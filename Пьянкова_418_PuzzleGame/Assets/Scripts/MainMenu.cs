using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	/* Завантаження ігрової карти*/
	public void PlayGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
	/* Розпочати нову гру*/
	public void ResetGame() {
		PlayerPrefs.DeleteAll();
	}
	/*Вихід з гри*/
	public void ExiteGame() {
		Application.Quit();
	}
}
