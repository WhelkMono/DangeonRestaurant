using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public TimeData timeData = null;
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
        if (timeData == null)
            return;

        timer += Time.deltaTime;

        if(timeData.minute + Time.deltaTime >= 60)
        {
            timer -= 60f;
            if (timeData.day + 1 >= 60)
            {
                timeData.day = 0;
                timeData.week++;
            }
            else
                timeData.day++;
        }
        timeData.minute = Mathf.FloorToInt(timer);
    }
}
