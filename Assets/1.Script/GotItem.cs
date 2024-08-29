using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GotItem : MonoBehaviour
{
    public Image img;
    public TMP_Text text;

    private float timer;

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2f)
        {
            Color color = new Color(1, 1, 1, img.color.a - Time.deltaTime);
            img.color = color;
            text.color = color;

            if (img.color.a <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
