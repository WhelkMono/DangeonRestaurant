using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackbox : MonoBehaviour
{
    private float timer;

    private void Start()
    {
        timer = 0.3f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            StartCoroutine(collision.GetComponent<Enemy>().TakeDmg(1));
        }
    }
}
