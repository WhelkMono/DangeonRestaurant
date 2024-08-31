using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public class Data
    {
        public int HP { get; set; }
        public int Power { get; set; }
        public int EXP { get; set; }
        public float Speed { get; set; }
        public float AtkDistance { get; set; }
        public float AtkSpeed { get; set; }
    }

    public enum State
    {
        Idle,
        Hit,
        Attak,
        Dead
    }

    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected Animator animator;

    protected bool play;
    protected Data data;
    protected State state;
    protected float atkTimer;
    protected float hitTimer;

    private PlayerAction p;
    private void Start()
    {
        hitTimer = 0f;
        state = State.Idle;
    }

    private void Update()
    {
        GameMgr.Instance.SortSprite(sr);

        if(!play || state == State.Dead) return;

        if (p == null)
        {
            p = GameMgr.Instance.Player;
            return;
        }

        if (state == State.Hit && hitTimer <= 0.2f)
        {
            hitTimer += Time.deltaTime;
            return;
        }

        float dis = Vector2.Distance(transform.position, p.transform.position);

        // 이동
        if (dis > data.AtkDistance)
        {
            Move();
        }
        // 공격
        else
        {
            Attak();
        }
    }

    private void Attak()
    {
        atkTimer += Time.deltaTime;
        if (atkTimer >= data.AtkSpeed)
        {
            atkTimer = 0f;
            state = State.Attak;
            animator.SetTrigger("attack");
            StartCoroutine(p.TakeDmg(data.Power));
        }
    }

    private void Move()
    {
        state = State.Idle;

        Vector2 d = p.transform.position - transform.position;
        Vector2 dir = d.normalized * data.Speed * Time.deltaTime;

        transform.Translate(dir);

        // normalized 값의 따라 좌우 구분
        if (dir.normalized.x > 0)
            sr.flipX = true;
        else if (dir.normalized.x < 0)
            sr.flipX = false;

        atkTimer = 0;
    }

    public IEnumerator TakeDmg(int Dmg)
    {
        if (data.HP <= 0)
            yield break;

        GameMgr.Instance.CreateDamageText(transform.position, Dmg);

        data.HP -= Dmg;
        //Debug.Log(data.HP);
        if (data.HP <= 0)
        {
            StartCoroutine(Death());
        }
        else
        {
            Hit();
        }
    }

    public void Hit()
    {
        hitTimer = 0f;
        state = State.Hit;
        animator.SetTrigger("hit");
    }

    public IEnumerator Death()
    {
        state = State.Dead;
        animator.SetTrigger("death");

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
