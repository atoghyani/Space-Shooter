using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] int score=0;
  //  [SerializeField] int scorePerKill = 62;
   

    // Use this for initialization
    void Awake () 
    {
     
        SetUpSingleton();
	}
	
	// Update is called once per frame
	void Update () 
    {
       // scoreText.text = score.ToString();
	}


    public void addToScore(int scorePerKill)
    {
        score += scorePerKill;
    
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType(GetType()).Length;
        if ( numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
