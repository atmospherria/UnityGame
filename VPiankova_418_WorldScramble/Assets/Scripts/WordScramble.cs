using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Word
{
	public string word;
	[Header("")]
	public string desiredRandom;

	public string GetString() {
		if (!string.IsNullOrEmpty(desiredRandom)) {
			return desiredRandom;
		}

		string result = word;
		result = "";

		while (result == word) { 
		List<char> characters = new List<char>(word.ToCharArray());
		while (characters.Count > 0) {
			int indexChar = Random.Range(0, characters.Count - 1);
			result += characters[indexChar];

			characters.RemoveAt(indexChar);
		}
	}

		return result;
	}
}


public class WordScramble : MonoBehaviour
{
	public Word[] words;

	[Header("UI REFERENCE")]
	public CharObject prefab;
	public Transform container;
	public float space;

	List<CharObject> charObjects = new List<CharObject>();
	CharObject firstSelected;

	public int currentWord;
	bool visible;
	public int score;
	public Text scoreText;
	public InputField Name;
	public static WordScramble main;
	List<Scores> highscore;

	void Awake() {
		main = this;
	}

    // Start is called before the first frame update
    void Start()
    {
		ShowScramble(currentWord);
		visible = true;
		score = PlayerPrefs.GetInt("Score");
		scoreText.text = "Score: " + score.ToString();
		highscore = new List<Scores>();
	}

    // Update is called once per frame
    void Update()
    {
		RepositionObject();
		
	}

	void RepositionObject () 
	{
		if (charObjects.Count == 0) {
			return;
		}

		float center = (charObjects.Count - 1) / 2;
		for (int i = 0; i < charObjects.Count; i++) {
			charObjects[i].rectTransform.anchoredPosition
				= new Vector2(i / center * space, 0);
			charObjects[i].index = i;
		}
	}

	//Show a random word to the screen
	public void ShowScramble () {
		ShowScramble(Random.Range(0, words.Length - 1));
	}

	// Show word from collection with desired index
	public void ShowScramble (int index) {
		charObjects.Clear();
		foreach (Transform child in container) {
			Destroy(child.gameObject);
		}

		if (index > words.Length - 1) {
			Debug.LogError("index out of range");
			return;
		}

		char[] chars = words[index].GetString().ToCharArray();
		foreach (char c in chars) {
			CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
			clone.transform.SetParent(container);

			charObjects.Add(clone.Init(c));
		}

		currentWord = index;
	}


	public void Swap(int indexA, int indexB) {
		CharObject tmpA = charObjects[indexA];

		charObjects[indexA] = charObjects[indexB];
		charObjects[indexB] = tmpA;

		charObjects[indexA].transform.SetAsLastSibling();
		charObjects[indexB].transform.SetAsLastSibling();
		CheckWord();
		
	}

	public void Select(CharObject charObject) {
		if (firstSelected) {
			Swap(firstSelected.index, charObject.index);

			//Unselected
			
			firstSelected.Select();
			charObject.Select();
		}
		else {
			firstSelected = charObject;
		}
	}

	public void UnSelect() {
		firstSelected = null;
	}

	public bool CheckWord() {
		string word = "";
		foreach(CharObject charObject in charObjects) {
			word += charObject.character;
		}

		if (word == words[currentWord].word) {
			score += +10;
			PlayerPrefs.SetInt("Score", score);
			scoreText.text = "Score: " + score.ToString();
			currentWord++;
			ShowScramble(currentWord);
			
			return true;
		}

		return false;
	}
	/*void OnGUI() {
		if (visible) {
			GUI.Box(new Rect(100, 90, 100, 100), "Навчання:");
			GUI.Label(new Rect(100, 120, 100, 100), "Клацай на букву та на іншу" +
				"\nЗбери слово");
			if (GUI.Button(new Rect(100, 200, 100, 30), "Так")) {
				visible = false;
			}
		}
	}*/

	public void LoadMenu() {
		Invoke("CreateNewRecord", 3.0f);
	}

	void CreateNewRecord() {
		if (Name.text.Length<=0) {
			LoadMenu();
		}
		ViewScoreTable._instance.SaveHighScore(Name.text, score);
		highscore = ViewScoreTable._instance.GetHighScore();
		SceneManager.LoadScene("MainMenu");
	}
}
