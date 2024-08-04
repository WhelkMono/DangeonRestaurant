using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public Sprite[] ItemSprites;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
