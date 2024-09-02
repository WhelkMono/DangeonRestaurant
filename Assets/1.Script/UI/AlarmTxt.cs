using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlarmTxt : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float delay;
    private float timer;

    private void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            StartCoroutine(Close());
        }
    }

    public IEnumerator Close()
    {
        while (_text.color.a > 0.08)
        {
            _text.color = new Color(1, 1, 1, _text.color.a - _text.color.a * Time.deltaTime);
            yield return null;
        }
        Destroy(this.gameObject);
    }

public void Init(string txt)
    {
        _text.text = txt;
    }
}
