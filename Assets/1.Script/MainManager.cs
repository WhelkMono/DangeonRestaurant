using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGamePlay()
    {
        LoadSceneManager.isFirstLoad = true;

        LocationData locationData = JsonDataManager.Instance.storageData.playerLocation;
        string sceneName = "Game";

        if (locationData.spaceType == SpaceType.Restaurant ||
            locationData.spaceType == SpaceType.Myroom)
        {
            sceneName = "Restaurant";
        }
        LoadSceneManager.LoadScene(sceneName);
    }
}
