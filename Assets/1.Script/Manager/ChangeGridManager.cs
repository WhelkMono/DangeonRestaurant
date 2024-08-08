using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGridManager : Singleton<ChangeGridManager>
{
    [System.Serializable] 
    public class GridDictionary
    {
        public SpaceType spaceType;
        public GameObject grid;
        public GameObject obj;
    }

    [SerializeField] private GridDictionary[] grids;

    private void Start()
    {
        if (GameMgr.Instance.isFirstLoad)
        {
            LocationData locationData = JsonDataManager.Instance.storageData.playerLocation;
            ChangeGrid(locationData.spaceType, locationData.pos);
        }
    }

    public void ChangeGrid(SpaceType spaceType, Vector2 nextPos)
    {
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].spaceType == spaceType)
            {
                grids[i].obj.SetActive(true);
                grids[i].grid.SetActive(true);
            }
            else
            {
                grids[i].obj.SetActive(false);
                grids[i].grid.SetActive(false);
            }
        }

        GameMgr.Instance.Player.transform.position = nextPos;
    }
}
