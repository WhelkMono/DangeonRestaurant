using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadSceneManager : MonoBehaviour
{
    static string nextScene;
    public static bool isFirstLoad = false;

    [SerializeField] private Image progresBar;
    [SerializeField] private TMP_Text loadingTxt;

    private float timer;

    public void Awake()
    {
        loadingTxt.text = "Loading";
        progresBar.fillAmount = 0f;
    }

    public void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer < 0.5f) return;
        timer = 0;

        if (loadingTxt.text == "Loading...") loadingTxt.text = "Loading";
        else loadingTxt.text += ".";
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadScene");
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);

        op.allowSceneActivation = false;

        float timer = 0f;
        float delay = 1f;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progresBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progresBar.fillAmount = Mathf.Lerp(0f, 1f, timer * 0.5f);

                if (progresBar.fillAmount >= 0.9f && progresBar.fillAmount < 0.902f)
                {
                    yield return new WaitForSecondsRealtime(delay);
                }

                if (progresBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    if (isFirstLoad)
                    {
                        isFirstLoad = false;
                        string sceneName = "GameUI";

                        if(nextScene == "Restaurant")
                        {
                            sceneName = "RestaurantUI";
                        }

                        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                        SceneManager.LoadScene("PlayerUI", LoadSceneMode.Additive);
                        GameMgr.Instance.MabSetting(JsonDataManager.Instance.storageData.playerLocation.pos, true);
                        TimeManager.Instance.LoadData();
                    }
                    else if (nextScene == "Game")
                    {
                        SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                        SceneManager.LoadScene("PlayerUI", LoadSceneMode.Additive);
                        GameMgr.Instance.MabSetting(new Vector3(-2.5f, -0.5f, 0), false);
                    }
                    else if (nextScene == "Restaurant")
                    {
                        SceneManager.LoadScene("RestaurantUI", LoadSceneMode.Additive);
                        SceneManager.LoadScene("PlayerUI", LoadSceneMode.Additive);
                        GameMgr.Instance.MabSetting(new Vector3(-6f, -2.3f, 0), false);
                    }
                    SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
                    yield break;
                }
            }
        }
    }
}
