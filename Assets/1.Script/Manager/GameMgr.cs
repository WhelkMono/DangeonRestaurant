using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    private PlayerAction p;
    public PlayerAction Player
    {
        get
        {
            if (p == null)
            {
                p = FindAnyObjectByType<PlayerAction>();
            }

            return p;
        }
    }

    private bool isPause;
    public bool IsPause
    {
        get
        {
            return isPause;
        }
    }

    private void Awake()
    {
        isPause = false;
        DontDestroyOnLoad(this);
    }

    public void Pause(bool _isPause)
    {
        isPause = _isPause;
        Time.timeScale = IsPause ? 0 : 1;
    }

    public Vector3 pos;
    public bool isFirstLoad;

    public void MabSetting(Vector3 pos, bool isFirstLoad)
    {
        this.pos = pos;
        this.isFirstLoad = isFirstLoad;
    }

    public void SortSprite(SpriteRenderer sr)
    {
        if (p == null)
        {
            p = FindAnyObjectByType<PlayerAction>();
            if (p == null)
                return;
        }

        if (p.transform.position.y - sr.transform.position.y > 0)
            sr.sortingOrder = 6;
        else
            sr.sortingOrder = 4;
    }

    [SerializeField] private TextMesh DmgText;

    public void CreateDamageText(Vector3 point, int dmg)
    {
        TextMesh dmgTextObj = Instantiate(DmgText, point, Quaternion.identity, PlayerUIManager.Instance.canvasTrans);
        dmgTextObj.text = dmg.ToString();
    }
}
