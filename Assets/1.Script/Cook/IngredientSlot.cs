using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientSlot : MonoBehaviour
{
    public Image image;
    public TMP_Text nameTxt;
    public TMP_Text countTxt;
    [SerializeField] private TMP_Text locationTxt;

    public void Init(Sprite sprite, string _name, int requCount, int possCount, string _location)
    {
        image.sprite = sprite;

        nameTxt.text = _name;
        countTxt.text = possCount.ToString() + "/" + requCount.ToString();
        locationTxt.text = _location;
    }
}
