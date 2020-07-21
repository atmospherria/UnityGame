using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEventManager : MonoBehaviour
{
	string name = "";
	int score = 0;
	List<Scores> highscore;
	public GameObject inf;
	public Text row;

	void Start() {
		score = PlayerPrefs.GetInt("Score");

		highscore = new List<Scores>();
	}
	/* Завантаження ігрової карти*/
	public void PlayGame() {
		PlayerPrefs.SetInt("Score", 0);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	/*Вихід з гри*/
	public void ExiteGame() {
		Application.Quit();
	}
	/* Розпочати нову гру*/
	public void ResetGame() {
		ViewScoreTable._instance.ClearLeaderBoard();
	}


	public void onClickSettings() {
		inf.SetActive(true);
		highscore = ViewScoreTable._instance.GetHighScore();
		foreach (Scores _score in highscore) {
			row.text += _score.name + "\t\t" + _score.score+"\n";
		}
	}
		
}
