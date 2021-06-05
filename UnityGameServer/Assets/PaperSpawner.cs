using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSpawner : MonoBehaviour
{
    public static Dictionary<int, PaperSpawner> spawners = new Dictionary<int, PaperSpawner>();
    private static int nextSpawnerId = 1;

    public int spawnerId;
    public bool hasItem = false;

    private void Start()
    {
        hasItem = false;
        spawnerId = nextSpawnerId;
        nextSpawnerId++;
        spawners.Add(spawnerId, this);

        StartCoroutine(SpawnItem());
    }

    public void PaperPickedUp(Player _player)
    {
        if (hasItem)
        {
            ItemPickedUp(_player.id);
        }
    }

    private IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(0.1f);

        hasItem = true;
        ServerSend.ItemSpawned(spawnerId);
    }

    private void ItemPickedUp(int _byPlayer)
    {
        hasItem = false;
        ServerSend.PaperPickedUp(spawnerId, _byPlayer);
    }
}
