using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [System.Serializable]
    public class BarUI
    {
        public Image img;
        public TMP_Text text;
    }

    [SerializeField] private BarUI HpBarUI;
    [SerializeField] private BarUI SpBarUI;

    PlayerData pData;

    // Start is called before the first frame update
    void Start()
    {
        pData = GameMgr.Instance.Player.data;
        SetBarUI();
    }

    // Update is called once per frame
    void Update()
    {
        SetBarUI();
    }

    public void SetBarUI()
    {
        HpBarUI.img.rectTransform.sizeDelta = new Vector2(pData.HP * 3, 40);
        HpBarUI.text.text = "HP: " + pData.HP.ToString();
        SpBarUI.img.rectTransform.sizeDelta = new Vector2(pData.Hunger * 3, 40);
        SpBarUI.text.text = "SP: " + pData.Hunger.ToString();
    }
}
