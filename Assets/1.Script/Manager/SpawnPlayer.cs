using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : Singleton<SpawnPlayer>
{
    [SerializeField] private PlayerAction playerObj;

    private void Awake()
    {
        Spawn(GameMgr.Instance.pos);
    }

    public void Spawn(Vector3 pos)
    {
        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name)
        {
            case "Game":
                Instantiate(playerObj).Init(pos, false);
                break;
            case "Restaurant":
                Instantiate(playerObj).Init(pos, true);
                break;
            default:
                break;
        }
    }
}
