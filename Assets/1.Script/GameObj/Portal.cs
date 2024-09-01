using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, GameObj
{
    public string sceneName;

    public void Action()
    {
        PlayerUIManager.Instance.playerInventory.SaveItemData();
        LoadSceneManager.LoadScene(sceneName);
    }
}
