using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, GameObj
{
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    public void Action()
    {
        Debug.Log("!");
    }
}
