using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, GameObj
{
    public ItemAddressData itemAddressData;
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
        Inventory.Instance.GetIt(itemAddressData);
        Destroy(gameObject);
    }
}
