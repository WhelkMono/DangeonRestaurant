using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    private PlayerAction p;
    public PlayerAction Player
    {
        get
        {
            if (p == null)
            {
                p = FindAnyObjectByType<PlayerAction>();
            }

            return p;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SortSprite(SpriteRenderer sr)
    {
        if (p == null)
        {
            p = FindAnyObjectByType<PlayerAction>();
        }

        if (p.transform.position.y - sr.transform.position.y > 0)
            sr.sortingOrder = 6;
        else
            sr.sortingOrder = 4;
    }
}
