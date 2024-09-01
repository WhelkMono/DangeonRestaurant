using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowMapName : Singleton<ShowMapName>
{
    [SerializeField] private TMP_Text mapNameTxt;

    public void Awake()
    {
        mapNameTxt.gameObject.SetActive(false);
    }

    public IEnumerator Show(string name)
    {
        mapNameTxt.gameObject.SetActive(true);
        mapNameTxt.text = name;
        mapNameTxt.color = Color.white;

        yield return new WaitForSeconds(2f);

        while (mapNameTxt.color.a > 0.08)
        {
            mapNameTxt.color = new Color(1, 1, 1, mapNameTxt.color.a - mapNameTxt.color.a * Time.deltaTime);
            yield return null;
        }

        mapNameTxt.gameObject.SetActive(false);
    }
}
