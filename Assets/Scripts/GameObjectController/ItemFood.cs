using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFood : ItemPickup
{
    protected override void effect(GameObject obj)
    {
        for (int i = 0; i < 2; i++)
            if (obj.GetComponent<PlayerController>().hp < obj.GetComponent<PlayerController>().maxHp)
                obj.GetComponent<PlayerController>().hp++;
    }
}
