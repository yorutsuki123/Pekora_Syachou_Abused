using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyController
{
    protected override void attack()
    {
        GameObject atkObj = Instantiate(attackPrefab, transform.position + new Vector3(0.716f * (transform.localScale.x < 0 ? -1 : 1), 0.627f), Quaternion.Euler(new Vector3()));
        attackTimer = Time.time + atkObj.GetComponent<AttackingController>().CD;
        atkObj.GetComponent<AttackingController>().from = "Zombie";
    }
}
