using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntitySpawner : MonoBehaviour
{
    private List<Entity> enemies = new List<Entity>();

    public void StartSpawn()
    {
        TimeManager.Instance.StartTimer(10, 0);
        StartCoroutine("SpawnEnemys");
        SpawnEnvironment(10);
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


    private IEnumerator SpawnEnemys()
    {
        for (int i = 0; i < DataManager.Instance.GetWaveCount(); i++) {
            var waveData = DataManager.Instance.GetWaveData(i);
            var mobData = DataManager.Instance.GetMonsterData(0);
            for(int j = 0; j < waveData.waveCount; j++)
            {
                var child = SpawnEnemy(waveData.mob0Count);
                for(int z = 0; z < child.Count; z++)
                {
                    if (child[z].TryGetComponent<Monster>(out var monsterComponent))
                        monsterComponent.Init(mobData.hp, mobData.speed, mobData.damage);
                }
                yield return YieldInstructionCache.WaitForSeconds(waveData.waveTerm);
            }
        }
        yield return null;
    }
}