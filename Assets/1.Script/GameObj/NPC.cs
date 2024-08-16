using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, GameObj
{
    public int ID;
    NPCData npcData;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        npcData = JsonDataManager.Instance.objectData.npcDatas[ID];
    }

    void Update()
    {
        GameMgr.Instance.SortSprite(sr);
    }

    public void Action()
    {
        if(!TalkManager.Instance.isAction)
            StartCoroutine(TalkManager.Instance.Action(npcData));
    }
}
