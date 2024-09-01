using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlowTime : Singleton<FlowTime>
{
    [System.Serializable]
    public class DescTxt
    {
        public TMP_Text day;
        public TMP_Text hour;
        public TMP_Text minute;
        public TMP_Text hP;
        public TMP_Text sP;
    }

    [SerializeField] private GameObject flowTimePanal;
    [SerializeField] private DescTxt descTxt;
    [SerializeField] private Slider slider;

    private void Start()
    {
        SetPanal(false);
    }

    public void SetPanal(bool isActive)
    {
        GameMgr.Instance.Pause(isActive);
        flowTimePanal.SetActive(isActive);
        SetDataPreview();
    }

    public void OnSave()
    {
        TimeData timeData = new();

        timeData.day = 0;
        timeData.hour = (int)slider.value;
        timeData.minute = 0;

        TimeManager.Instance.FlowTime(timeData);
        JsonDataManager.Instance.SavePlayerJsonData();
        SetPanal(false);
    }

    public void OnHandValueChange()
    {
        SetDataPreview();
    }

    private void SetDataPreview()
    {
        TimeData nowTimeData = TimeManager.Instance.timeData;
        TimeData timeData = new();

        timeData.day = 0;
        timeData.hour = (int)slider.value;
        timeData.minute = 0;

        timeData.minute += nowTimeData.minute;
        if (timeData.minute >= 60)
        {
            timeData.minute -= 60;
            timeData.hour++;
        }
        timeData.hour += nowTimeData.hour;
        if (timeData.hour >= 24)
        {
            timeData.hour -= 24;
            timeData.day++;
        }
        timeData.day += nowTimeData.day;

        descTxt.day.text = $"Day: {nowTimeData.day} -> {timeData.day}";
        descTxt.hour.text = $"Hour: {nowTimeData.hour} -> {timeData.hour}";
        descTxt.minute.text = $"Minute: {nowTimeData.minute} -> {timeData.minute}";

        PlayerData pData = GameMgr.Instance.Player.data;
        int value = TimeManager.Instance.DataPreview(timeData);

        int hp = pData.HP + value > 100 ? 100 : pData.HP + value;
        int sp = pData.Hunger - value < 0 ? 0 : pData.Hunger - value;

        descTxt.hP.text = $"HP: {pData.HP} -> {hp}";
        descTxt.sP.text = $"SP: {pData.Hunger} -> {sp}";
    }
}
