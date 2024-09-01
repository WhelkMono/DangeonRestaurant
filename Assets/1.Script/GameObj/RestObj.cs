using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestObj : MonoBehaviour, GameObj
{
    public SpaceType spaceType;

    public void Action()
    {
        JsonDataManager.Instance.storageData.playerLocation.spaceType = spaceType;
        JsonDataManager.Instance.storageData.playerLocation.pos = GameMgr.Instance.Player.transform.position;

        FlowTime.Instance.SetPanal(true);
    }
}
