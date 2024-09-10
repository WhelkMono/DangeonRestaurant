using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [System.Serializable]
    public class CheckBox
    {
        public GameObject box;
        public TMP_Text descTxt;
        public Button yesButton;
    }

    [System.Serializable]
    public class SettingWindow
    {
        public GameObject window;
        public Slider masterSlider;
        public TMP_Text masterValueTxt;
        public Slider musicSlider;
        public TMP_Text musicValueTxt;
        public Slider effectsSlider;
        public TMP_Text effectsValueTxt;
    }

    [SerializeField] private GameObject backPanel;
    [SerializeField] private Buttons buttons;
    [SerializeField] private CheckBox checkBox;
    [SerializeField] private SettingWindow settingWindow;

    private void Start()
    {
        bool bl = JsonDataManager.Instance.storageData.isPlay;
        buttons.PlayButton.SetActive(!bl);
        buttons.ContinueButton.SetActive(bl);
        buttons.NewGameButton.SetActive(bl);
        OnSetting(false);
        OnVisibleCheckPanel(false);
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
        checkBox.yesButton.onClick.AddListener(SetNewGame);
        SetCheckPanel("모든 진행 상황이 사라집니다.");
        OnVisibleCheckPanel(true);
    }

    public void SetNewGame()
    {
        JsonDataManager.Instance.ResetPlayerJsonData();
        JsonDataManager.Instance.LoadPlayerJsonData();

        buttons.PlayButton.SetActive(true);
        buttons.ContinueButton.SetActive(false);
        buttons.NewGameButton.SetActive(false);

        OnVisibleCheckPanel(false);
    }

    public void OnSetting(bool isVi)
    {
        settingWindow.window.SetActive(isVi);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void SetCheckPanel(string desc)
    {
        checkBox.descTxt.text = desc;
    }

    public void OnVisibleCheckPanel(bool isVi)
    {
        checkBox.box.SetActive(isVi);
        backPanel.SetActive(isVi);
    }
}
