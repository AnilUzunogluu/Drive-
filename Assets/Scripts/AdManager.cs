using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testModeForAd = true;
   
    public static AdManager Instance;
    private GameManager _gameManager;
    private readonly string _androidGameId = "4988282";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            Advertisement.AddListener(this);
            Advertisement.Initialize(_androidGameId, testModeForAd);
        }
    }

    public void ShowAd(GameManager gameManager)
    {
        _gameManager = gameManager;
        
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Unity Ads Ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"Ads Error message: {message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                _gameManager.ContinueGame();
                break;
            case ShowResult.Skipped:
                _gameManager.ContinueGame();
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }
}
