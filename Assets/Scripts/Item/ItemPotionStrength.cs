using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPotionStrength : ItemPickup
{
    protected override void effect(GameObject obj)
    {
        obj.GetComponent<PlayerController>().buff_posion++;
    }
}