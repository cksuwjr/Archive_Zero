using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>, IManager
{
    public Pool soundPool;

    public void Init()
    {
        soundPool?.Init();
    }
}