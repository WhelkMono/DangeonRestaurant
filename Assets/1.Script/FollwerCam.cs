using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollwerCam : MonoBehaviour
{
    private PlayerAction p;

    private void Start()
    {
        if (p == null && GameMgr.Instance != null)
        {
            p = GameMgr.Instance.Player;
        }

        Vector3 vec = p.transform.position;
        transform.position = new Vector3(vec.x, vec.y, -10f);
    }

    void Update()
    {
        Vector3 vec = p.transform.position;
        float x = ChangeGridManager.Instance.nowGrid.xSize - 11f;
        float y = ChangeGridManager.Instance.nowGrid.ySize - 6.5f;
        float clmapX = Mathf.Clamp(vec.x, -x, x);
        float clmapY = Mathf.Clamp(vec.y, -y, y);
        transform.position = new Vector3(clmapX, clmapY, -10);
    }
}
