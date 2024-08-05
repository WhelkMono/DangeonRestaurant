using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour, GameObj
{
    public ItemData itemData;
    SpriteRenderer sr;

    public void Init(ItemData _itemData)
    {
        itemData = _itemData;
    }

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
        if (PlayerUIManager.Instance.playerInventory.GetIt(itemData))
        {
            Destroy(gameObject);
        }
    }
}
