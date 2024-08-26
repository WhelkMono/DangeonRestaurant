using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject inventouryKey;

    Rigidbody2D rigid;
    Animator am;
    SpriteRenderer sr;

    Vector2 dirVec;
    GameObject scanObject;

    public float speed;
    float h;
    float v;
    bool isHorizonMove; 
    bool isAttack;
    float attackDelay;
    float attackTimer;

    public bool isHouse;

    public void Init(Vector2 startPos, bool isHouse)
    {
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
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
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
}
