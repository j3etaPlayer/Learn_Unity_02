using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform enemySpawnPosition;
    [SerializeField] private GameObject dollarItem;
    [SerializeField] private Transform dollarSpawnPosition;
    [SerializeField] private GameObject powerUpItem;
    [SerializeField] private Transform powerUpSpawnPosition;
    [SerializeField] private Transform spawnZone;
    public int enemyCount = 0;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        SpawnEnemy(waveNumber);
        SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<SampleEnemy>().Length;
        if (enemyCount == 0)
        {
            Debug.Log("hiUpdate");
            SpawnEnemy(waveNumber);
            waveNumber++;
        }
    }

    // 한개의 적만 특정위치에서 생성되는 코드
    private void SpawnEnemy(int spawnNumber)
    {
        for(int i = 0; i < spawnNumber; i++)
        {
            GameObject enemyObj = Instantiate(enemy, enemySpawnPosition.position, Quaternion.identity);
        }
    }
    private GameObject SpawnItem()
    {
        GameObject itemObj;
        bool randBool = Random.value > 0.5;

        Debug.Log(randBool);
        if (randBool)
            itemObj = Instantiate(dollarItem, dollarSpawnPosition.position, Quaternion.identity);
        else
            itemObj = Instantiate(powerUpItem, powerUpSpawnPosition.position, Quaternion.identity);

        return itemObj;
    }
    //private Vector3 RandomSpawnPosition()
    //{
    //    Vector3 random = Random.rotation()
    //    Vector3 randomPos = new Vector3(spawnZone.position);
    //    return randomPos;
    //}
}
