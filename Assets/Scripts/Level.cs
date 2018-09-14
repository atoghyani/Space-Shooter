using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    SceneManager seceneManager;

    public void LoadGameSecen()
    {
        SceneManager.LoadScene("Game");
      // FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {

        StartCoroutine(DelayLevel());

    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        QuitGame();
    }

    public IEnumerator DelayLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game Over");
    }
}
