using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, GameObj
{
    public void Action()
    {
        JsonDataManager.Instance.SavePlayerJsonData();
    }
}
