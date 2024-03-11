using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform enemySpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 한개의 적만 특정위치에서 생성되는 코드
    private void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(enemy, enemySpawnPosition.position, Quaternion.identity);
    }
}
