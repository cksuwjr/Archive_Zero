using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveItem : Item
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                player.GetArchive(10);
                ReturnToPool();
            }
        }
    }
}
