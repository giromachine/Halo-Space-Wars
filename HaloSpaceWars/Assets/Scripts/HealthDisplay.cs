using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    Player playerHealth;
    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        playerHealth = FindObjectOfType<Player>();
    }
    private void Update()
    {
        scoreText.text = playerHealth.GetHealth().ToString();
    }
}
