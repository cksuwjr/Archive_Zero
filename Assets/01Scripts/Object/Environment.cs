using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : Entity
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EntityOuter"))
        {
            EntitySpawner.RePosition(this);
        }
    }
}
