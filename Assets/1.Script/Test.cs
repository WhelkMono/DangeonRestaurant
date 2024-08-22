using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isPointer = false;
    float delay = 0;
    private void Update()
    {
        if (isPointer)
        {
            delay += Time.deltaTime;

            if (delay >= 0.5f)
            {
                Appear();
                isPointer = false;
            }
        }
    }

    public void Appear()
    {
        Debug.Log("����");
    }

    public void Disappear()
    {
        Debug.Log("����");
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = 0;
        isPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointer = false;
        Disappear();
    }
}
