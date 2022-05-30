using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlimeController : EnemyController
{
    protected override void attack()
    {
        GameObject atkObj = Instantiate(attackPrefab, transform.position + new Vector3(0.401f * (transform.localScale.x < 0 ? -1 : 1), 0.6f), Quaternion.Euler(new Vector3()));
        attackTimer = Time.time + atkObj.GetComponent<AttackingController>().CD;
    }
}
