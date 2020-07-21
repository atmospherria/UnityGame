using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzlesBoard : MonoBehaviour
{
	public int m_size;
	public List<GameObject> m_puzzlePiece;
	public float timeStart = 60;
	public float randomeTime = 2;
	public Text textTime;
	public GameObject winPanel;
	public GameObject puzzlesArea;
	public GameObject losePanel;
	public Toggle isPaint;
	public Toggle isGlue;
	PuzzleSection[,] m_puzzle;
	PuzzleSection m_puzzleSelection;
	public int m_random;
	public int CurrentNumber;
	int levelComplete;
	int sceneIndex;
	int score;
	public Text textScore;
	//bool visible;
	// Use this for initialization
	void Start() {
		score = PlayerPrefs.GetInt("Score");
		textScore.text = "Score" + score;
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
		levelComplete = PlayerPrefs.GetInt("LevelComplete");
		switch (sceneIndex) {
			case 2:
				isPaint.enabled = false;
				isGlue.enabled = false;
				//visible = true;
				break;
			case 3:
				isPaint.enabled = false;
				if (score > 10) {
					isGlue.enabled = true;
				}
				
				break;
			case 4:
				if (score > 20) {
					isPaint.enabled = true;
				}
				isGlue.enabled = true;
				break;
		}
		textTime.text = timeStart.ToString();
		GameObject temp;
		m_puzzle = new PuzzleSection[m_size, m_size];
		for (int i = 0; i < m_size; i++) {
			for (int j = 0; j < m_size; j++) {
				temp = Instantiate(m_puzzlePiece[CurrentNumber], new Vector2(i * 500 / m_size, j * 500/ m_size), Quaternion.identity);
				temp.transform.SetParent(transform);
				m_puzzle[i, j] = (PuzzleSection)temp.GetComponent("PuzzleSection");
				m_puzzle[i, j].CreatePuzzlePiece(m_size);
			}
		}

		SetupBoard();
		RandomizePlacement();
		
	}

	void Update() {
		timeStart -= Time.deltaTime;
		if (isGlue.isOn) {
			isGlue.interactable = false;
		}
		if (timeStart == randomeTime && !isGlue.isOn) {
			RandomizePlacement();
		} else if (timeStart < 0) {
			Lose();
		}
		textTime.text = Math.Round(timeStart).ToString();
		if (isPaint.isOn){
			isPaint.interactable = false;
			SetupBoard();
			Invoke("Win", 1.5f);
		}
		onClickLoadMenu();
	}
	void onClickLoadMenu() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("Menu");
		}
	}

	private void RandomizePlacement() {
		VectorInt2[] puzzleLocation = new VectorInt2[2];
		Vector2[] puzzleOffset = new Vector2[2];
		do {
			for (int i = 0; i < m_random; i++) {
				puzzleLocation[0].x = UnityEngine.Random.Range(0, m_size);
				puzzleLocation[0].y = UnityEngine.Random.Range(0, m_size);
				puzzleLocation[1].x = UnityEngine.Random.Range(0, m_size);
				puzzleLocation[1].y = UnityEngine.Random.Range(0, m_size);

				puzzleOffset[0] = m_puzzle[puzzleLocation[0].x, puzzleLocation[0].y].GetImageOffset();
				puzzleOffset[1] = m_puzzle[puzzleLocation[1].x, puzzleLocation[1].y].GetImageOffset();

				m_puzzle[puzzleLocation[0].x, puzzleLocation[0].y].AssignImage(puzzleOffset[1]);
				m_puzzle[puzzleLocation[1].x, puzzleLocation[1].y].AssignImage(puzzleOffset[0]);

			}
		} while (CheckBoard() == true);
		

	}
	public void SetupBoard() {
		Vector2 offset;
		Vector2 m_scale = new Vector2(1f / m_size, 1f / m_size);
		for (int i = 0; i < m_size; i++) {
			for (int j = 0; j < m_size; j++) {
				offset = new Vector2(i * (1f / m_size), j * (1f / m_size));
				m_puzzle[i, j].AssignImage(m_scale, offset);
			}
		}
	}
	public PuzzleSection GetSelection() {
		return m_puzzleSelection;

	}
	public void SetSelection(PuzzleSection selection) {
		m_puzzleSelection = selection;
	}
	public bool CheckBoard() {
		for (int i = 0; i < m_size; i++) {
			for (int j = 0; j < m_size; j++) {
				if (m_puzzle[i, j].CheckGoodPlacement() == false)
					return false;

			}
		}
		return true;
	}
	public void Win() {
		if (levelComplete < sceneIndex) {
			score += 10;
			PlayerPrefs.SetInt("LevelComplete",sceneIndex);
			PlayerPrefs.SetInt("Score", score);
			textScore.text = "Score" + score;
		}
		winPanel.SetActive(true);
		puzzlesArea.SetActive(false);
	}

	public void Lose() {
		score -= 10;
		PlayerPrefs.SetInt("Score", score);
		textScore.text = "Score" + score;
		losePanel.SetActive(true);
		puzzlesArea.SetActive(false);
	}

	public void LoadSceneMap() {
		SceneManager.LoadScene("Maping");
	}

	public void ReloadSceneLevels() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}