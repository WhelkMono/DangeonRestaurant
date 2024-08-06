using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text countTxt;
    [SerializeField] private TMP_Text locationTxt;

    public void Init(Sprite sprite, string _name, int requCount, int possCount, string _location)
    {
        image.sprite = sprite;

        nameTxt.text = _name;
        countTxt.text = requCount.ToString() + "/" + possCount.ToString();
        locationTxt.text = _location;
    }
}
