using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfilePanel : MonoBehaviour
{
    [System.Serializable]
    public class ProfilePanelUI
    {
        public GameObject profilePanel;
        public TMP_Text hpTxt;
        public TMP_Text spTxt;
    }

    [SerializeField] private ProfilePanelUI profilePanelUI;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    public void SetActivePanel(bool active)
    {
        isActive = active;
        profilePanelUI.profilePanel.SetActive(isActive);

        PlayerData pData = GameMgr.Instance.Player.data;

        profilePanelUI.hpTxt.text = "HP: " + pData.HP.ToString();
        profilePanelUI.spTxt.text = "SP: " + pData.Hunger.ToString();
    }
}
