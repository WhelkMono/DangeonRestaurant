using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, GameObj
{
    public string sceneName;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GameMgr.Instance.SortSprite(sr);
    }

    public void Action()
    {
        LoadSceneManager.LoadScene(sceneName);
    }
}
