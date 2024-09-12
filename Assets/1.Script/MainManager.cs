using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [System.Serializable]
    public class Buttons
    {
        public GameObject PlayButton;
        public GameObject ContinueButton;
        public GameObject NewGameButton;
        public GameObject SettingButton;
        public GameObject QuitButton;
    }

    [SerializeField] private Buttons buttons;

    private void Start()
    {
        bool bl = JsonDataManager.Instance.storageData.isPlay;
        buttons.PlayButton.SetActive(!bl);
        buttons.ContinueButton.SetActive(bl);
        buttons.NewGameButton.SetActive(bl);
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

    public void OnNewGame()
    {
        MenuManager.Instance.SetCheckBox(SetNewGame, "모든 진행 상황이 사라집니다.");
    }

    public void SetNewGame()
    {
        JsonDataManager.Instance.ResetPlayerJsonData();
        JsonDataManager.Instance.LoadPlayerJsonData();

        buttons.PlayButton.SetActive(true);
        buttons.ContinueButton.SetActive(false);
        buttons.NewGameButton.SetActive(false);

        MenuManager.Instance.VisibleCheckPanel(false);
    }

    public void OnSetting()
    {
        MenuManager.Instance.OnVisibleSettingPanel(true);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
