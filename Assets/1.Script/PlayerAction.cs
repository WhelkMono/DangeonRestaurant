using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private GameObject AtackBox;
    public GameObject inventouryKey;

    Rigidbody2D rigid;
    Animator am;
    SpriteRenderer sr;

    Vector2 dirVec;
    GameObj scanObject;

    public int speed;
    float h;
    float v;
    bool isHorizonMove; 
    bool isAttack;
    float attackDelay;
    float attackTimer;
    public bool isHouse;
    public PlayerData data;

    public void Init(Vector2 startPos, bool isHouse)
    {
        data = JsonDataManager.Instance.storageData.playerData;

        transform.position = startPos;
        this.isHouse = isHouse;

        rigid = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        isAttack = false;
        attackDelay = 0.4f;
        attackTimer = 0f;

        inventouryKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Instance.IsPause)
            return;

        Attack();
        Move();
        inventouryKey.SetActive(PlayerUIManager.Instance.OnInteractionKey(scanObject));
    }

    private void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        if (TalkManager.Instance.isAction || isAttack)
            return;

        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.2f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null && rayHit.collider.GetComponent<GameObj>() != null)
            scanObject = rayHit.collider.gameObject.GetComponent<GameObj>();
        else
            scanObject = null;
    }

    private void Attack()
    {
        if (isHouse)
            return;

        if (isAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                attackTimer = 0f;
                isAttack = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(AtackBox, transform);
            obj.transform.localPosition = dirVec;
            isAttack = true;
            am.SetTrigger("attack");
        }
    }

    private void Move()
    {
        if (TalkManager.Instance.isAction || isAttack)
        {
            h = 0;
            v = 0;
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (hDown || (v == 0 && vUp))
            isHorizonMove = true;
        else if (vDown || (h == 0 && hUp))
            isHorizonMove = false;

        //Diraction
        if (isHorizonMove)
        {
            if (h == 1)
                dirVec = Vector2.right;
            else if (h == -1)
                dirVec = Vector2.left;
        }
        else
        {
            if (v == 1)
                dirVec = Vector2.up;
            else if (v == -1)
                dirVec = Vector2.down;
        }
        
        //ÁÂ¿ì¹ÝÀü
        if (h != 0)
            sr.flipX = h == -1 ? true : false;

        if (!isAttack)
        {
            //Animation
            am.SetBool("hMove", isHorizonMove);
            if (am.GetInteger("hAixsRaw") != h)
            {
                am.SetBool("isChange", true);
                am.SetInteger("hAixsRaw", (int)h);
            }
            else if (am.GetInteger("vAixsRaw") != v)
            {
                am.SetBool("isChange", true);
                am.SetInteger("vAixsRaw", (int)v);
            }
            else
                am.SetBool("isChange", false);
        }
    }

    public void SetHunger(int value)
    {
        if (data.Hunger <= 0)
            return;

        data.Hunger += value;
        JsonDataManager.Instance.storageData.playerData.Hunger = data.Hunger;

        if (value < 0) SetHP(-value);

        if (data.Hunger > 100) data.Hunger = 100;
        else if (data.Hunger <= 0)
        {
            data.Hunger = 0;
            Death();
        }
    }

    public void SetHP(int value)
    {
        if (data.HP <= 0)
            return;

        data.HP += value;
        JsonDataManager.Instance.storageData.playerData.HP = data.HP;

        if (data.HP > 100) data.HP = 100;
        else if (data.HP <= 0)
        {
            data.HP = 0;
            Death();
        }
    }

    public void TakeDmg(int Dmg)
    {
        if (data.HP <= 0)
            return;

        SetHP(-Dmg);
        GameMgr.Instance.CreateDamageText(transform.position, Dmg);
    }

    public void Death()
    {

    }
}
