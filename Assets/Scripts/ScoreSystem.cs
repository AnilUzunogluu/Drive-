using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private CollisionManager collisionManager;

    
    private float _rawScore;
    private int _score;
    private int _minutes;
    private int _seconds;
    private bool _hasCrashed;
    
    
    private const float SCORE_MULTIPLIER = 3f;
    public const string HIGH_SCORE_KEY = "HighScore";
    
    private void OnEnable()
    {
        collisionManager.OnCrash += SetCrashedState;
    }
    
    private void OnDestroy()
    {
        collisionManager.OnCrash -= SetCrashedState;
        var currentHighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);

        if (_rawScore > currentHighScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, _score);
        }
    }

    private void Update()
    {
        if (_hasCrashed) return;
        UpdateScore();
        DisplayScore();
    }

    private void UpdateScore()
    {
        _rawScore += Time.deltaTime * SCORE_MULTIPLIER;
        _score = Mathf.FloorToInt(_rawScore);
    }

    private void DisplayScore()
    {
        scoreText.text = _score.ToString();
    }

    public void SetCrashedState(bool value)
    {
        _hasCrashed = value;
    }
}
