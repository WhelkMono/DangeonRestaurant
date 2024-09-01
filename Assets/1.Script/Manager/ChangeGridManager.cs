using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGridManager : Singleton<ChangeGridManager>
{
    public SpaceType mainSpaceType;

    [System.Serializable] 
    public class GridDictionary
    {
        public SpaceType spaceType;
        public GameObject grid;
        public GameObject obj;
        public int xSize;
        public int ySize;
    }

    [SerializeField] private GridDictionary[] grids;
    public GridDictionary nowGrid;

    private void Start()
    {
        if (GameMgr.Instance.isFirstLoad)
        {
            LocationData locationData = JsonDataManager.Instance.storageData.playerLocation;
            ChangeGrid(locationData.spaceType, locationData.pos);
        }
        else
        {
            for (int i = 0; i < grids.Length; i++)
            {
                if (grids[i].spaceType == mainSpaceType)
                {
                    grids[i].obj.SetActive(true);
                    grids[i].grid.SetActive(true);
                    nowGrid = grids[i];
                }
            }
        }

        ShowName();
    }

    public void ChangeGrid(SpaceType spaceType, Vector3 nextPos)
    {
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].spaceType == spaceType)
            {
                grids[i].obj.SetActive(true);
                grids[i].grid.SetActive(true);
                nowGrid = grids[i];
            }
            else
            {
                grids[i].obj.SetActive(false);
                grids[i].grid.SetActive(false);
            }
        }

        GameMgr.Instance.Player.transform.position = nextPos;
        ShowName();
    }

    private void ShowName()
    {
        string name;

        switch (nowGrid.spaceType)
        {
            case SpaceType.StartFloor:
                name = "시작의 땅";
                break;
            case SpaceType.HomeFloor:
                name = "토가리코";
                break;
            case SpaceType.Restaurant:
                name = "식당";
                break;
            case SpaceType.Myroom:
                name = "침실";
                break;
            default:
                name = "???";
                break;
        }

        StartCoroutine(ShowMapName.Instance.Show(name));
    }
}
