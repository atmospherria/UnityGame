using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewScoreTable : MonoBehaviour
{
	private static ViewScoreTable m_instance;
	private const int LeaderboardLength = 10;
	
	

	public static ViewScoreTable _instance
	{
		get {
			if (m_instance == null) {
				m_instance = new GameObject("ViewScoreTable").AddComponent<ViewScoreTable>();
			}
			return m_instance;
		}
	}
	void Awake() {
		if (m_instance == null) {
			m_instance = this;
		} else if (m_instance != this)Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void SaveHighScore(string name, int score) {
		List<Scores> HighScores = new List<Scores>();

		int i = 1;
		while (i <= LeaderboardLength && PlayerPrefs.HasKey("Score" + i + "score")) {
			Scores temp = new Scores();
			temp.score = PlayerPrefs.GetInt("Score" + i + "score");
			temp.name = PlayerPrefs.GetString("Score" + i + "name");
			HighScores.Add(temp);
			i++;
		}
		if (HighScores.Count == 0) {
			Scores _temp = new Scores();
			_temp.name = name;
			_temp.score = score;
			HighScores.Add(_temp);
		} else {
			for (i = 1; i <= HighScores.Count && i <= LeaderboardLength; i++) {
				if (score > HighScores[i - 1].score) {
					Scores _temp = new Scores();
					_temp.name = name;
					_temp.score = score;
					HighScores.Insert(i - 1, _temp);
					break;
				}
				if (i == HighScores.Count && i < LeaderboardLength) {
					Scores _temp = new Scores();
					_temp.name = name;
					_temp.score = score;
					HighScores.Add(_temp);
					break;
				}
			}
		}

		i = 1;
		while (i <= LeaderboardLength && i <= HighScores.Count) {
			PlayerPrefs.SetString("Score" + i + "name", HighScores[i - 1].name);
			PlayerPrefs.SetInt("Score" + i + "score", HighScores[i - 1].score);
			i++;
		}

	}

	public List<Scores> GetHighScore() {
		List<Scores> HighScores = new List<Scores>();

		int i = 1;
		while (i <= LeaderboardLength && PlayerPrefs.HasKey("Score" + i + "score")) {
			Scores temp = new Scores();
			temp.score = PlayerPrefs.GetInt("Score" + i + "score");
			temp.name = PlayerPrefs.GetString("Score" + i + "name");
			HighScores.Add(temp);
			i++;
		}

		return HighScores;
	}
	public void ClearLeaderBoard() {
		PlayerPrefs.DeleteAll();
	}

	void OnApplicationQuit() {
		PlayerPrefs.Save();
	}
}
public class Scores
{
	public int score;
	public string name;

}