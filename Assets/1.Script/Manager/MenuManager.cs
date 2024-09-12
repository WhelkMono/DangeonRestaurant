using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MenuManager : Singleton<MenuManager>
{
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
    [SerializeField] private CheckBox checkBox;
    [SerializeField] private SettingWindow settingWindow;

    private void Start()
    {
        OnVisibleSettingPanel(false);
        VisibleCheckPanel(false);
        SetSoundData();
    }

    public void OnVisibleSettingPanel(bool isVi)
    {
        settingWindow.window.SetActive(isVi);
    }

    public void SetCheckBox(UnityAction unityAction, string desc)
    {
        checkBox.descTxt.text = desc;
        checkBox.yesButton.onClick.AddListener(unityAction);
        VisibleCheckPanel(true);
    }

    public void VisibleCheckPanel(bool isVi)
    {
        checkBox.box.SetActive(isVi);
        backPanel.SetActive(isVi);
    }

    private void SetSoundData()
    {
        SoundData soundData = JsonDataManager.Instance.storageData.soundData;
        settingWindow.masterSlider.value = soundData.master;
        settingWindow.musicSlider.value = soundData.music;
        settingWindow.effectsSlider.value = soundData.effects;
    }

    public void OnMasterValue()
    {
        int value = (int)settingWindow.masterSlider.value;
        settingWindow.masterValueTxt.text = $"{value}%";
    }

    public void OnMusicValue()
    {
        int value = (int)settingWindow.musicSlider.value;
        settingWindow.musicValueTxt.text = $"{value}%";
    }

    public void OnEffectsValue()
    {
        int value = (int)settingWindow.effectsSlider.value;
        settingWindow.effectsValueTxt.text = $"{value}%";
    }
}
