using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaController : EnemyController
{
    public GameObject attack2Prefab;
    public GameObject attack3Prefab;
    public bool isPlaying;
    public bool isLeft;

    [SerializeField] int attackNum;
    [SerializeField] int counter;
    [SerializeField] int skillroll;

    protected override void FixedUpdate()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
        if (player != null)
            target = player.transform.position;
        checkGround();
        attackControll();
        animationControll();
    }

    protected override void attackControll()
    {
        if (isPlaying)
        {
            movingControll(isLeft ? -1 : 1);
        }
        else
        {
            movingControll(0);
            if (!isAttacking && attackTimer < Time.time && !isBlocked())
            {
                if (skillroll == 0 || counter == 3)
                {
                    int tmp = Random.Range(1, 5);
                    if (skillroll == tmp)
                        skillroll = Random.Range(1, 5);
                    else
                        skillroll = tmp;
                    counter = 0;
                }
                switch (skillroll)
                {
                    case 1:
                    case 2:
                    case 3:
                        switch (counter)
                        {
                            case 0:
                                attackTimer = Time.time + 2;
                                break;
                            case 1:
                                attackNum = skillroll;
                                break;
                            case 2:
                                attackTimer = Time.time + 2;
                                break;
                        }
                        break;
                    case 4:
                        switch (counter)
                        {
                            case 0:
                                attackTimer = Time.time + 1;
                                break;
                            case 1:
                                isPlaying = true;
                                break;
                            case 2:
                                attackTimer = Time.time + 1;
                                break;
                        }
                        break;
                }
                counter++;
            }
        }
        
    }

    protected override void attack()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject atkObj = Instantiate(attackPrefab, transform.position + new Vector3(0.99f * (transform.localScale.x < 0 ? -1 : 1), 0.26f), Quaternion.Euler(new Vector3()));
            Rigidbody2D atkRig = atkObj.GetComponent<Rigidbody2D>();
            atkObj.transform.localScale = new Vector3(atkObj.transform.localScale.x * (transform.localScale.x < 0 ? -1 : 1), atkObj.transform.localScale.y, atkObj.transform.localScale.z);
            atkRig.velocity = new Vector2(4 * (transform.localScale.x < 0 ? -1 : 1), 7 * i);
            atkObj.GetComponent<AttackingController>().from = "Aqua";
        }
    }

    void attack2()
    {
        GameObject atkObj = Instantiate(
            attack2Prefab, 
            transform.position + new Vector3(0.27f * (transform.localScale.x < 0 ? -1 : 1), 1.03f), 
            Quaternion.Euler(new Vector3(0, 90 * (transform.localScale.x < 0 ? -1 : 1), 0)));
        //atkObj.GetComponent<AttackingController>().from = "Aqua";
    }

    void attack3()
    {
        GameObject atkObj = Instantiate(attack3Prefab, new Vector3(player.transform.position.x, 6f), Quaternion.Euler(new Vector3()));
        Rigidbody2D atkRig = atkObj.GetComponent<Rigidbody2D>();
        atkObj.GetComponent<AttackingController>().from = "Aqua";
    }

    public override void getAttacked(int damage, string from, float direction, float block = 0.5f, string type = "Hurt")
    {
        if (damage > hp * 10)
        {
            print("CG attack from " + from);
        }
        if (type == "Hurt" || type == "Explode")
        {
            isHurt = true;
            hp -= damage;
        }
        if (type == "Donchan" && !isAttacking)
        {
            isDonchan = true;
            isContiDonchan = true;
        }
        if (blockedTimer < Time.time + block && !isAttacking)
            blockedTimer = Time.time + block;
        if (isAttacking)
            destoryWhenDie();
    }

    protected override void animationControll()
    {
        if (isBlocked() == false)
            isContiDonchan = false;
        //animator.SetBool("Stay", !isContiDonchan && ((isGround && !isWalk) || !isGround));
        animator.SetBool("Walk", !isContiDonchan && isGround && isWalk);
        animator.SetBool("Hurt", isHurt && !isAttacking);
        animator.SetBool("Donchan", isDonchan);
        animator.SetBool("Donchaning", isContiDonchan);
        animator.SetInteger("Attack", attackNum);
        isHurt = isDonchan = false;
        attackNum = 0;
    }

    protected override void whenDie()
    {
        print("BOSS DEAD");
    }

}
