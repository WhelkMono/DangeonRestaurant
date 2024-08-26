using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, GameObj
{
    [SerializeField] private ItemData itemData;

    public void Action()
    {
        PlayerUIManager.Instance.playerInventory.GetIt(itemData);
    }
}
