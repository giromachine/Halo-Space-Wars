using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameState gameState;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameState = FindObjectOfType<GameState>();
    }

    private void Update()
    {
        scoreText.text = gameState.GetScore().ToString();
    }
}
