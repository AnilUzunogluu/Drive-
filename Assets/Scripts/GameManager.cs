using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenuUI;
    [SerializeField] private CollisionManager collisionManager;
    [SerializeField] private CarMovement carMovement;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private Button continueButton;

    private void OnEnable()
    {
        collisionManager.OnCrash += SetGameOver;
    }

    private void OnDisable()
    {
        collisionManager.OnCrash -= SetGameOver;
    }

    private void SetGameOver(bool value)
    {
        gameOverMenuUI.SetActive(value);
    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void WatchAdToContinue()
    {
        AdManager.Instance.ShowAd(this);
        continueButton.interactable = false;
    }

    public void ContinueGame()
    {
        carMovement.SetCarPositionToStart();
        carMovement.SetCrashedState(false);
        scoreSystem.SetCrashedState(false);
        SetGameOver(false);
    }

}
