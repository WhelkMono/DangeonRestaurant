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
    [SerializeField] private Button button;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform handPreview;
    [SerializeField] private Transform alarmParent;
    [SerializeField] private AlarmTxt alarmTxt;

    private void Start()
    {
        SetPanal(false);
    }

    public void SetPanal(bool isActive)
    {
        GameMgr.Instance.Pause(isActive);
        slider.value = 1;
        flowTimePanal.SetActive(isActive);
        slider.enabled = true;
        button.enabled = true;
        hand.eulerAngles = new Vector3(0, 0, TimeManager.Instance.timeData.hour * -15f);
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

        StartCoroutine(TimeFlowAnimation());
    }

    private IEnumerator TimeFlowAnimation()
    {
        slider.enabled = false;
        button.enabled = false;

        yield return new WaitForSecondsRealtime(0.3f);

        while (Mathf.Abs(Vector3.Distance(hand.eulerAngles, handPreview.eulerAngles)) > 1)
        {
            float t = -Mathf.Abs(Mathf.Clamp(hand.eulerAngles.z - handPreview.eulerAngles.z, -80 * Time.unscaledDeltaTime, 80 * Time.unscaledDeltaTime));
            hand.Rotate(0, 0, t);
            yield return null;
        }

        hand.eulerAngles = handPreview.eulerAngles;

        yield return new WaitForSecondsRealtime(0.3f);

        Instantiate(alarmTxt, alarmParent).Init("ÀúÀåµÊ");
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

        float t = timeData.hour * -15f;
        handPreview.eulerAngles = new Vector3(0, 0, t);
    }
}
