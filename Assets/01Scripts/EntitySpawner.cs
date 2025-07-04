using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EntitySpawner : SingletonDestroy<EntitySpawner>
{
    private List<Entity> enemies = new List<Entity>();

    public void StartSpawn()
    {
        TimeManager.Instance.StartTimer(10, 0);
        StartCoroutine("SpawnEnemys");
        StartCoroutine("SpawnBossCo");
        //StartCoroutine("SpawnBigWhiteEnemys");
        //StartCoroutine("SpawnBigGreenEnemys");
        SpawnEnvironment(10);

        Invoke("SpawnFinalBoss", 480);

        Monster.OnMonsterDie += MonsterAddDieEvent;
        Monster.OnMonsterHit += MonsterAddHitEvent;
          
    }

    private void SpawnEnvironment(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = PoolManager.Instance.environmentPool.GetPoolObject();
            var spawnPos = GetSpawnPosition();
            spawnPos.y = (obj.transform.localScale.y - 1) / 2f;
            obj.transform.position = spawnPos; 
        }
    }

    private void StopSpawnEnemy()
    {
        StopEnemys();
        StopCoroutine("Spawn");
    }

    private void StopEnemys()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            //enemies[i].Stop();
        }
        enemies.Clear();
    }

    public static Vector3 GetSpawnPosition()
    {
        var player = GameManager.Instance.Player;

        var n = Mathf.Pow(-1, Random.Range(0, 2));

        Vector3 spawnPos = player.transform.position;
        spawnPos.y = 1;

        if (n == -1)
        { // horizon
            spawnPos.x += Random.Range(-18f, 18f);
            spawnPos.z += 20f * Mathf.Pow(-1, Random.Range(0, 2));
        }
        if (n == 1)
        { // vertical
            spawnPos.x += 20f * Mathf.Pow(-1, Random.Range(0, 2));
            spawnPos.z += Random.Range(-15f, 15f);
        }
        return spawnPos;
    }

    public static void RePosition(Entity entity)
    {
        entity.transform.position = GetSpawnPosition();
        //obje.Reboot();
    }

    public List<GameObject> SpawnEnemy(int count = 1)
    {
        List<GameObject> monsters = new List<GameObject>();
        for(int i = 0; i < count; i++)
        {
            var monster = PoolManager.Instance.monsterPool.GetPoolObject();
            monster.transform.position = GetSpawnPosition();
            monsters.Add(monster);
        }
        return monsters;
    }

    public List<GameObject> SpawnPurpleEnemy(int count = 1)
    {
        List<GameObject> monsters = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var monster = PoolManager.Instance.monsterPurplePool.GetPoolObject();
            monster.transform.position = GetSpawnPosition();
            monsters.Add(monster);
        }
        return monsters;
    }

    public List<GameObject> SpawnRedEnemy(int count = 1)
    {
        List<GameObject> monsters = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var monster = PoolManager.Instance.monsterRedPool.GetPoolObject();
            monster.transform.position = GetSpawnPosition();
            monsters.Add(monster);
        }
        return monsters;
    }

    public List<GameObject> SpawnYellowEnemy(int count = 1)
    {
        List<GameObject> monsters = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var monster = PoolManager.Instance.monsterYellowPool.GetPoolObject();
            monster.transform.position = GetSpawnPosition();
            monsters.Add(monster);
        }
        return monsters;
    }

    public List<GameObject> SpawnMiddleBoss(int count = 1)
    {
        List<GameObject> monsters = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var monster = PoolManager.Instance.middleBossPool.GetPoolObject();
            monster.transform.position = GetSpawnPosition();
            monsters.Add(monster);
        }
        return monsters;
    }

    public List<GameObject> SpawnBoss(int count = 1)
    {
        List<GameObject> monsters = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var monster = PoolManager.Instance.bossPool.GetPoolObject();
            monster.transform.position = GetSpawnPosition();
            monsters.Add(monster);
        }
        return monsters;
    }


    // 40  60  100  180  320 codec
    // 60  90  120  150  180 dot
    // 20  40  60  80  100   basic

    // Blue 40
    // yellow 80
    // red  150

    // purple 1 = 450
    // middleBoss 1 x 3 1000
    // middleBoss 2 x 6 1000
    // middleBoss 3 x 9 10000

    // finalBoss 5000


    private IEnumerator SpawnEnemys()
    {
        for (int i = 0; i < DataManager.Instance.GetWaveCount(); i++)
        {
            var waveData = DataManager.Instance.GetWaveData(i);
            MonsterData mobData; 
            for (int j = 0; j < waveData.waveCount; j++)
            {
                mobData = DataManager.Instance.GetMonsterData(0);
                var child = SpawnEnemy(waveData.mob0Count);
                for (int z = 0; z < child.Count; z++)
                {
                    if (child[z].TryGetComponent<Monster>(out var monsterComponent))
                        monsterComponent.Init(mobData.hp, mobData.speed, mobData.damage);
                }

                mobData = DataManager.Instance.GetMonsterData(1);
                child = SpawnYellowEnemy(waveData.mob1Count);
                for (int z = 0; z < child.Count; z++)
                {
                    if (child[z].TryGetComponent<Monster>(out var monsterComponent))
                        monsterComponent.Init(mobData.hp, mobData.speed, mobData.damage);
                }

                mobData = DataManager.Instance.GetMonsterData(2);
                child = SpawnRedEnemy(waveData.mob2Count);
                for (int z = 0; z < child.Count; z++)
                {
                    if (child[z].TryGetComponent<Monster>(out var monsterComponent))
                        monsterComponent.Init(mobData.hp, mobData.speed, mobData.damage);
                }
                yield return YieldInstructionCache.WaitForSeconds(waveData.waveTerm);
            }
        }
        yield return null;

        GameObject obj;
        Rigidbody rb;
        GameObject player = GameManager.Instance.Player;
        for(int i = 0; i < 40; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                obj = PoolManager.Instance.archiveItemPool.GetPoolObject();
                obj.transform.position = player.transform.position + new Vector3(0, 5, 0);
                if (!(rb = obj.GetComponent<Rigidbody>()))
                    rb = obj.AddComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.AddForce(new Vector3(Random.Range(-20, 20), Random.Range(10, 20), Random.Range(-20, 20)));
            }
            yield return YieldInstructionCache.WaitForSeconds(0.3f);
        }

        UIManager.Instance.OpenOutro();
    }


    private IEnumerator SpawnBossCo()
    {
        yield return YieldInstructionCache.WaitForSeconds(240f);



        // 3 MiddleBoss
        MonsterData mobData; 
        var child = SpawnPurpleEnemy(25);
        for (int z = 0; z < child.Count; z++)
        {
            mobData = DataManager.Instance.GetMonsterData(2);
            if (child[z].TryGetComponent<Monster>(out var monsterComponent))
                monsterComponent.Init(mobData.hp / 2f, 1f, mobData.damage);
        }
        yield return YieldInstructionCache.WaitForSeconds(1);
        mobData = DataManager.Instance.GetMonsterData(3);
        for (int i = 0; i < 3; i++)
        {
            var mob = PoolManager.Instance.middleBossPool.GetPoolObject();
            mob.transform.position = GetSpawnPosition();
            if (mob.TryGetComponent<Monster>(out var monsterComponent))
                monsterComponent.Init(mobData.hp, mobData.speed, mobData.damage);
        }


        

        yield return YieldInstructionCache.WaitForSeconds(150f);
        // 6 middleBoss

        mobData = DataManager.Instance.GetMonsterData(3);
        for (int i = 0; i < 6; i++)
        {
            var mob = PoolManager.Instance.middleBossPool.GetPoolObject();
            mob.transform.position = GetSpawnPosition();
            if (mob.TryGetComponent<Monster>(out var monsterComponent))
                monsterComponent.Init(mobData.hp, mobData.speed, mobData.damage);
        }
        yield return YieldInstructionCache.WaitForSeconds(150f);

        // 9 middleBoss(x10)

        mobData = DataManager.Instance.GetMonsterData(3);
        for (int i = 0; i < 9; i++)
        {
            var mob = PoolManager.Instance.middleBossPool.GetPoolObject();
            mob.transform.position = GetSpawnPosition();
            if (mob.TryGetComponent<Monster>(out var monsterComponent))
                monsterComponent.Init(mobData.hp * 10, mobData.speed, mobData.damage);
        }
        yield return YieldInstructionCache.WaitForSeconds(150f);

        // 1 FinalBoss

        yield return YieldInstructionCache.WaitForSeconds(1);
        

    }

    private void SpawnFinalBoss()
    {

        var mobData = DataManager.Instance.GetMonsterData(2);
        var child = SpawnPurpleEnemy(40);
        for (int z = 0; z < child.Count; z++)
        {
            if (child[z].TryGetComponent<Monster>(out var monsterComponent))
                monsterComponent.Init(mobData.hp * 10, mobData.speed, mobData.damage);
        }

        mobData = DataManager.Instance.GetMonsterData(4);
        for (int i = 0; i < 1; i++)
        {
            var mob = PoolManager.Instance.bossPool.GetPoolObject();
            mob.transform.position = GetSpawnPosition();
            if (mob.TryGetComponent<Monster>(out var monsterComponent))
                monsterComponent.Init(mobData.hp, mobData.speed, mobData.damage);
        }
    }




    //private IEnumerator SpawnBigWhiteEnemys()
    //{
    //    for (int i = 0; i < DataManager.Instance.GetWaveWhiteCount(); i++)
    //    {
    //        var waveData = DataManager.Instance.GetWaveWhiteData(i);
    //        var mobData = DataManager.Instance.GetMonsterData(0);
    //        for (int j = 0; j < waveData.waveCount; j++)
    //        {
    //            var child = SpawnEnemy(waveData.mob0Count);
    //            for (int z = 0; z < child.Count; z++)
    //            {
    //                if (child[z].TryGetComponent<Monster>(out var monsterComponent))
    //                    monsterComponent.Init(mobData.hp * 2, 1, mobData.damage);

    //                yield return YieldInstructionCache.WaitForSeconds(waveData.waveTerm);
    //            }
    //        }
    //    }
    //    yield return null;
    //}

    //private IEnumerator SpawnBigGreenEnemys()
    //{
    //    for (int i = 0; i < DataManager.Instance.GetWaveGreenDataCount(); i++)
    //    {
    //        var waveData = DataManager.Instance.GetWaveGreenData(i);
    //        var mobData = DataManager.Instance.GetMonsterData(1);
    //        for (int j = 0; j < waveData.waveCount; j++)
    //        {
    //            var child = SpawnEnemy(waveData.mob0Count);
    //            for (int z = 0; z < child.Count; z++)
    //            {
    //                if (child[z].TryGetComponent<Monster>(out var monsterComponent))
    //                    monsterComponent.Init(mobData.hp * 2, 1, mobData.damage);
    //                yield return YieldInstructionCache.WaitForSeconds(waveData.waveTerm);
    //            }
    //        }
    //    }
    //    yield return null;
    //}


    public void SpawnItem(ItemType itemType, Vector3 position)
    {
        switch (itemType)
        {
            case ItemType.Heal:
                PoolManager.Instance.hpItemPool.GetPoolObject().transform.position = position;
                break;

            case ItemType.Exp:
                PoolManager.Instance.expItemPool.GetPoolObject().transform.position = position;
                break;

            case ItemType.Archive:
                PoolManager.Instance.archiveItemPool.GetPoolObject().transform.position = position;
                break;
        }
    }

    public void MonsterAddDieEvent(Monster monster)
    {
        if (Random.Range(0, 101) <= 60)
            SpawnItem(ItemType.Exp, monster.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f)));
        if (Random.Range(0, 101) <= 30)
            SpawnItem(ItemType.Archive, monster.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f)));

        if (Random.Range(0, 101) <= 8)
            SpawnItem(ItemType.Heal, monster.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f)));


        var effect = PoolManager.Instance.dieEffectPool.GetPoolObject();
        effect.transform.position = monster.transform.position;
        effect.GetComponent<Effect>().Init();
    }

    public void MonsterAddHitEvent(Monster monster)
    {
        var effect = PoolManager.Instance.hitEffectPool.GetPoolObject();
        effect.transform.position = monster.transform.position;
        effect.GetComponent<Effect>().Init();
    }

    private void OnDisable()
    {
        Monster.OnMonsterDie -= MonsterAddDieEvent;
        Monster.OnMonsterHit -= MonsterAddHitEvent;
    }
}