using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObj : MonoBehaviour
{
    public float center;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GameMgr.Instance.SortSprite(sr, transform.position.y + center);
    }
}
