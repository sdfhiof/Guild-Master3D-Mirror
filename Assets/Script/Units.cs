using Fusion.LagCompensation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static UnityEngine.UI.CanvasScaler;

public class Units : WarManager
{
    public float maxHealth; // �ִ�ü��
    public float curHealth; // ����ü��
    public float damage; // ��
    public float range; // ���ݹ���
    public float attackT;
    public float curAnimeT;

    public bool isRun;
    public bool isInRange; // ���ݹ��� �ȿ� �ִ°�
    public bool isAttack; // �����ϴ� ���ΰ�
    public bool doDie;
    public bool doVictory;

    float timer;
    
    NavMeshAgent agent;
    Animator anime;
    Rigidbody rigid;
    Transform trans;
    MeshRenderer[] meshs; // �÷��̾�� ����, ��, �ٸ� ���������� �迭�� ������

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        trans = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        SetState();
        checkAllDead();
    }

    void SetState()
    {
        Run();
        CheckInRange();
        Attack();
        Die();
    }
    
    // this will move to WarManger
    void checkAllDead()
    {
        for(int i = 0; i<enemy.Count; i++)
        {
            CheckAllDie[i] = enemy[i].doDie; // true | false �� ����ֱ�
        }
        if (CheckAllDie.All(x => x == true)) // ��� true���{
        {
            doVictory = true;
            StartCoroutine(AnimVictory());
        }
    }
    
    public float GetRange(Units obj) // �ڱ��ڽŰ� Ÿ������list�� ������ �Ÿ� ���
    {
        float posX = trans.position.x - obj.trans.position.x;
        float posZ = trans.position.z - obj.trans.position.z;
        return (float)Mathf.Sqrt(posX * posX + posZ * posZ);
    }

    // set state function 
    void CheckInRange()
    {
        if(range >= GetRange(tUnit))
        {
            isInRange = true;
        } 
        else
        {
            isInRange = false;
        }
    }
    void Run()
    {
        // check
        if (tag == "Guild")
            FindTargetUnit(enemy);
        else if (tag == "Enemy")
            FindTargetUnit(guild);

        if (range >= GetRange(tUnit))
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }

        // animation
        agent.SetDestination(tUnit.transform.position);
        if (!isAttack && !isInRange && !doDie && !doVictory)
        {
            isRun = true;
            anime.SetBool("isRun", true);
            agent.isStopped = false;
            LookTarget();
        }
        else
        {
            isRun = false;
            anime.SetBool("isRun", false);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    void Attack()
    {
        // animation
        if (!isRun && isInRange && !doDie && !doVictory)
        {
            isAttack = true;
            anime.SetBool("isAttack1", true);
            LookTarget();
        }

        else
        {
            isAttack = false;
            anime.SetBool("isAttack1", false);
        }

        // real working
        attackT = anime.GetCurrentAnimatorStateInfo(0).length;
        if (timer > attackT && isAttack && !doDie)
        {
            timer = 0;
            if (tUnit.curHealth > 0)
            {
                tUnit.curHealth -= damage;
                StartCoroutine(GiveHit());
            }
        }
    }
    IEnumerator GiveHit()
    {
        // �¾��� �� ȸ������
        tUnit.instance.GetComponent<MeshRenderer>().material.color = Color.gray;

        yield return new WaitForSeconds(1.0f);

        if (tag == "Guild") // ���� �ְ� ����±׸�
        {
            tUnit.instance.GetComponent<MeshRenderer>().material.color = Color.red;
            // Ÿ�� ģ���� ���ʹ����״� ���������� ���ƿ���
        }
        if (tag == "Enemy") // ���� �ְ� ���±׸�
        {
            tUnit.instance.GetComponent<MeshRenderer>().material.color = new Color(0 / 255f, 172 / 255f, 255 / 255f);
            // Ÿ�� ģ���� ������״� �Ķ������� ���ƿ���
        }
    }
    void LookTarget()
    {
        transform.LookAt(tUnit.transform.position);
    }
    void Die()
    {
        if (curHealth > 0)
            doDie = false;
        else
            doDie = true;

        if (doDie)
        {
            instance.GetComponent<MeshRenderer>().material.color = Color.black;

            StartCoroutine(AnimDie());
        }
    }

    // anim corutine
    IEnumerator AnimDie()
    {
        anime.SetBool("isDead", true);

        curAnimeT = anime.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(2.5f);

        gameObject.SetActive(false);

        anime.SetBool("isDead", false);
    }
    IEnumerator AnimVictory()
    {
        anime.SetBool("doVictory", true);
        curAnimeT = anime.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(curAnimeT);
    }
}
