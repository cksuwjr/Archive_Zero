using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : Item
{
    public override void Acquired(Entity entity)
    {
        if (entity.TryGetComponent<PlayerController>(out var player))
            player.GetHeal(25);
        base.Acquired(entity);
    }
}
