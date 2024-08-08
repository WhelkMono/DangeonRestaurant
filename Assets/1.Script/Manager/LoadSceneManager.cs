using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    static string nextScene;
    public static bool isFirstLoad = false;

    [SerializeField] private Image progresBar;

    public void Start()
    {
        StartCoroutine(LoadSceneProcess());
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
        float delay = 0f;

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
                        LocationData locationData = JsonDataManager.Instance.storageData.playerLocation;
                        string sceneName = "GameUI";

                        if(locationData.spaceType == SpaceType.Restaurant &&
                            locationData.spaceType == SpaceType.Myroom)
                        {
                            sceneName = "RestaurantUI";
                        }

                        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                        SceneManager.LoadScene("PlayerUI", LoadSceneMode.Additive);
                        GameMgr.Instance.MabSetting(Vector3.zero, false);
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
                        GameMgr.Instance.MabSetting(new Vector3(-5.5f, -1, 0), false);
                    }
                    yield break;
                }
            }
        }
    }
}
