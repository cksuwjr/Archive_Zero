using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : Item
{
    public override void Acquired(Entity entity)
    {
        if (entity.TryGetComponent<PlayerController>(out var player))
            player.GetExp(2);
        base.Acquired(entity);
    }
}
