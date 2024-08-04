using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    static string nextScene;

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
                    if (nextScene == "Game")
                    {
                        SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                        SceneManager.LoadScene("PlayerUI", LoadSceneMode.Additive);
                    }
                    else if (nextScene == "Restaurant")
                    {
                        SceneManager.LoadScene("RestaurantUI", LoadSceneMode.Additive);
                        SceneManager.LoadScene("PlayerUI", LoadSceneMode.Additive);
                    }
                    yield break;
                }
            }
        }
    }
}
