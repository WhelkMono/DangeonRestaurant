using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    public void Init()
    {
        data = new Data();
        data.HP = 3;
        data.Power = 5;
        data.Speed = 3;
        data.AtkDistance = 1f;
        data.AtkSpeed = 1.5f;

        atkTimer = 1f;
        play = false;

        Invoke("AppearAm", 1f);
    }

    private void AppearAm()
    {
        //Debug.Log("appear");
        play = true;
    }
}
