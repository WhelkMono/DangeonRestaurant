using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
    [SerializeField] private GameObject profilePanel;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActivePanel(bool active)
    {
        isActive = active;
        profilePanel.SetActive(isActive);
    }
}
