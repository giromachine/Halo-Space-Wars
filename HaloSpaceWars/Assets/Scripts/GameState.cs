using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [Range(0.1f, 5f)] [SerializeField] float timeScale = 1f;

    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameState>().Length;
        if (gameStatusCount > 1)
        {
            DestroyGameStatus();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
    }
    public void AddScore(int num)
    {
        int Multi = num + 1;
        currentScore += Random.Range(8 * Multi, 10 * Multi);
        
    }
    public void DestroyGameStatus()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public int GetScore()
    {
        return currentScore;
    }
}
