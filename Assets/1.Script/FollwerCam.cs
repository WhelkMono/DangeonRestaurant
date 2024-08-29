using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollwerCam : MonoBehaviour
{
    private PlayerAction p;

    void Update()
    {
        if (p == null && GameMgr.Instance != null)
        {
            p = GameMgr.Instance.Player;
        }

        Vector3 vec = p.transform.position;
        float clmapX = Mathf.Clamp(vec.x, -20f, 20f);
        float clmapY = Mathf.Clamp(vec.y, -20f, 20f);
        transform.position = new Vector3(clmapX, clmapY, -10);
    }
}
