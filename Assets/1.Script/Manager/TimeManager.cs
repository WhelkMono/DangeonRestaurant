using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ClockUI
{
    public Image timeImg;
    public Image weatherImg;
    public Transform hourHand;
    public TMP_Text dayTxt;
}


public class TimeManager : Singleton<TimeManager>
{
    public TimeData timeData;
    public TimeData pTimeData;
    public ClockUI clockUI;
    public float timer = 0;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadData()
    {
        timeData = JsonDataManager.Instance.storageData.timeData;
        pTimeData = new();
        pTimeData.day = timeData.day;
        pTimeData.hour = timeData.hour;
        pTimeData.minute = timeData.minute;
        timer = timeData.minute;
    }

    public void SaveData()
    {
        JsonDataManager.Instance.storageData.timeData = timeData;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 60)
        {
            timer -= 60f;
            if (timeData.hour + 1 == 24)
            {
                timeData.hour = 0;
                timeData.day++;
            }
            else
                timeData.hour++;
        }
        timeData.minute = Mathf.FloorToInt(timer);

        SetPdata();

        if (clockUI.dayTxt != null)
        {
            clockUI.dayTxt.text = "day " + timeData.day.ToString();
            clockUI.hourHand.eulerAngles = new Vector3(0, 0, timeData.hour * -15f);
        }
    }

    public void FlowTime(TimeData _timeData)
    {
        timeData.minute += _timeData.minute;
        if (timeData.minute >= 60)
        {
            timeData.minute -= 60;
            timeData.hour++;
        }
        timeData.hour += _timeData.hour;
        if (timeData.hour >= 24)
        {
            timeData.hour -= 24;
            timeData.day++;
        }
        timeData.day += _timeData.day;

        SetPdata();
    }

    public void SetPdata()
    {
        if (GameMgr.Instance.Player == null) return;

        int d = timeData.day - pTimeData.day - 1;
        if (d < 0) d = 0;

        int hunger = timeData.hour - pTimeData.hour;
        if (hunger < 0) hunger += 24;
        hunger = hunger * 2 + d * 48;

        hunger += Mathf.FloorToInt((timeData.minute - pTimeData.minute) / 30f);
        GameMgr.Instance.Player.SetHunger(-hunger);

        int t = timeData.minute - pTimeData.minute;
        if (t < 0) t = -(t + 60);
        int minute = t % 30;

        pTimeData.day = timeData.day;
        pTimeData.hour = timeData.hour;
        pTimeData.minute = timeData.minute;

        if (minute < 0)
        {
            pTimeData.minute = 60 + minute;

            if (pTimeData.hour == 0)
            {
                pTimeData.hour = 23;
                pTimeData.day--;
            }
            else
            {
                pTimeData.hour--;
            }
        }
        else
        {
            pTimeData.minute -= minute;
        }
    }

    public int DataPreview(TimeData _timeData)
    {
        int d = _timeData.day - pTimeData.day - 1;
        if (d < 0) d = 0;

        int hunger = _timeData.hour - pTimeData.hour;
        if (hunger < 0) hunger += 24;
        hunger = hunger * 2 + d * 48;

        hunger += Mathf.FloorToInt((_timeData.minute - pTimeData.minute) / 30f);
        return hunger;
    }
}
