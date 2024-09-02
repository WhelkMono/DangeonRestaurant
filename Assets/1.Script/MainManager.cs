using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject ContinueButton;
    public GameObject AbandonRunButton;

    private void Start()
    {
        bool bl = JsonDataManager.Instance.storageData.isPlay;
        PlayButton.SetActive(!bl);
        ContinueButton.SetActive(bl);
        AbandonRunButton.SetActive(bl);
    }

    private void GameStart(string sceneName)
    {
        LoadSceneManager.isFirstLoad = true;
        LoadSceneManager.LoadScene(sceneName);
    }

    public void OnPlay()
    {
        GameStart("Game");
    }

    public void OnContinue()
    {
        LocationData locationData = JsonDataManager.Instance.storageData.playerLocation;
        string sceneName = "Game";

        if (locationData.spaceType == SpaceType.Restaurant ||
            locationData.spaceType == SpaceType.Myroom)
        {
            sceneName = "Restaurant";
        }

        GameStart(sceneName);
    }

    public void OnAbandonRun()
    {
        JsonDataManager.Instance.ResetPlayerJsonData();
        JsonDataManager.Instance.LoadPlayerJsonData();

        PlayButton.SetActive(true);
        ContinueButton.SetActive(false);
        AbandonRunButton.SetActive(false);
    }

    public void OnSettings()
    {
        
    }
}
