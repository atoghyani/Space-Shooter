using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    TextMeshProUGUI scoreText;
    GameSession gamesession;
    [SerializeField] int score;

    // Use this for initialization
    void Awake ()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gamesession = FindObjectOfType<GameSession>();
        scoreText.text = gamesession.GetScore().ToString();

    }
	
	// Update is called once per frame
	void Update ()
    {
        gamesession = FindObjectOfType<GameSession>();
        scoreText.text = gamesession.GetScore().ToString();
        score = gamesession.GetScore();

	}
}
