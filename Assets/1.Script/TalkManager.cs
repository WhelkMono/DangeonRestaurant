using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkManager : Singleton<TalkManager>
{
    [SerializeField] private GameObject talkWindow;
    [SerializeField] private TMP_Text talkText;

    public bool isAction;

    private void Start()
    {
        isAction = false;
        talkWindow.SetActive(false);
    }

    // Start is called before the first frame update
    public IEnumerator Action(string[] desc)
    {
        isAction = true;
        talkWindow.SetActive(true);

        for (int i = 0; i < desc.Length; i++)
        {
            talkText.text = desc[i];

            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }

        isAction = false;
        talkWindow.SetActive(false);
    }
}
