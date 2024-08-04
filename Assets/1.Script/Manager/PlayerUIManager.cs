using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    [SerializeField] private GameObject InteractionKey;
    GameObj scanObject;

    // Start is called before the first frame update
    void Start()
    {
        InteractionKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && scanObject != null && !Inventory.Instance.isAction)
        {
            InteractionKey.SetActive(false);
            scanObject.Action();
        }
    }

    public void OnInteractionKey(GameObject _scanObject)
    {
        if(_scanObject != null && _scanObject.GetComponent<GameObj>() != null)
            scanObject = _scanObject.GetComponent<GameObj>();
        else
            scanObject = null;

        if (scanObject != null && !TalkManager.Instance.isAction)
        {
            InteractionKey.SetActive(true);
        }
        else
        {
            InteractionKey.SetActive(false);
        }
    }
}
