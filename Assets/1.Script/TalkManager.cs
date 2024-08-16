using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkManager : Singleton<TalkManager>
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject talkWindow;
    [SerializeField] private TMP_Text talkText;

    public bool isAction;

    private void Start()
    {
        isAction = false;
        talkWindow.SetActive(false);
    }

    // Start is called before the first frame update
    public IEnumerator Action(NPCData npcData)
    {
        isAction = true;
        talkWindow.SetActive(true);

        Sprite[] npcSprites = SpriteManager.Instance.npcSprites[npcData.id].talk;

        for (int i = 0; i < npcData.desc.Length; i++)
        {
            talkText.text = npcData.desc[i];
            image.sprite = npcSprites[npcData.emotion[i]];

            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }

        isAction = false;
        talkWindow.SetActive(false);
    }
}
