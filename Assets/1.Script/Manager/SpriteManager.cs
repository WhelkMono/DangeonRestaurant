using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public Sprite[] FoodSprites;
    public Sprite[] IngredientSprites;
    public Sprite[] GadgetSprites;

    [System.Serializable]
    public class NPCSprite
    {
        public Sprite[] talk;
        public Sprite sit;
    }

    public NPCSprite[] npcSprites;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
