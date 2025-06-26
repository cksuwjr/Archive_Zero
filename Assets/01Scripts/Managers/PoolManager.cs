using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonDestroy<PoolManager>, IManager
{
    public Pool basicAttackPool;
    public Pool codecBlastPool;
    public Pool monsterPool;
    public Pool monsterYellowPool;
    public Pool monsterRedPool;
    public Pool monsterPurplePool;
    public Pool environmentPool;

    public Pool expItemPool;
    public Pool archiveItemPool;
    public Pool hpItemPool;

    public Pool hitEffectPool;
    public Pool dieEffectPool;

    public Pool middleBossPool;
    public Pool bossPool;

    public void Init()
    {
        basicAttackPool?.Init();
        codecBlastPool?.Init();
        monsterPool?.Init();
        monsterYellowPool?.Init();
        monsterRedPool?.Init();
        monsterPurplePool?.Init();
        environmentPool?.Init();

        expItemPool?.Init();
        archiveItemPool?.Init();
        hpItemPool?.Init();

        hitEffectPool?.Init();
        dieEffectPool?.Init();

        middleBossPool?.Init();
        bossPool?.Init();
    }
}