using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text countTxt;

    public void Init(Sprite sprite, string _name, int requCount, int possCount)
    {
        image.sprite = sprite;

        nameTxt.text = _name;
        countTxt.text = possCount.ToString() + "/" + requCount.ToString();
    }
}