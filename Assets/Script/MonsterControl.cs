using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    enum MobState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die,
        Stuned
    }
    MobState m_State;

    public GameObject bossRagdoll, mobRagdoll;
    public float findDIstance = 8f;
    Transform player;

    public float attackDistance = 2f;
    float moveSpeed = 0.5f;
    CharacterController CC;
    Animator anim;

    int stunStack = 0;
    float currentTime = 0;
    float attackDelay = 4f;
    public int attakPower = 3;

    int maxHP;
    public int bossHP = 50;
    public int hp = 15;
    Vector3 originPos;
    public float moveDistance = 20f;

    // Start is called before the first frame update
    void Start()
    {
        m_State= MobState.Idle;
        player = GameObject.Find("character").transform;
        CC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        originPos = transform.position;

        if(this.gameObject.layer == 14)
        {
            maxHP = bossHP;
            hp = bossHP;
        }
        else
        {
            maxHP = hp;
        }
    }

    void Move()
    {
        anim.SetBool("isWalking", true);
        transform.forward = - player.forward;
        if(this.gameObject.layer == 14)
        {
            transform.forward = -player.forward;
        }
        else
        {
            transform.forward = -player.forward;
        }
        if(Vector3.Distance(transform.position,originPos) > moveDistance)
        {
            m_State = MobState.Return;
        }
        else if(Vector3.Distance(transform.position, player.position)>attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            CC.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            m_State = MobState.Attack;
            currentTime = attackDelay;
        }
    }


    void Return()
    {
        if(Vector3.Distance(transform.position, originPos) > 0.1f) 
        {
            Vector3 dir = (originPos - transform.position).normalized;
            CC.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;
            hp = maxHP;
            anim.SetBool("isWalking", false);
            m_State= MobState.Idle;

        }
    }

    void Damaged()
    {
        
        StartCoroutine(DamageProcess());
    }
    IEnumerator DamageProcess() 
    {
        stunStack += 1;
        yield return new WaitForSeconds(0.5f);
        Debug.Log(stunStack);
        if (stunStack >= 3)
        {
            m_State = MobState.Stuned;
        }
        else
        {
            m_State = MobState.Move;
        }
        anim.SetBool("isHit", false);
    }


    void Stuned()
    {
        StartCoroutine(isStuned());
        Debug.Log("stuned");
    }
    IEnumerator isStuned()
    {
        anim.SetBool("isStun", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("isStun", false);
        m_State = MobState.Move;
    }


    public void HitEnemy(int hitPower)
    {
        if(m_State == MobState.Damaged || m_State == MobState.Die || m_State == MobState.Return)
        {
            return;
        }
        hp -= hitPower;
        if(hp>0)
        {
            anim.SetBool("isHit", true);
            m_State = MobState.Damaged;
            Damaged();
        }
        else
        {
            m_State = MobState.Die;
            Die();
        }
    }

    void Idle()
    {
        if(Vector3.Distance(transform.position, player.position) < findDIstance)
        {
            m_State = MobState.Move;
        }
    }

    void Die()
    {
        StopAllCoroutines();
        if(this.gameObject.layer == 14)
        {
            Transform dieTrans = this.gameObject.transform;
            Instantiate(bossRagdoll, dieTrans.transform.position, Quaternion.LookRotation(player.transform.position - dieTrans.transform.position));
        }
        else
        {
            Transform dieTrans = this.gameObject.transform;
            Instantiate(mobRagdoll, dieTrans.transform.position, Quaternion.LookRotation(player.transform.position - dieTrans.transform.position));
        }
        Destroy(gameObject);
    }


    void Attack()
    {
        transform.forward = -player.forward;
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                anim.SetBool("isAttacking", true);
                player.GetComponent<PlayerControl>().DamageAction(attakPower);
                currentTime = 0;
            }

        }
        else
        {
            anim.SetBool("isAttacking", false);
            m_State = MobState.Move;
            currentTime= 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13 || other.gameObject.layer == 14)
        {
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            HitEnemy(arrow.weaponPower);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_State)
        {
            case MobState.Idle:
                Idle();
                break;
            case MobState.Move:
                Move();
                break;
            case MobState.Attack:
                Attack();
                break;
            case MobState.Return:
                Return();
                break;
            case MobState.Damaged:
                Damaged();
                break;
            case MobState.Die:
                Die();
                break;
            case MobState.Stuned:
                Stuned();
                break;
        }
    }
}
