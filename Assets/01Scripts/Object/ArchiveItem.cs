using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveItem : Item
{
    public override void Acquired(Entity entity)
    {
        if (entity.TryGetComponent<PlayerController>(out var player))
            player.GetArchive(Random.Range(8f, 12f));
        base.Acquired(entity);
    }
}
