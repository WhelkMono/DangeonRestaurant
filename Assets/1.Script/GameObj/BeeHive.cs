using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHive : MonoBehaviour, GameObj
{
    [SerializeField] private Bee bee;
    [SerializeField] private Sprite deathSprite;
    
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GameMgr.Instance.SortSprite(sr);
    }

    public void Action()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;

        int count = Random.Range(2, 4);

        for (int i = 0; i < count; i++)
        {
            Bee beeObj = Instantiate(bee, EnemyManager.Instance.enemyTrans);
            beeObj.Init();
            beeObj.transform.position = this.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }

        ItemData itemData = new ItemData();
        itemData.type = ItemDataType.ingredientData;
        itemData.id = 4;
        itemData.count = count;

        PlayerUIManager.Instance.playerInventory.GetIt(itemData);

        sr.sprite = deathSprite;
        Invoke("Death", 2f);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
