using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour, GameObj
{
    public void Action()
    {
        CookingManager.Instance.OnCookPanel();
    }
}
