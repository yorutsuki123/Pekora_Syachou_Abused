using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPotionWeakness : ItemPickup
{
    protected override void effect(GameObject obj)
    {
        obj.GetComponent<PlayerController>().nerf_posion++;
    }
}