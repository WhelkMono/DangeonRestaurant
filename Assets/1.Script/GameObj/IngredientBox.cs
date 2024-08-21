using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour, GameObj
{
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Action()
    {
        PlayerUIManager.Instance.OnBoxInven(InventoryType.ingredient);
    }
}
