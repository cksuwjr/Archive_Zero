using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>, IManager
{
    public Pool soundPool;
    public Pool basicAttackPool;
    public Pool codecBlastPool;
    public Pool monsterPool;
    public Pool environmentPool;

    public void Init()
    {
        soundPool?.Init();
        basicAttackPool?.Init();
        codecBlastPool?.Init();
        monsterPool?.Init();
        environmentPool?.Init();
    }
}