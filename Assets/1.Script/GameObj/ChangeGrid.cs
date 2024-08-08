using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGrid : MonoBehaviour, GameObj
{
    [SerializeField] private SpaceType nextSpaceType;
    [SerializeField] private Vector2 nextPos;

    public void Action()
    {
        ChangeGridManager.Instance.ChangeGrid(nextSpaceType, nextPos);
    }
}
