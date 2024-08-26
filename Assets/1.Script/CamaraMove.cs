using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 targat = GameMgr.Instance.Player.transform.position + new Vector3(0, 0, -10);

        transform.position = Vector3.Lerp(transform.position, targat, Time.deltaTime * 10f);
    }
}
