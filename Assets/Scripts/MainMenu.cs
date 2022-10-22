using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private EnergySystem energySystem;
    
    [Header("Medals")]
    [SerializeField] private Image medalImageUI;
    [SerializeField] private Sprite bronzeMedal;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private Sprite goldMedal;


    public event Action OnPLay;
    private void Start()
    {
        var highScore = PlayerPrefs.GetInt(ScoreSystem.HIGH_SCORE_KEY, 0);
        highScoreText.text = $"High Score: {highScore}";
        SetHighScoreMedal(highScore);
    }

    public void Play()
    {
        if (energySystem.Energy > 0)
        {
            OnPLay?.Invoke();
            SceneManager.LoadScene("Game");
        }
    }

    private void SetHighScoreMedal(int score)
    {
        switch (score)
        {
            case > 300:
                medalImageUI.sprite = goldMedal;
                medalImageUI.enabled = true;
                break;
            case > 200:
                medalImageUI.sprite = silverMedal;
                medalImageUI.enabled = true;
                break;
            case > 100:
                medalImageUI.sprite = bronzeMedal;
                medalImageUI.enabled = true;
                break;
        }
    }
}
