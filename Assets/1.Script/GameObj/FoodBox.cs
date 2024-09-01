using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBox : MonoBehaviour, GameObj
{
    public void Action()
    {
        PlayerUIManager.Instance.OnBoxInven(InventoryType.food);
    }
}
