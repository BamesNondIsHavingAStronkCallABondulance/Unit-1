using System.Collections;
using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject goblinSpawnerPrefab;

    [SerializeField]
    private float spawnerInterval = 1f;

    int count = 0;
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnerInterval, goblinSpawnerPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        if (count < 6)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(-130, -99), -86), Quaternion.identity);
            count += 1;
            StartCoroutine(spawnEnemy(interval, enemy));
        }
    } 
}
