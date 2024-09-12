using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCMenu : MonoBehaviour
{
    [SerializeField] private GameObject EscMenu;

    // Start is called before the first frame update
    void Start()
    {
        VisibleEscMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VisibleEscMenu(!EscMenu.activeSelf);
        }
    }

    private void VisibleEscMenu(bool isVi)
    {
        GameMgr.Instance.Pause(isVi);

        PlayerUIManager.Instance.isESCMenu = isVi;

        EscMenu.SetActive(isVi);
        MenuManager.Instance.OnVisibleSettingPanel(false);
        MenuManager.Instance.VisibleCheckPanel(false);
    }

    public void OnSave()
    {
        JsonDataManager.Instance.SavePlayerJsonData();
    }

    public void OnSetting()
    {
        MenuManager.Instance.OnVisibleSettingPanel(true);
    }

    public void OnExit()
    {
        MenuManager.Instance.SetCheckBox(GoToMain, "저장하지 않은 진행 상황은 사라집니다.");
    }

    public void GoToMain()
    {
        LoadSceneManager.LoadScene("Main");
    }
}
