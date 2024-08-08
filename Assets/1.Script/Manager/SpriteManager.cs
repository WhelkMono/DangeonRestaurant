using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public Sprite[] FoodSprites;
    public Sprite[] IngredientSprites;
    public Sprite[] GadgetSprites;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
