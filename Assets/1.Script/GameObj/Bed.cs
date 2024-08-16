using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, GameObj
{
    public void Action()
    {
        JsonDataManager.Instance.storageData.playerLocation.spaceType = SpaceType.Myroom;
        JsonDataManager.Instance.storageData.playerLocation.pos = GameMgr.Instance.Player.transform.position;
        JsonDataManager.Instance.SavePlayerJsonData();
    }
}
