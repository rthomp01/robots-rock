using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text scoreText;
    public Image[] healthBars;

    private int currentHealthBarIndex;
    public Color[] healthBarColors;

    private int playerScore;

    private void OnEnable()
    {
        currentHealthBarIndex = healthBars.Length - 1;
        UpdateHealthBarColors();
    }

    public void RemoveHealth()
    {
        healthBars[currentHealthBarIndex].enabled = false;
        currentHealthBarIndex = Mathf.Max(0, currentHealthBarIndex - 1);
        UpdateHealthBarColors();
    }

    void UpdateHealthBarColors()
    {
        for (int i = 0; i < healthBars.Length; i++)
        {
            healthBars[i].color = healthBarColors[currentHealthBarIndex];
        }
    }

    public void AddPlayerScore()
    {
        playerScore++;
        scoreText.text = string.Format("SCORE: {0}", playerScore);
    }
}
