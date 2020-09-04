using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadCoreGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadAgainGame()
    {
        FindObjectOfType<GameState>().DestroyGameStatus();
        SceneManager.LoadScene("Game");
    }
    public void LoadGameOver()
    {
        StartCoroutine(SceneDelay());
        
    }
    IEnumerator SceneDelay()
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game Over");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
