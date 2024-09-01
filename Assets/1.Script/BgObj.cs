using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObj : MonoBehaviour
{
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GameMgr.Instance.SortSprite(sr);
    }
}
