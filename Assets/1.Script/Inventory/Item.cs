using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text countTxt;

    public ItemData itemData;
    public Slot slot;

    private Vector2 offset;

    public void Init(ItemData _itemData)
    {
        slot = null;
        itemData = _itemData;

        image.sprite = SpriteManager.Instance.ItemSprites[itemData.id];
        countTxt.text = itemData.count.ToString();
    }

    public void AddData(int count)
    {
        itemData.count += count;
        countTxt.text = itemData.count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemData != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(PlayerUIManager.Instance.canvasTrans);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemData != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        this.transform.SetParent(slot.transform);
        this.transform.localPosition = Vector2.zero;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}