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
    public ClockUI clockUI;
    public float timer = 0;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadData()
    {
        timeData = JsonDataManager.Instance.storageData.timeData;
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

        if(timeData.minute + Time.deltaTime >= 60)
        {
            timer -= 60f;
            if (timeData.hour + 1 >= 24)
            {
                timeData.hour = 0;
                timeData.day++;
            }
            else
                timeData.hour++;
        }
        timeData.minute = Mathf.FloorToInt(timer);

        if (clockUI.dayTxt == null)
            return;

        clockUI.dayTxt.text = "day " + timeData.day.ToString();
        clockUI.hourHand.eulerAngles = new Vector3(0, 0, timeData.hour * -15f);
    }
}
